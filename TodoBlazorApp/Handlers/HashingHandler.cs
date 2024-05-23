using BCrypt.Net;
using System.Security.Cryptography;
using System.Text;

namespace TodoBlazorApp.Handlers;

public class HashingHandler
{

    public string SHA2Hashing(string txtToHash)
    {
        SHA256 sha256 = SHA256.Create();
        //Convert text to byte array
        byte[] txtToHashAsByteArray = Encoding.ASCII.GetBytes(txtToHash);
        //Compute hash value to byte array
        byte[] hashedValue = sha256.ComputeHash(txtToHashAsByteArray);
        //convert hashed byte array to base64 string and return
        return Convert.ToBase64String(hashedValue);
    }

    public string BCryptHashing(string txtToHash)
    {
        //generate salt
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        bool enhancedEntropy = true;
        HashType hashType = HashType.SHA256;

        //hasing text with Bcrypt with salt and hash type
        return BCrypt.Net.BCrypt.HashPassword(txtToHash, salt, enhancedEntropy, hashType);
    }

    //verifying text against the Bcrypt hash using SHA-256
    public bool BCryptHashingVerify(string txtToHash, string hashedValueAsString)
    {
        return BCrypt.Net.BCrypt.Verify(txtToHash, hashedValueAsString, true, HashType.SHA256);
    }

}
