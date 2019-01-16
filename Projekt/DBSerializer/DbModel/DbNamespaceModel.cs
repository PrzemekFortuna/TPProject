using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer.DbModel
{
    public class DbNamespaceModel : BaseNamespaceModel
    {
        public int DbNamespaceModelId { get; set; }
        public string Name { get; set; }
        public List<DbReflectedType> Classes { get; set; }
        public List<DbReflectedType> Interfaces { get; set; }
        public List<DbReflectedType> ValueTypes { get; set; }
    }
}
