using Microsoft.AspNetCore.DataProtection;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TodoBlazorApp.Handlers;

public class EncryptionHandler
{
    private string _privateKey;
    private string _publicKey;

    public EncryptionHandler()
    {   
        //Check if the private key exists
        if (!File.Exists("privateKey.pem"))
        {
            //Create new RSA Key pair
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                //Get keys in XML format
                _privateKey = rsa.ToXmlString(true);
                _publicKey = rsa.ToXmlString(false);

                //Saving keys
                File.WriteAllText("privateKey.pem", _privateKey);
                File.WriteAllText("publicKey.pem", _publicKey);
            }
        }
        else 
        { 
            //loading keys
            _privateKey = File.ReadAllText("privateKey.pem"); 
            _publicKey = File.ReadAllText("publicKey.pem");
        }

    }

    public string AsymmetricEncryption(string textToEncrypt)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            //loading public key
            rsa.FromXmlString(_publicKey);

            //converting text to byte array
            byte[] byteArrayTextToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
            //encrypt byte array with RSA
            byte[] encryptedDataAsByteArray = rsa.Encrypt(byteArrayTextToEncrypt, true);
            //convert encrypted byte array to base64 string
            var encryptedDataAsString = Convert.ToBase64String(encryptedDataAsByteArray);

            return encryptedDataAsString;
        }
    }

    public string AsymmetricDecryption(string textToDecrypt)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            //load private key
            rsa.FromXmlString(_privateKey);

            //convert base64 text to byte array
            byte[] byteArrayTextToDecrypt = Convert.FromBase64String(textToDecrypt);
            //decrypt byte array with RSA
            byte[] decryptedDataAsByteArray = rsa.Decrypt(byteArrayTextToDecrypt, true);
            // convert decrypted byte array to string
            string decryptedDataAsString = Encoding.UTF8.GetString(decryptedDataAsByteArray);

            return decryptedDataAsString;
        }
    }
}
