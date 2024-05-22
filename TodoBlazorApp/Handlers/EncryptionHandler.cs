using Microsoft.AspNetCore.DataProtection;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace TodoBlazorApp.Handlers;

public class EncryptionHandler
{

    private readonly IDataProtector _dataProtector;
    private string _privateKey;
    private string _publicKey;


    public EncryptionHandler(IDataProtectionProvider dataProtector)
    {
        _dataProtector = dataProtector.CreateProtector("NielsErVoresFavoritLærer");

        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        _privateKey = rsa.ToXmlString(true);
        _publicKey = rsa.ToXmlString(false);
    }

    public string SymmetricEncryption(string txtToEncrypt)
    {
        return _dataProtector.Protect(txtToEncrypt);
    }
    public string SymmetricDecryption(string txtToEncrypt)
    {
        return _dataProtector.Unprotect(txtToEncrypt);
    }

    public static string AsymmetricEncryption(string textToEncrypt, string publicKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);

            byte[] byteArrayTextToEncrypt = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
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
            string decryptedDataAsString = System.Text.Encoding.UTF8.GetString(decryptedDataAsByteArray);

            return decryptedDataAsString;
        }
    }
}
