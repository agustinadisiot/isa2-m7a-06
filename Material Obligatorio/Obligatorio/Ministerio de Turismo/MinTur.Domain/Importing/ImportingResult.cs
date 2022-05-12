using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Domain.Importing
{
    public class ImportingResult
    {
        public List<Resort> SuccesfulImportedResorts { get; set; }
        public List<TouristPoint> SuccesfulImportedTouristPoints { get; set; }
        public List<KeyValuePair<Resort, string>> FailedImportingResorts { get; set; }

        public ImportingResult()
        {
            SuccesfulImportedResorts = new List<Resort>();
            SuccesfulImportedTouristPoints = new List<TouristPoint>();
            FailedImportingResorts = new List<KeyValuePair<Resort, string>>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var importingResult = obj as ImportingResult;
            return SuccesfulImportedResorts.SequenceEqual(importingResult.SuccesfulImportedResorts)
                && SuccesfulImportedTouristPoints.SequenceEqual(importingResult.SuccesfulImportedTouristPoints)
                && FailedImportingResorts.SequenceEqual(importingResult.FailedImportingResorts);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SuccesfulImportedResorts, SuccesfulImportedTouristPoints, FailedImportingResorts);
        }
    }
}
