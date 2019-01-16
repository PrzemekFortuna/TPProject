using AutoMapper;
using BaseModel.Reflection;
using DBSerializer.DbModel;
using MEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer
{    
    [Export(typeof(ISerializer))]
    public class DbSerializer : ISerializer
    {
        private IMapper _mapper;
        public ReflectionContext Context { get; set; } = new ReflectionContext();

        public DbSerializer()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BaseReflectionModel, DbReflectionModel>();
                cfg.CreateMap<BaseNamespaceModel, DbNamespaceModel>();
                cfg.CreateMap<BaseReflectedType, DbReflectedType>();
                cfg.CreateMap<BaseMethodModel, DbMethodModel>();
                cfg.CreateMap<BasePropertyModel, DbPropertyModel>();
                cfg.CreateMap<BaseFieldModel, DbFieldModel>();
                cfg.CreateMap<BaseParameterModel, DbParameterModel>();
                //cfg.CreateMap<BaseAccessModifier, XMLAccessModifier>();
                //cfg.CreateMap<BaseKindModel, XMLKindModel>();
                cfg.DisableConstructorMapping();
            });

            _mapper = config.CreateMapper();
        }
        public BaseReflectionModel Deserialize(string path)
        {

            Context.ReflectedTypes
                .Include(t => t.BaseType)
                .Include(t => t.Fields)
                .Include(t => t.Constructors)
                .Include(t => t.Methods)
                .Include(t => t.Properties)
                .Include(t => t.ImplementedInterfaces).Load();

            Context.NamespaceModels
                .Include(n => n.Classes)
                .Include(n => n.Interfaces)
                .Include(n => n.ValueTypes).Load();

            Context.ParameterModels
                .Include(p => p.ParamType).Load();

            Context.PropertyModels
                .Include(p => p.GetMethod)
                .Include(p => p.SetMethod)
                .Include(p => p.Type).Load();

            Context.MethodModels
                .Include(m => m.Parameters)
                .Include(m => m.ReturnType).Load();

            Context.FieldModels
                .Include(f => f.Type).Load();

            DbReflectionModel model = Context.AssemblyModels
                .Include(a => a.Namespaces)
                .First() ?? throw new ArgumentException("No such assembly");

            return _mapper.Map<BaseReflectionModel>(model);
        }

        public void Serialize(BaseReflectionModel model, string fileName)
        {
            DbReflectionModel dbModel = _mapper.Map<DbReflectionModel>(model);

            Context.AssemblyModels.Add(dbModel);
            Context.SaveChanges();
        }
    }
}
