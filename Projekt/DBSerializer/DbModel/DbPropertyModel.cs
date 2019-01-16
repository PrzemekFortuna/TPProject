using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer.DbModel
{
    public class DbPropertyModel
    {
        public enum Access
        {
            ReadOnly,
            ReadWrite
        }
        public int DbPropertyModelId { get; set; }
        public string Name { get; set; }
        public Access PropertyAccess { get; set; }
        public DbMethodModel SetMethod { get; set; }
        public DbMethodModel GetMethod { get; set; }
        public DbReflectedType Type { get; set; }
    }
}
