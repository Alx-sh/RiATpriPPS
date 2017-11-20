namespace Lab2
{
    interface IMethods
    {
        bool Ping(string host, int port);
        byte[] GetInputData(string host, int port);
        void WriteAnswer(string host, int port, byte[] requestBody);
    }
}
