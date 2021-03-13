// https://www.codingame.com/playgrounds/11117/simple-encryption-using-c-and-xor-technique
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTasks
{
    class Program
    {
        // this is the private key to translate the message
        private const int KEY = 9;

        static void Main(string[] args)
        {
            string msg;

            Console.WriteLine("Simple Encryption using C# and XOR technique\n\nEnter your message\n\n");
            msg = Console.ReadLine();

            // encrypt
            Console.Write($"Encrypting:\n{msg}\n\n");
            string decMsg = EncryptDecrypt(msg, KEY);

            // decrypt
            Console.Write($"Decrypting:\n{decMsg}\n\n");

            string res = EncryptDecrypt(decMsg, KEY);
            Console.Write($"Result:\n{res}\n\n");

            Console.WriteLine("Done...");
            Console.ReadKey();
        }

        /// <summary>
        /// Encrypt/Decrypt a string given the shared key. Performs an XOR operation to the plain text to encrypt it.
        /// </summary>
        /// <param name="plainText">The message to encrypt/decrypt.</param>
        /// <param name="encryptionKey">The key.</param>
        /// <returns></returns>
        static string EncryptDecrypt(string plainText, int encryptionKey)
        {
            StringBuilder inputSb = new StringBuilder(plainText);
            StringBuilder outputSb = new StringBuilder(plainText.Length);
            char textCh;

            for (int i = 0; i < plainText.Length; ++i)
            {
                // iterate through message
                textCh = inputSb[i];

                // set current char to char XOR encryptionKey and append to output
                textCh = (char)(textCh ^ encryptionKey);
                outputSb.Append(textCh);
            }

            return outputSb.ToString();
        }
    }
}
