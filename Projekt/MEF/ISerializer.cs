using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TPProjectLib.Utility
{
    public interface ISerializer<T>
    {
        void Serialize(T t, string fileName);
        T Deserialize(string path);
    }
}
