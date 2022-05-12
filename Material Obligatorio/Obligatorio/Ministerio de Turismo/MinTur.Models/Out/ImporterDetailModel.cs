using MinTur.BusinessLogicInterface.Importing;
using MinTur.Domain.Importing;
using System;
using System.Collections.Generic;

namespace MinTur.Models.Out
{
    public class ImporterDetailModel
    {
        public string Name { get; set; }
        public List<ImporterParameterModel> Parameters { get; set; }

        public ImporterDetailModel(IImporterAdapter importer)
        {
            Name = importer.GetName();
            Parameters = new List<ImporterParameterModel>();

            foreach (ImporterParameter parameter in importer.GetNecessaryParameters())
            {
                Parameters.Add(new ImporterParameterModel(parameter));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var importerDetailModel = obj as ImporterDetailModel;
            return Name == importerDetailModel.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
