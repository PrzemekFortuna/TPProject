using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TPProjectLib.Model;
using TPProjectLib.Reflection;

namespace TPProjectLib.Utility
{
    public class XMLSerializer : ISerializer<Student>
    {
        public Student Deserialize(string path)
        {
            Student student = null;
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Student));
                    student = (Student)serializer.ReadObject(reader, true);
                }
            }
            return student;
        }

        public ReflectionModel DeserializeReflecion(string path)
        {
            ReflectionModel reflecion = null;
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(ReflectionModel));
                    reflecion = (ReflectionModel)serializer.ReadObject(reader, true);
                }
            }
            return reflecion;
        }

        public void Serialize(Student t)
        {
            using (FileStream fileStream = new FileStream("xmlfile.xml", FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Student));
                serializer.WriteObject(fileStream, t);
            }
        }

        public void Serialize(ReflectionModel reflecion, string filename)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(ReflectionModel));
                serializer.WriteObject(fileStream, reflecion);
            }
        }
    }
}
