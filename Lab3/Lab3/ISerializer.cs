namespace Lab3
{
    public interface ISerializer
    {
        T Deserialize<T>(byte[] str);
        byte[] Serialize<T>(T obj);
    }
}
