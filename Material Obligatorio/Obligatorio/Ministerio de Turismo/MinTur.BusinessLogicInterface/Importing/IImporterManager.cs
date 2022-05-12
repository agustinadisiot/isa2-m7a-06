using MinTur.Domain.Importing;
using System.Collections.Generic;

namespace MinTur.BusinessLogicInterface.Importing
{
    public interface IImporterManager
    {
        List<IImporterAdapter> GetImporters();
        ImportingResult ImportResources(ImportingInput input);
    }
}
