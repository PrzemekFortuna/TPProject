using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer.DbModel
{
    public class DbParameterModel : BaseParameterModel
    {
        public string Name { get; set; }
        public DbReflectedType ParamType { get; set; }
    }
}
