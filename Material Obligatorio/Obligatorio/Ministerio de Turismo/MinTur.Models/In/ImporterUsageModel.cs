using MinTur.Domain.Importing;
using System.Collections.Generic;

namespace MinTur.Models.In
{
    public class ImporterUsageModel
    {
        public string ImporterName { get; set; }
        public List<ImporterParameterIntent> Parameters { get; set; }

        public ImporterUsageModel()
        {
            Parameters = new List<ImporterParameterIntent>();
        }

        public ImportingInput ToEntity()
        {
            ImportingInput importingInput = new ImportingInput()
            {
                ImporterName = ImporterName
            };

            foreach(ImporterParameterIntent parameterIntent in Parameters)
            {
                importingInput.Parameters.Add(new ImporterParameterInput()
                {
                    Name = parameterIntent.Name,
                    Value = parameterIntent.Value
                });
            }

            return importingInput;
        }
    }
}
