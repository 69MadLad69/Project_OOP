using System.Text;
using System.Security.Cryptography;
namespace Project_OOP;
public class Hash
{
    byte[] _bytedPassword;
    byte[] _hashedPassword;

    public string HashPassword(string password)
    {
        _bytedPassword = Encoding.ASCII.GetBytes(password);
        _hashedPassword = new MD5CryptoServiceProvider().ComputeHash(_bytedPassword);
        return ByteArrayToString(_hashedPassword);
    }
    
    static string ByteArrayToString(byte[] input)
    {
        int i;
        StringBuilder output = new StringBuilder(input.Length);
        for (i=0;i < input.Length; i++)
        {
            output.Append(input[i].ToString("X2"));
        }
        return output.ToString();
    }
}