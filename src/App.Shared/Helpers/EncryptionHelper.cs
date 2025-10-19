using System.Security.Cryptography;
using System.Text;

public static class EncryptionHelper
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456");

    public static string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string cipherText)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        var buffer = Convert.FromBase64String(cipherText);
        using var ms = new MemoryStream(buffer);
        using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}
