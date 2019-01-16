using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer.DbModel
{
    public class DbReflectionModel
    {
        public int DbReflectionModelId { get; set; }
        public string Name { get; set; }
        public List<DbNamespaceModel> Namespaces { get; set; }
    }
}
