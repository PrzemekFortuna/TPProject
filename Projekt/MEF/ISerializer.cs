using BaseModel.Reflection;

namespace MEF
{
    public interface ISerializer
    {
        void Serialize(BaseReflectionModel model, string fileName);
        BaseReflectionModel Deserialize(string path);
    }
}
