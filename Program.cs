using System;
using System.Text;
using System.Security.Cryptography; // Aes
using System.Numerics; // BigInteger
using System.IO;

namespace _539_project_3
{
class Program
{

static void Main(string[] args)
{
// Make sure you are familiar with the System.Numerics.BigInteger class and how to use some of the functions it has (Parse, Pow, ModPow, etc.)
byte[] IV = Encoding.ASCII.GetBytes(args[0]);
int  g_e = Int32.Parse(args[1]);
BigInteger g_c = BigInteger.Parse(args[2]);
int  N_e = Int32.Parse(args[3]);
BigInteger N_c = BigInteger.Parse(args[4]);

BigInteger g = BigInteger.Pow((BigInteger)2, g_e);
g = BigInteger.Subtract(g, g_c);
BigInteger N = BigInteger.Pow((BigInteger)2, N_e);
N = BigInteger.Subtract(N, N_c);

int x = Int32.Parse(args[5]);

BigInteger g_x = BigInteger.ModPow(g,(BigInteger)x,N);

Console.WriteLine(g);
Console.WriteLine(N);
Console.WriteLine(g_x);


// optional hint: for encryptiong/ decryption with AES, lookup the microsoft documentation on Aes (System.Security.

/*
dotnet run
"A2 2D 93 61 7F DC 0D 8E C6 3E A7 74 51 1B 24 B2" 251 465 255 1311 2101864342 8995936589171851885163650660432521853327227178155593274584 17851704581358902 "F2 2C 95 FC 6B 98 BE 40 AE AD 9C 07 20 3B B3 9F F8 2F 6D 2D 69 D6 5D 40 0A 75 45 80 45 F2 DE C8 6E C0 FF 33 A4 97 8A AF 4A CD 6E 50 86 AA 3E DF" AfYw7Z6RzU9ZaGUloPhH3QpfA1AXWxnCGAXAwk3f6MoTx
*/
}
}
}