using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer.DbModel
{
    public class DbFieldModel
    {
        public int DbFieldModelId { get; set; }
        public string Name { get; set; }
        public BaseAccessModifier Access { get; set; }
        public DbReflectedType Type { get; set; }

        [InverseProperty("Type")]
        public virtual ICollection<DbReflectedType> FieldTypes { get; set; }
    }
}
