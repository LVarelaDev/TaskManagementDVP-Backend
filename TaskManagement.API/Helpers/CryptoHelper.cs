using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace TaskManagement.API.Helpers
{
    public class CryptoHelper
    {
        private readonly IConfiguration _configuration;

        public CryptoHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Encrypt<T>(T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var plainTextBytes = Encoding.UTF8.GetBytes(json);

            string keyValue = _configuration["Crypto:Key"] ?? "";
            string IvValue = _configuration["Crypto:IV"] ?? "";

            byte[] keyBytes = Encoding.Default.GetBytes(keyValue);
            byte[] ivBytes = Encoding.Default.GetBytes(IvValue);

            using var aes = Aes.Create();
            using var encryptor = aes.CreateEncryptor(keyBytes, ivBytes);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

            cs.Write(plainTextBytes, 0, plainTextBytes.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public T Decrypt<T>(string encryptedText)
        {
            var buffer = Convert.FromBase64String(encryptedText);

            string keyValue = _configuration["Crypto:Key"] ?? "";
            string IvValue = _configuration["Crypto:IV"] ?? "";

            byte[] Key = Encoding.UTF8.GetBytes(keyValue);
            byte[] IV = Encoding.UTF8.GetBytes(IvValue);

            using var aes = Aes.Create();
            using var decryptor = aes.CreateDecryptor(Key, IV);
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            var decryptedText = sr.ReadToEnd();
            return JsonSerializer.Deserialize<T>(decryptedText);
        }
    }
}
