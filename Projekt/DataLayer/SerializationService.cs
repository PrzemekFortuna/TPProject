using AutoMapper;
using BaseModel.Reflection;
using BusinessLogic.Reflection;
using MEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    [Export(typeof(SerializationService))]
    public class SerializationService
    {
        [Import(typeof(ISerializer))]
        private ISerializer _serializer;

        public ISerializer Serializer { get => _serializer; }
        private IMapper _mapper;

        public SerializationService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReflectionModel, BaseReflectionModel>().PreserveReferences().ReverseMap();
                cfg.CreateMap<Namespace, BaseNamespaceModel>().PreserveReferences().ReverseMap();
                cfg.CreateMap<ReflectedType, BaseReflectedType>().PreserveReferences().ReverseMap();
                cfg.CreateMap<Field, BaseFieldModel>().PreserveReferences().ReverseMap();
                cfg.CreateMap<Parameter, BaseParameterModel>().PreserveReferences().ReverseMap();
                cfg.CreateMap<Property, BasePropertyModel>().PreserveReferences().ReverseMap();
                cfg.CreateMap<Method, BaseMethodModel>().PreserveReferences().ReverseMap();
                //cfg.CreateMap<Kind, BaseKindModel>().PreserveReferences().ReverseMap();
                //cfg.CreateMap<AccessModifier, BaseAccessModifier>().PreserveReferences().ReverseMap();
                cfg.DisableConstructorMapping();
            });
            _mapper = config.CreateMapper();
        }

        public ReflectionModel Deserialize(string path)
        {
            BaseReflectionModel baseReflectionModel = Serializer.Deserialize(path);
            return _mapper.Map<ReflectionModel>(baseReflectionModel);
        }

        public void Serialize(ReflectionModel model, string path)
        {
            _serializer.Serialize(_mapper.Map<BaseReflectionModel>(model), path);
        }
    }
}
