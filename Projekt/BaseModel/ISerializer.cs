using BaseModel.Reflection;

namespace MEF
{
    public interface ISerializer
    {
        void Serialize(ReflectionModel model, string fileName);
        BaseReflectionModel Deserialize(string path);
    }
}
