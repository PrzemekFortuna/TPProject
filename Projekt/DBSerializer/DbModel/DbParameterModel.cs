﻿using BaseModel.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer.DbModel
{
    public class DbParameterModel
    {
        public int DbParameterModelId { get; set; }
        public string Name { get; set; }
        public DbReflectedType ParamType { get; set; }

        //[InverseProperty("ParamType")]
        public virtual ICollection<DbReflectedType> ParameterTypes { get; set; }
        public virtual ICollection<DbMethodModel> MethodParameters { get; set; }

    }
}
