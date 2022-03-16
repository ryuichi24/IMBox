namespace IMBox.Shared.Infrastructure.Helpers.Hash
{
    public interface IHashHelper
    {
        public (byte[] hash, byte[] salt) CreateHash(string plainValue);
        public bool VerifyHash(string plainValue, byte[] hash, byte[] salt);
    }
}