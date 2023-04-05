using System;
using System.Text;
using System.Security.Cryptography; // Aes
using System.Numerics; // BigInteger
using System.IO;

namespace _539_project_3
{
class Program
{
    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 256;
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 256;
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
static void Main(string[] args)
{
// Make sure you are familiar with the System.Numerics.BigInteger class and how to use some of the functions it has (Parse, Pow, ModPow, etc.)
    string[] strBytes = args[0].Split(" ");
    string[] decryptString = args[7].Split(" ");

    byte[] IVBytes = new byte[strBytes.Length];
    byte[] decryptBytes = new byte[decryptString.Length];
    for(var i = 0; i < strBytes.Length; i++)
    {
        IVBytes[i] = Convert.ToByte(strBytes[i],16);
    }

    for(var i = 0; i < decryptString.Length; i++)
    {
        // Console.WriteLine(decryptString[i]);
        decryptBytes[i] = Convert.ToByte(decryptString[i],16);
    }

    int  g_e = Int32.Parse(args[1]);
    BigInteger g_c = BigInteger.Parse(args[2]);
    int  N_e = Int32.Parse(args[3]);
    BigInteger N_c = BigInteger.Parse(args[4]);

    BigInteger g = BigInteger.Pow((BigInteger)2, g_e);
    g = BigInteger.Subtract(g, g_c);
    BigInteger N = BigInteger.Pow((BigInteger)2, N_e);
    N = BigInteger.Subtract(N, N_c);

    int x = Int32.Parse(args[5]);
    BigInteger gy_x = BigInteger.ModPow(BigInteger.Parse(args[6]), (BigInteger) x, N);

    string plainText = args[8];
    using (Aes myAes = Aes.Create())
    {
        string decrypted = DecryptStringFromBytes_Aes(decryptBytes, gy_x.ToByteArray(), IVBytes);
        byte[] encrypted = EncryptStringToBytes_Aes(plainText, gy_x.ToByteArray(), IVBytes);
        Console.Write(decrypted + "," + BitConverter.ToString(encrypted).Replace("-"," "));
        
    }


    // optional hint: for encryptiong/ decryption with AES, lookup the microsoft documentation on Aes (System.Security.

    /*
    dotnet run "A2 2D 93 61 7F DC 0D 8E C6 3E A7 74 51 1B 24 B2" 251 465 255 1311 2101864342 8995936589171851885163650660432521853327227178155593274584417851704581358902 "F2 2C 95 FC 6B 98 BE 40 AE AD 9C 07 20 3B B3 9F F8 2F 6D 2D 69 D6 5D 40 0A 75 45 80 45 F2 DE C8 6E C0 FF 33 A4 97 8A AF 4A CD 6E 50 86 AA 3E DF" AfYw7Z6RzU9ZaGUloPhH3QpfA1AXWxnCGAXAwk3f6MoTx
    */
    }
    }
}