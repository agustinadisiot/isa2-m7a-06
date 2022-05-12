using MinTur.Domain.Importing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Models.Out
{
    public class ImportingResultModel
    {
        public List<ResortDetailsModel> SuccesfulImportedResorts { get; set; }
        public List<TouristPointBasicInfoModel> SuccesfulImportedTouristPoints { get; set; }
        public List<FailedResortModel> FailedImportingResorts { get; set; }

        public ImportingResultModel(ImportingResult importingResult)
        {
            SuccesfulImportedResorts = importingResult.SuccesfulImportedResorts
                .Select(r => new ResortDetailsModel(r)).ToList();
            SuccesfulImportedTouristPoints = importingResult.SuccesfulImportedTouristPoints
                .Select(t => new TouristPointBasicInfoModel(t)).ToList();
            FailedImportingResorts = importingResult.FailedImportingResorts
                .Select(r => new FailedResortModel(r)).ToList();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var importingResultModel = obj as ImportingResultModel;
            return SuccesfulImportedResorts.SequenceEqual(importingResultModel.SuccesfulImportedResorts)
                && SuccesfulImportedTouristPoints.SequenceEqual(importingResultModel.SuccesfulImportedTouristPoints)
                && FailedImportingResorts.SequenceEqual(importingResultModel.FailedImportingResorts);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SuccesfulImportedResorts, SuccesfulImportedTouristPoints
                , FailedImportingResorts);
        }
    }
}
