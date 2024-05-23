using Microsoft.AspNetCore.DataProtection;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

namespace TodoBlazorApp.Handlers;

public class EncryptionHandler
{
    private string _privateKey;
    private string _publicKey;

    public EncryptionHandler()
    {
        if (!File.Exists("privateKey.pem"))
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                _privateKey = rsa.ToXmlString(true);
                _publicKey = rsa.ToXmlString(false);

                File.WriteAllText("privateKey.pem", _privateKey);
                File.WriteAllText("publicKey.pem", _publicKey);
            }
        }
        else 
        { 
            _privateKey = File.ReadAllText("privateKey.pem"); 
            _publicKey = File.ReadAllText("publicKey.pem");
        }

    }

    public string AsymmetricEncryption(string textToEncrypt)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(_publicKey);

            byte[] byteArrayTextToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
            byte[] encryptedDataAsByteArray = rsa.Encrypt(byteArrayTextToEncrypt, true);
            var encryptedDataAsString = Convert.ToBase64String(encryptedDataAsByteArray);

            return encryptedDataAsString;
        }
    }

    public string AsymmetricDecryption(string textToDecrypt)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(_privateKey);

            byte[] byteArrayTextToDecrypt = Convert.FromBase64String(textToDecrypt);
            byte[] decryptedDataAsByteArray = rsa.Decrypt(byteArrayTextToDecrypt, true);
            string decryptedDataAsString = Encoding.UTF8.GetString(decryptedDataAsByteArray);

            return decryptedDataAsString;
        }
    }
}
