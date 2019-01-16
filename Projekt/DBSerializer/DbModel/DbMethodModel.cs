using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer.DbModel
{
    public class DbMethodModel
    {
        public int DbMethodModelId { get; set; }
        public string Name { get; set; }
        public DbReflectedType ReturnType { get; set; }
        public BaseAccessModifier Access { get; set; }
        public List<DbParameterModel> Parameters { get; set; }
    }
}
