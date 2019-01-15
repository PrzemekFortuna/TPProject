using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using AutoMapper;
using BaseModel.Reflection;
using MEF;
using Serializers.XMLModel;

namespace Serializers
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        private IMapper _mapper;
        public XMLSerializer()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BaseReflectionModel, XMLReflectionModel>();
                cfg.CreateMap<BaseNamespaceModel, XMLNamespaceModel>();
                cfg.CreateMap<BaseReflectedType, XMLReflectedType>();
                cfg.CreateMap<BaseMethodModel, XMLMethodModel>();
                cfg.CreateMap<BasePropertyModel, XMLPropertyModel>();
                cfg.CreateMap<BaseFieldModel, XMLFieldModel>();
                cfg.CreateMap<BaseParameterModel, XMLParameterModel>();
                cfg.CreateMap<BaseAccessModifier, XMLAccessModifier>();
                cfg.CreateMap<BaseKindModel, XMLKindModel>();
                cfg.DisableConstructorMapping();
            });

            _mapper = config.CreateMapper();
        }
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
            return _mapper.Map<BaseReflectionModel>(obj);
        }

        public void Serialize(BaseReflectionModel t, string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                XMLReflectionModel xmlModel = _mapper.Map<XMLReflectionModel>(t);
                DataContractSerializer serializer = new DataContractSerializer(typeof(XMLReflectionModel));
                serializer.WriteObject(fileStream, xmlModel);
            }
        }
    }
}
