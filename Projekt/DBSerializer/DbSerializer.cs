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
                cfg.CreateMap<BaseReflectionModel, DbReflectionModel>().ForMember(dest => dest.DbReflectionModelId, opt => opt.Ignore()); ;
                cfg.CreateMap<BaseNamespaceModel, DbNamespaceModel>().ForMember(dest => dest.DbNamespaceModelId, opt => opt.Ignore());
                cfg.CreateMap<BaseReflectedType, DbReflectedType>().ForMember(dest => dest.DbReflectedTypeId, opt => opt.Ignore());
                cfg.CreateMap<BaseMethodModel, DbMethodModel>().ForMember(dest => dest.DbMethodModelId, opt => opt.Ignore());
                cfg.CreateMap<BasePropertyModel, DbPropertyModel>().ForMember(dest => dest.DbPropertyModelId, opt => opt.Ignore());
                cfg.CreateMap<BaseFieldModel, DbFieldModel>().ForMember(dest => dest.DbFieldModelId, opt => opt.Ignore());
                cfg.CreateMap<BaseParameterModel, DbParameterModel>().ForMember(dest => dest.DbParameterModelId, opt => opt.Ignore());
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
            ClearDB();
            DbReflectionModel dbModel = _mapper.Map<DbReflectionModel>(model);
            dbModel.Name = "DbAssemblyModel";
            Context.AssemblyModels.Add(dbModel);
            Context.SaveChanges();
        }

        private void ClearDB()
        {
            using (ReflectionContext context = new ReflectionContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM DbParameterModels");
                context.Database.ExecuteSqlCommand("DELETE FROM DbMethodModels");
                context.Database.ExecuteSqlCommand("DELETE FROM DbPropertyModels");
                context.Database.ExecuteSqlCommand("DELETE FROM DbFieldModels");
                context.Database.ExecuteSqlCommand("ALTER TABLE DbReflectedTypeDbReflectedTypes NOCHECK CONSTRAINT ALL");
                context.Database.ExecuteSqlCommand("DELETE FROM DbReflectedTypes");
                context.Database.ExecuteSqlCommand("ALTER TABLE DbReflectedTypeDbReflectedTypes CHECK CONSTRAINT ALL");
                context.Database.ExecuteSqlCommand("DELETE FROM DbNamespaceModels");
                context.Database.ExecuteSqlCommand("DELETE FROM DbReflectionModels");
            }
        }
    }
}
