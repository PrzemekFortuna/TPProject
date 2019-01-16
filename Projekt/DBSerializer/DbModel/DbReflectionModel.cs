using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer.DbModel
{
    public class DbReflectionModel : BaseReflectionModel
    {
        public int DbReflectionModelId { get; set; }
        public new List<DbNamespaceModel> Namespaces { get; set; }
    }
}
