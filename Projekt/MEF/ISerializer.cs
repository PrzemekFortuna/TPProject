namespace MEF
{
    public interface ISerializer<T>
    {
        void Serialize(T t, string fileName);
        T Deserialize(string path);
    }
}
