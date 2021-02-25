using System;
using System.IO;

namespace HelloMachineLearning
{
    class Program
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzåäö";
        private static readonly Random random = new Random();

        static void Main()
        {
            string lengthInput = "";
            string accuracyInput;
            int[,] combinationScore = FindPatterns();

            while (lengthInput != "q" && lengthInput != "Q")
            {
                Console.WriteLine("Enter Q to exit application\nLeave blank for random length (2-9 characters)");
                Console.Write("Enter desired length of generated name: ");
                lengthInput = Console.ReadLine();

                if (String.IsNullOrEmpty(lengthInput))
                    lengthInput = Convert.ToString(random.Next(2, 10));

                if (Int32.TryParse(lengthInput, out int num))
                {
                    Console.Write("Enter desired accuracy (default value is 500 if left blank): ");
                    accuracyInput = Console.ReadLine();

                    if (String.IsNullOrEmpty(accuracyInput))
                        accuracyInput = "500";

                    Console.WriteLine();

                    if (Int32.TryParse(accuracyInput, out int acc))
                        Console.WriteLine(GenerateNewName(num, acc, combinationScore));
                    else
                        Console.WriteLine("No valid input");

                    Console.ReadLine();
                }
            }
        }

        private static int[,] FindPatterns()
        {
            int[,] combinationScore = new int[Alphabet.Length, Alphabet.Length];
            string path = AppDomain.CurrentDomain.BaseDirectory + "/SwedishFirstNames";
            string[] names = File.ReadAllLines(path);

            foreach (string name in names)
            {
                for (int i = 1; i < name.Length; i++)
                {
                    if (Alphabet.Contains(Char.ToLower(name[i - 1])) && Alphabet.Contains(Char.ToLower(name[i])))
                        combinationScore[Alphabet.IndexOf(Char.ToLower(name[i - 1])), Alphabet.IndexOf(Char.ToLower(name[i]))]++;
                }
            }

            return combinationScore;
        }

        private static string GenerateNewName(int lengthOfName, int accuracy, int[,] combinationScore)
        {
            string newName = "";

            newName += Alphabet[random.Next(0, Alphabet.Length)];

            int start = Environment.TickCount;

            for (int i = 0; i < lengthOfName - 1; i++)
            {

                char temp = Alphabet[random.Next(0, Alphabet.Length)];

                if (combinationScore[Alphabet.IndexOf(newName[i]), Alphabet.IndexOf(temp)] > accuracy)
                    newName += temp;

                else
                    i--;

                if (Environment.TickCount - start > 1000)
                    break;
            }

            if (Environment.TickCount - start > 1000)
                return GenerateNewName(lengthOfName, accuracy, combinationScore);

            for (int i = 0; i < newName.Length - 2; i++)
            {
                if (newName[i] == newName[i + 1] && newName[i] == newName[i + 2])
                {
                    return GenerateNewName(lengthOfName, accuracy, combinationScore);
                }
            }

            if (lengthOfName > 1)
                newName = Char.ToUpper(newName[0]) + newName[1..];

            return newName;
        }
    }
}
