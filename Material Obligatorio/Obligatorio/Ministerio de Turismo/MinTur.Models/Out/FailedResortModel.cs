using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;

namespace MinTur.Models.Out
{
    public class FailedResortModel
    {
        public ResortDetailsModel Resort { get; set; }
        public string ErrorInCreation { get; set; }

        public FailedResortModel(KeyValuePair<Resort, string> failedResort)
        {
            Resort = new ResortDetailsModel(failedResort.Key);
            ErrorInCreation = failedResort.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var failedResortModel = obj as FailedResortModel;
            return Resort.Equals(failedResortModel.Resort);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Resort);
        }
    }
}
