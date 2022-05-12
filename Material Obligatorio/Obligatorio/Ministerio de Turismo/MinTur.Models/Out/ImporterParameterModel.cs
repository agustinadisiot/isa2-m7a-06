using MinTur.Domain.Importing;

namespace MinTur.Models.Out
{
    public class ImporterParameterModel
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public ImporterParameterModel(ImporterParameter parameter)
        {
            Name = parameter.Name;
            Type = parameter.Type.ToString();
        }
    }
}
