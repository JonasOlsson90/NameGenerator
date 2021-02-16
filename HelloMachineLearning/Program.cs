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
            int lengthOfName = 5; //Enter desired length of your new name
            int accuracy = 60; //Enter desired accuract of letter selection

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

            for (int i = 0; i < lengthOfName; i++)
            {

                char temp = alphabet[random.Next(0, alphabet.Length)];

                if (combinationScore[alphabet.IndexOf(newName[i]), alphabet.IndexOf(temp)] > accuracy)
                    newName += temp;

                else
                    i--;
            }



            Console.WriteLine(newName);

            Console.ReadLine();
        }
    }
}