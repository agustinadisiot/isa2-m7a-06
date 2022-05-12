using MinTur.Domain.BusinessEntities;
using MinTur.Domain.Importing;
using System.Collections.Generic;

namespace MinTur.BusinessLogicInterface.Importing
{
    public interface IImporterAdapter
    {
        string GetName();
        List<ImporterParameter> GetNecessaryParameters();
        List<Resort> RetrieveResorts(List<ImporterParameterInput> parameters);
    }
}
