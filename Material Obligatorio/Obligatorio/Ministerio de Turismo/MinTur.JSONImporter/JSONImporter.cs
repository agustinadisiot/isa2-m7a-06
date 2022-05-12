using MinTur.Exceptions;
using MinTur.ImporterInterface.DTOs;
using MinTur.ImporterInterface.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MinTur.JSONImporter
{
    public class JSONImporter : IImporter
    {
        private List<ImporterParameterDescription> _requiredParameters;
        private string basePathForJsonFiles;

        public JSONImporter(IConfiguration configuration)
        {
            basePathForJsonFiles = configuration.GetSection("JSONFilesForImporter").Value;

            _requiredParameters = new List<ImporterParameterDescription>()
            {
                new ImporterParameterDescription()
                {
                    Name = "File to parse",
                    Type = PossibleParameters.File
                }
            };
        }

        public string GetName()
        {
            return "JSON Importer";
        }

        public List<ImporterParameterDescription> GetNecessaryParameters()
        {
            return new List<ImporterParameterDescription>(_requiredParameters);
        }

        public List<ImportedResort> RetrieveResorts(List<ImportingParameterValue> parameters)
        {
            List<ImportedResort> parsedResorts = new List<ImportedResort>();

            ValidateParametersOrFail(parameters);
            string fileName = parameters.Find(p => p.Name == "File to parse").Value;

            try
            {
                string jsonString = File.ReadAllText(basePathForJsonFiles + '/' + fileName);

                if (jsonString.StartsWith('['))
                    parsedResorts = JsonConvert.DeserializeObject<List<ImportedResort>>(jsonString);
                else
                    parsedResorts.Add(JsonConvert.DeserializeObject<ImportedResort>(jsonString));

                return parsedResorts;
            }
            catch (FileNotFoundException)
            {
                throw new InvalidRequestDataException("Couldnt find specified file");
            }
        }

        private void ValidateParametersOrFail(List<ImportingParameterValue> parameters)
        {
            if (parameters.Count != _requiredParameters.Count)
                throw new InvalidRequestDataException("Wrong number of parameters for this importer");

            if (!_requiredParameters.TrueForAll(rp => parameters.Any(p => p.Name == rp.Name)))
                throw new InvalidRequestDataException("Wrong parameters for this importer");

        }
    }
}
