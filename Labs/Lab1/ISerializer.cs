namespace Lab1
{
    interface ISerializer
    {
        T Deserialize<T>(string str);
        string Serialize<T>(T obj);
    }
}
