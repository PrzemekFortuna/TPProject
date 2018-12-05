using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace TPProjectLib.Utility
{
    [Export(typeof(ISerializer<>))]
    public class XMLSerializer<T> : ISerializer<T>
    {
        public T Deserialize(string path)
        {
            T obj = default(T);
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    obj = (T)serializer.ReadObject(reader, true);
                }
            }
            return obj;
        }

        public void Serialize(T t, string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(fileStream, t);
            }
        }
    }
}
