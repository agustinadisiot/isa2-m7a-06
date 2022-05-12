using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Models.Out
{
    public class ImageBasicInfoModel
    {
        public string Data { get; set; }

        public ImageBasicInfoModel(Image image) 
        {
            Data = image.Data;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var image = obj as ImageBasicInfoModel;
            return Data == image.Data;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Data);
        }

    }
}
