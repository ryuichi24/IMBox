namespace IMBox.Shared.Database.MongoDB
{
    public class MongoDBSettings
    {
        public string Host { get; init; }

        public int Port { get; init; }

        public string Username { get; init; }

        public string Password { get; init; }

        public string ConnectionString => $"mongodb://{Username}:{Password}@{Host}:{Port}";
    }
}