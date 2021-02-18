using System;
using Microsoft.ML;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace HelloMachineLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            //string s = $"{GenerateNewName(3, 60)}-{GenerateNewName(5, 60)}";

            Console.WriteLine(GenerateNewName(5, 300));

            Console.ReadLine();
        }

        private static string GenerateNewName(int lengthOfName, int accuracy)
        {

            string alphabet = "abcdefghijklmnopqrstuvwxyzåäö";
            int[,] combinationScore = new int[alphabet.Length, alphabet.Length];
            string path = AppDomain.CurrentDomain.BaseDirectory + "/SwedishFirstNames";
            var random = new Random();
            string newName = "";

            string[] names = File.ReadAllLines(path);



            foreach (string name in names)
            {
                for (int i = 1; i < name.Length; i++)
                {
                    if (alphabet.Contains(name[i - 1]) && alphabet.Contains(name[i]))
                        combinationScore[alphabet.IndexOf(name[i - 1]), alphabet.IndexOf(name[i])]++;
                }
            }

            newName += alphabet[random.Next(0, alphabet.Length)];

            int start = Environment.TickCount;

            for (int i = 0; i < lengthOfName - 1; i++)
            {

                char temp = alphabet[random.Next(0, alphabet.Length)];

                if (combinationScore[alphabet.IndexOf(newName[i]), alphabet.IndexOf(temp)] > accuracy)
                    newName += temp;

                else
                    i--;

                if (Environment.TickCount - start > 1000)
                    break;
            }

            if (Environment.TickCount - start > 1000)
                return GenerateNewName(lengthOfName, accuracy);

            return newName;
        }
    }
}