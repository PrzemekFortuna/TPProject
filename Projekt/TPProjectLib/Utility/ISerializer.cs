using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Utility
{
    interface ISerializer<T>
    {
        void Serialize(T t);
        T Deserialize(string path);
    }
}
