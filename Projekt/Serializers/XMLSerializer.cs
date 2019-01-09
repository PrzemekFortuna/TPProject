using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using BaseModel.Reflection;
using MEF;
using Serializers.XMLModel;

namespace Serializers
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public BaseReflectionModel Deserialize(string path)
        {
            XMLReflectionModel obj = new XMLReflectionModel();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(XMLReflectionModel));
                    obj = (XMLReflectionModel)serializer.ReadObject(reader, true);
                }
            }
            return obj;
        }

        public void Serialize(BaseReflectionModel t, string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(XMLReflectionModel));
                serializer.WriteObject(fileStream, t);
            }
        }
    }
}
