using MinTur.Domain.BusinessEntities;
using MinTur.Domain.Importing;
using MinTur.ImporterInterface.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.BusinessLogic.Importing
{
    public class ImportingDTOMapper
    {
        public TouristPoint MapTouristPoint(ImportedTouristPoint importedTouristPoint)
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = importedTouristPoint.Id,
                Name = importedTouristPoint.Name,
                Image = new Image() 
                {
                    Data = importedTouristPoint.Image 
                },
                Description = importedTouristPoint.Description,
                RegionId = importedTouristPoint.RegionId,
                Region = new Region()
                {
                    Id = importedTouristPoint.RegionId
                }
            };

            foreach(int categoryId in importedTouristPoint.CategoriesId)
            {
                touristPoint.AddCategory(new Category() { Id = categoryId });
            }

            return touristPoint;
        }

        public Resort MapResort(ImportedResort importedResort)
        {
            return new Resort()
            {
                Name = importedResort.Name,
                Address = importedResort.Address,
                Description = importedResort.Description,
                Available = importedResort.Available,
                PhoneNumber = importedResort.PhoneNumber,
                ReservationMessage = importedResort.ReservationMessage,
                PricePerNight = importedResort.PricePerNight,
                Stars = importedResort.Stars,
                Images = importedResort.ImagesData.Select(d => new Image() { Data = d }).ToList(),
                TouristPoint = MapTouristPoint(importedResort.TouristPoint),
                TouristPointId = importedResort.TouristPoint.Id
            };
        }

        public List<ImportingParameterValue> MapParameterInputs(List<ImporterParameterInput> importingParameterInputs)
        {
            return importingParameterInputs.Select(p => new ImportingParameterValue()
            {
                Name = p.Name,
                Value = p.Value
            }).ToList();
        }

        public List<ImporterParameter> MapParameters(List<ImporterParameterDescription> parameters)
        {
            return parameters.Select(p => new ImporterParameter()
            {
                Name = p.Name,
                Type = p.Type.ToString()
            }).ToList();
        }
    }
}
