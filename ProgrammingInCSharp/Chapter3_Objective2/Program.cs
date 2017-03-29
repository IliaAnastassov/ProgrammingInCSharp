namespace Chapter3_Objective2
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Permissions;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
        }

        public static void ConvertToUnsecureString(SecureString securePassword)
        {
            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                Console.WriteLine(Marshal.PtrToStringUni(unmanagedString));
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        private static void GenerateSecureString()
        {
            using (var ss = new SecureString())
            {
                Console.WriteLine("Enter password");

                while (true)
                {
                    var cki = Console.ReadKey(true);

                    if (cki.Key == ConsoleKey.Enter)
                    {
                        break;
                    }

                    ss.AppendChar(cki.KeyChar);
                    Console.Write("*");
                }

                ss.MakeReadOnly();
            }
        }

        private static void ImperativeCAS()
        {
            var permission = new FileIOPermission(PermissionState.None);
            permission.AllLocalFiles = FileIOPermissionAccess.Read;

            try
            {
                permission.Demand();
            }
            catch (SecurityException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [FileIOPermission(SecurityAction.Demand, AllLocalFiles = FileIOPermissionAccess.Read)]
        private void DeclarativeCAS()
        {
            // Implementation
        }

        private static void SignAndVerify()
        {
            var textToSign = "My signed text";
            var signature = Sign(textToSign, "cn=IliaAnastassov");
            Console.WriteLine(Verify(textToSign, signature));
        }

        private static bool Verify(string text, byte[] signature)
        {
            var cert = GetSertificate();
            var csp = (RSACryptoServiceProvider)cert.PublicKey.Key;
            var hash = HashData(text);
            return csp.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), signature);
        }

        private static byte[] Sign(string text, string certSubject)
        {
            var cert = GetSertificate();
            var csp = (RSACryptoServiceProvider)cert.PrivateKey;
            var hash = HashData(text);
            return csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
        }

        private static byte[] HashData(string text)
        {
            var hashAlgorithm = new SHA1Managed();
            var encoding = new UnicodeEncoding();
            var data = encoding.GetBytes(text);
            return hashAlgorithm.ComputeHash(data);
        }

        private static X509Certificate2 GetSertificate()
        {
            var store = new X509Store("TestCertStore", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            return store.Certificates[0];
        }

        private static void UseSHA256()
        {
            var byteConverter = new UnicodeEncoding();
            var sha256 = SHA256.Create();

            var data = "My original text";
            var hashA = sha256.ComputeHash(byteConverter.GetBytes(data));

            data = "My changed text";
            var hashB = sha256.ComputeHash(byteConverter.GetBytes(data));

            data = "My original text";
            var hashC = sha256.ComputeHash(byteConverter.GetBytes(data));

            Console.WriteLine(hashA.SequenceEqual(hashB));
            Console.WriteLine(hashA.SequenceEqual(hashC));
        }

        private static void EnctyptAndDecryptDataAsync()
        {
            var keyProvider = new RSACryptoServiceProvider();
            var publicKeyXML = keyProvider.ToXmlString(false);
            var privateKeyXML = keyProvider.ToXmlString(true);

            var byteConverter = new UnicodeEncoding();
            var dataToEncrypt = byteConverter.GetBytes("Fuck Islam!");

            byte[] encryptedData;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKeyXML);
                encryptedData = rsa.Encrypt(dataToEncrypt, false);
            }

            byte[] decryptedData;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKeyXML);
                decryptedData = rsa.Decrypt(encryptedData, false);
            }

            var decryptedText = byteConverter.GetString(decryptedData);
            Console.WriteLine(decryptedText);
        }

        private static void UsePublicAndPrivateKeys()
        {
            var rsa = new RSACryptoServiceProvider();
            var publicKeyXML = rsa.ToXmlString(false);
            var privateKeyXML = rsa.ToXmlString(true);

            Console.WriteLine(publicKeyXML);
            Console.WriteLine(privateKeyXML);
        }
    }
}
