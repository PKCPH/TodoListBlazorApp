using BCrypt.Net;
using System.Security.Cryptography;
using System.Text;

namespace TodoBlazorApp.Handlers;

public class HashingHandler
{
    public string SHA2Hashing(string txtToHash)
    {
        SHA256 sha256 = SHA256.Create();
        byte[] txtToHashAsByteArray = Encoding.ASCII.GetBytes(txtToHash);
        byte[] hashedValue = sha256.ComputeHash(txtToHashAsByteArray);
        return Convert.ToBase64String(hashedValue);
    }

    public string BCryptHashing(string txtToHash)
    {
        ////First example
        //return BCrypt.Net.BCrypt.HashPassword(txtToHash);

        ////Second example
        //int workFactor = 10;
        //bool enhancedEntropy = true;
        //return BCrypt.Net.BCrypt.HashPassword(txtToHash, workFactor, enhancedEntropy);

        //Third example
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        bool enhancedEntropy = true;
        HashType hashType = HashType.SHA256;

        return BCrypt.Net.BCrypt.HashPassword(txtToHash, salt, enhancedEntropy, hashType);

    }

    public bool BCryptHashingVerify(string txtToHash, string hashedValueAsString)
    {
        ////First example
        //return BCrypt.Net.BCrypt.Verify(txtToHash, hashedValueAsString);

        ////Second example
        //return BCrypt.Net.BCrypt.Verify(txtToHash, hashedValueAsString, true);

        //Third example
        return BCrypt.Net.BCrypt.Verify(txtToHash, hashedValueAsString, true, HashType.SHA256);
    }

}
