using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer.DbModel
{
    public class DbPropertyModel : BasePropertyModel
    {
        //public enum Access
        //{
        //    ReadOnly,
        //    ReadWrite
        //}
        public string Name { get; set; }
        public Access PropertyAccess { get; set; }
        public DbMethodModel SetMethod { get; set; }
        public DbMethodModel GetMethod { get; set; }
        public DbReflectedType Type { get; set; }
    }
}
