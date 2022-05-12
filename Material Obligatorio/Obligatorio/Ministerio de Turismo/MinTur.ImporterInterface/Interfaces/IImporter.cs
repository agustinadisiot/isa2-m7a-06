using MinTur.ImporterInterface.DTOs;
using System.Collections.Generic;

namespace MinTur.ImporterInterface.Interfaces
{
    public interface IImporter
    {
        string GetName();
        List<ImporterParameterDescription> GetNecessaryParameters();
        List<ImportedResort> RetrieveResorts(List<ImportingParameterValue> parameters);
    }
}
