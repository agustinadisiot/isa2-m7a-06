using MinTur.BusinessLogicInterface.Importing;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.Importing;
using MinTur.ImporterInterface.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.BusinessLogic.Importing
{
    public class ImporterAdapter : IImporterAdapter
    {
        private IImporter _importer;
        private ImportingDTOMapper _mapper;

        public ImporterAdapter(IImporter importer)
        {
            _importer = importer;
            _mapper = new ImportingDTOMapper();
        }

        public string GetName()
        {
            return _importer.GetName();
        }

        public List<ImporterParameter> GetNecessaryParameters()
        {
            return _mapper.MapParameters(_importer.GetNecessaryParameters());
        }

        public List<Resort> RetrieveResorts(List<ImporterParameterInput> parameters)
        {
            return _importer.RetrieveResorts(_mapper.MapParameterInputs(parameters))
                .Select(r => _mapper.MapResort(r)).ToList();
        }
    }
}
