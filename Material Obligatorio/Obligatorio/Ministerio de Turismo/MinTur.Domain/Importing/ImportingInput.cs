using System.Collections.Generic;

namespace MinTur.Domain.Importing
{
    public class ImportingInput
    {
        public string ImporterName { get; set; }
        public List<ImporterParameterInput> Parameters { get; set; }

        public ImportingInput()
        {
            Parameters = new List<ImporterParameterInput>();
        }
    }
}
