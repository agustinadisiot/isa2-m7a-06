using MinTur.ImporterInterface.DTOs;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using MinTur.Exceptions;
using System.Linq;
using MinTur.ImporterInterface.Interfaces;

namespace MinTur.XMLImporter
{
    public class XMLImporter : IImporter
    {
        private List<ImporterParameterDescription> _requiredParameters;
        private string basePathForXMLFiles;

        public XMLImporter(IConfiguration configuration)
        {
            basePathForXMLFiles = configuration.GetSection("XMLFilesForImporter").Value;

            _requiredParameters = new List<ImporterParameterDescription>()
            {
                new ImporterParameterDescription()
                {
                    Name = "File to parse",
                    Type = PossibleParameters.File
                }
            };
        }

        public string GetName()
        {
            return "XML Importer";
        }

        public List<ImporterParameterDescription> GetNecessaryParameters()
        {
            return new List<ImporterParameterDescription>(_requiredParameters);
        }

        public List<ImportedResort> RetrieveResorts(List<ImportingParameterValue> parameters)
        {
            List<ImportedResort> parsedResorts = new List<ImportedResort>();

            ValidateParametersOrFail(parameters);
            string fileName = parameters.Find(p => p.Name == "File to parse").Value;

            try
            {
                string xmlString = File.ReadAllText(basePathForXMLFiles + '/' + fileName);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlString);

                XmlNodeList resortsXML = GetResortDetailsFromXML(xmlDocument);
                for(int i = 0; i < resortsXML.Count; i++)
                {
                    string jsonString = JsonConvert.SerializeXmlNode(resortsXML.Item(i));
                    jsonString = RemoveParentObjectFromJSONString(jsonString);

                    parsedResorts.Add(JsonConvert.DeserializeObject<ImportedResort>(jsonString));
                }

                return parsedResorts;
            }
            catch (FileNotFoundException)
            {
                throw new InvalidRequestDataException("Couldnt find specified file");
            }
        }

        private XmlNodeList GetResortDetailsFromXML(XmlDocument xmlDocument)
        {
            XmlNode rootNode = xmlDocument.SelectSingleNode("root");

            if (rootNode == null)
                throw new InvalidRequestDataException("Invalid XML Format");

            XmlNodeList resortList = rootNode.SelectNodes("resort");

            if(resortList == null)
                throw new InvalidRequestDataException("Invalid XML Format");

            return resortList;
        }

        private string RemoveParentObjectFromJSONString(string jsonString)
        {
            jsonString = jsonString.Replace("\"resort\":{", string.Empty);
            jsonString = jsonString.Remove(jsonString.Length - 1);
            return jsonString;
        }

        private void ValidateParametersOrFail(List<ImportingParameterValue> parameters)
        {
            if (parameters.Count != _requiredParameters.Count)
                throw new InvalidRequestDataException("Wrong number of parameters for this importer");

            if (!_requiredParameters.TrueForAll(rp => parameters.Any(p => p.Name == rp.Name)))
                throw new InvalidRequestDataException("Wrong parameters for this importer");

        }
    }
}
