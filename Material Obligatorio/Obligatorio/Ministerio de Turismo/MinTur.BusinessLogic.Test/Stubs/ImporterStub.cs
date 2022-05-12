using Microsoft.Extensions.Configuration;
using MinTur.ImporterInterface.DTOs;
using MinTur.ImporterInterface.Interfaces;
using System.Collections.Generic;

namespace MinTur.BusinessLogic.Test.Stubs
{
    public class ImporterStub : IImporter
    {
        public ImporterStub() { }
        public ImporterStub(IConfiguration configuration) { }

        public string GetName()
        {
            return "Stub Importer";
        }

        public List<ImporterParameterDescription> GetNecessaryParameters()
        {
            return new List<ImporterParameterDescription>()
            {
                new ImporterParameterDescription()
                {
                    Name = "Archivo a parsear",
                    Type = PossibleParameters.File
                },
                new ImporterParameterDescription()
                {
                    Name = "Limite",
                    Type = PossibleParameters.Number
                }
            };
        }

        public List<ImportedResort> RetrieveResorts(List<ImportingParameterValue> parameters)
        {
            return new List<ImportedResort>()
            {
                new ImportedResort()
                {
                    Stars = 87,
                    Address = "Av Italia 123",
                    Available = true,
                    Description = "Descripcion..",
                    ImagesData = new List<string>() { "imagen" },
                    Name = "Hotel que falla por validacion",
                    PhoneNumber = "09438175",
                    ReservationMessage = "Gracias",
                    PricePerNight = 23,
                    TouristPoint = new ImportedTouristPoint()
                },
                new ImportedResort()
                {
                    Stars = 2,
                    Address = "Rambla",
                    Available = true,
                    Description = "Descripcion..",
                    ImagesData = new List<string>() { "imagen", "imagen2" },
                    Name = "Hotel que falla porque el punto turistico tiene region invalida",
                    PhoneNumber = "09938282",
                    ReservationMessage = "Thx",
                    PricePerNight = 50,
                    TouristPoint = new ImportedTouristPoint()
                    {
                        RegionId = -4,
                        Description = "Descripcion",
                        Image = "asd",
                        Name = "Nombre",
                        Id = 0,
                        CategoriesId = new List<int>() { 1, 2 }
                    }
                },
                new ImportedResort()
                {
                    Stars = 2,
                    Address = "Rambla",
                    Available = true,
                    Description = "Descripcion..",
                    ImagesData = new List<string>() { "imagen", "imagen2" },
                    Name = "Hotel que sale todo bien",
                    PhoneNumber = "09938282",
                    ReservationMessage = "Thx",
                    PricePerNight = 50,
                    TouristPoint = new ImportedTouristPoint()
                    {
                        RegionId = 2,
                        Description = "Descripcion",
                        Image = "asd",
                        Name = "Nombre",
                        Id = 0,
                        CategoriesId = new List<int>() { 1 ,3 ,4 }
                    }
                }
            };
        }

    }
}
