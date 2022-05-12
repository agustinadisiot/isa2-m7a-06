using Microsoft.Extensions.Configuration;
using MinTur.BusinessLogicInterface.Importing;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.Importing;
using MinTur.Exceptions;
using MinTur.ImporterInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MinTur.BusinessLogic.Importing
{
    public class ImporterManager : IImporterManager
    {
        private readonly IRepositoryFacade _repositoryFacade;
        private readonly IConfiguration _configuration;
        private string _importersPath;

        public ImporterManager(IConfiguration configuration, IRepositoryFacade repositoryFacade)
        {
            _repositoryFacade = repositoryFacade;
            _configuration = configuration;
        }

        public List<IImporterAdapter> GetImporters()
        {
            _importersPath = _configuration.GetSection(@"ImportersDllDirectory").Value;
            List<IImporterAdapter> availableImporters = new List<IImporterAdapter>();
            string[] filePaths = Directory.GetFiles(_importersPath);

            foreach (string file in filePaths)
            {
                if (FileIsDll(file))
                {
                    FileInfo dllFile  = new FileInfo(file);
                    Assembly myAssembly = Assembly.LoadFile(dllFile.FullName);

                    foreach (Type type in myAssembly.GetTypes())
                    {
                        if (ImplementsRequiredInterface(type))
                        {
                            IImporter instance = (IImporter)Activator.CreateInstance(type,_configuration);
                            availableImporters.Add(new ImporterAdapter(instance));
                        }
                    }
                }
            }
            return availableImporters;
        }

        public ImportingResult ImportResources(ImportingInput input)
        {
            IImporterAdapter desiredImporter = GetDesiredImporter(input);
            List<Resort> resortToStore = desiredImporter.RetrieveResorts(input.Parameters);
            ImportingResult result = new ImportingResult();

            foreach(Resort resort in resortToStore)
            {
                TryRegisteringResort(resort, result);
            }

            return result;
        }

        private void TryRegisteringResort(Resort resort, ImportingResult result)
        {
            try
            {
                resort.ValidOrFail();
                TouristPoint relatedTouristPoint = _repositoryFacade.GetTouristPointById(resort.TouristPointId);
                resort.TouristPoint = relatedTouristPoint;

                int newResortId = _repositoryFacade.StoreResort(resort);
                Resort createdResort = _repositoryFacade.GetResortById(newResortId);

                result.SuccesfulImportedResorts.Add(createdResort);
            }
            catch(ResourceNotFoundException)
            {
                TryRegisteringResortsTouristPoint(resort, result);
            }
            catch(InvalidRequestDataException e)
            {
                result.FailedImportingResorts.Add(new KeyValuePair<Resort,string>(resort, e.Message));
            }
        }

        private void TryRegisteringResortsTouristPoint(Resort resort, ImportingResult result)
        {
            try
            {
                TouristPoint touristPointToStore = resort.TouristPoint;
                touristPointToStore.ValidOrFail();

                int newTouristPointId = _repositoryFacade.StoreTouristPoint(touristPointToStore);
                TouristPoint createdTouristPoint = _repositoryFacade.GetTouristPointById(newTouristPointId);

                resort.TouristPoint = createdTouristPoint;
                resort.TouristPointId = newTouristPointId;

                result.SuccesfulImportedTouristPoints.Add(createdTouristPoint);
                TryRegisteringResort(resort, result);
            }
            catch(Exception e)
            {
                result.FailedImportingResorts.Add(new KeyValuePair<Resort, string>(resort, e.Message));
            }
        }

        private IImporterAdapter GetDesiredImporter(ImportingInput input)
        {
            List<IImporterAdapter> availableImporters = GetImporters();
            IImporterAdapter desiredImporter = null;

            foreach(IImporterAdapter importer in availableImporters)
            {
                if (importer.GetName() == input.ImporterName)
                    desiredImporter = importer;
            }

            if (desiredImporter == null)
                throw new ResourceNotFoundException("Couldnt find specified importer");

            return desiredImporter;
        }

        public bool ImplementsRequiredInterface(Type type)
        {
            return typeof(IImporter).IsAssignableFrom(type) && !type.IsInterface;
        }

        private bool FileIsDll(string file)
        {
            return file.EndsWith("dll");
        }

    }
}
