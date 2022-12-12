using System;

namespace WordGameOO
{
    class Program
    {
        public const int EASY = 0, HARD = 1, FALSE = 0, TRUE = 1;
        public static int wordLenght,lifes,exit, choice;
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello There");

            exit = FALSE;
            do
            {
                Console.Clear();
                Console.WriteLine("Hello There");
                Console.WriteLine("Choose game mode: \n0 = Easy; 1 = Hard!;\n Any other number to exit.");
                int valid = 0;
                string temp;
                do
                {
                    temp = Console.ReadLine();
                    
                    int.TryParse(temp, out valid);
                    if (temp == "0") valid = 1;
                    if (valid == 0) Print("Character Not Valid\nPlease try again!");

                } while (valid == 0);
                choice = Convert.ToInt32(temp);

                switch (choice)
                {
                    case EASY:
                        Console.Clear();
                        EasyMode();
                        exit = FALSE;
                        break;
                    case HARD:
                        Console.Clear();
                        HardMode();
                        exit = FALSE;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Thanks For Playing! =)");
                        Console.Read();
                        exit = TRUE;
                        break;
                }

            } while (exit == FALSE);
        }

        private static void HardMode()
        {
            
            exit = FALSE;
            WordFamily families = new WordFamily();
     
            
            Letter letter = new Letter();
            families.Clear(families);
            wordLenght = families.GetLenght();
            string[] dictionary = families.GetWords(wordLenght);
            Console.WriteLine("Words: {0}", dictionary.Length);
            
            int count = 0;

            lifes = GetLifes(wordLenght);
            char input;

            families.emptyWord = families.GetEmptyWord(wordLenght, families);

            do
            {
                
                do
                {// repeat until input is valid.
                    input = letter.GetLetter();
                } while (input == ' ');
                lifes--;

                dictionary = families.GetFamilyHard(input,dictionary,families);
                Print("\n\nLifes left: " + lifes.ToString());
                letter.PrintUsedLetters();
                PrintEmptyWord(families.emptyWord);

                if (lifes == 0)
                {
                    Console.Clear();
                    Console.WriteLine("\nUnfortunately you run out of lifes!\n\nThe correct word was: {0}\nBetter Luck next time =)\n", dictionary[0]);
                    Console.Read();
                    exit = TRUE;

                }
                else if (!(new String(families.emptyWord).Contains("_")))
                {

                    Console.Clear();
                    Print("Congratulations! you guessed the word correctly!");
                    PrintEmptyWord(families.emptyWord);
                    Console.Read();
                    exit = TRUE;
                }
            } while (exit == FALSE);

        }

        private static void EasyMode()
        {
            exit = FALSE;
            WordFamily families = new WordFamily();
            families.wordFamilies.Clear();
            Letter letter = new Letter();
            wordLenght = families.GetLenght();
            string[] dictionary = families.GetWords(wordLenght);

            int count = 0;

            lifes = GetLifes(wordLenght);
            char input;
            families.emptyWord = families.GetEmptyWord(wordLenght,families);

            Print("Lifes left: " + lifes.ToString());
            letter.PrintUsedLetters();
            PrintEmptyWord(families.emptyWord);
            do
            {// repeat until input is valid.
                input = letter.GetLetter();
            } while (input == ' ');
            lifes--;
            families.GetWordFamilies(input, dictionary,families);
            dictionary = families.GetLargestFamily();
            if((int)families.wordFamilies[0].chosenFamily != 0)
            {
                dictionary = families.GetLetterPosition(dictionary, input, families.wordFamilies[0].chosenFamily,families.wordFamilies[0]);
            }
            
             

            do
            {
                
                Print("\n\nLifes left: " + lifes.ToString());
                letter.PrintUsedLetters();
                PrintEmptyWord(families.emptyWord);
                do
                {// repeat until input is valid.
                    input = letter.GetLetter();
                } while (input == ' ');
                lifes--;
                families.GetWordFamilies(input, dictionary, families);
                dictionary = families.GetLargestFamily();
                //show the word if is the lst one testing
                //if (dictionary.Length == 1) Console.WriteLine("Word: {0}", dictionary[0]);
                if ((int)families.wordFamilies[0].chosenFamily != 0)

                {
                    dictionary = families.GetLetterPosition(dictionary, input, families.wordFamilies[0].chosenFamily, families.wordFamilies[0]);
                }

                if (lifes == 0 )
                {
                    Console.Clear();
                    Console.WriteLine("\nUnfortunately you run out of lifes!\n\nThe correct word was: {0}\nBetter Luck next time =)\n", dictionary[0]);
                    Console.Read();
                    exit = TRUE;

                }
                else if (!(new String(families.emptyWord).Contains("_")))
                {

                    Console.Clear();
                    Print("Congratulations! you guessed the word correctly!");
                    PrintEmptyWord(families.emptyWord);
                    Console.Read();
                    exit = TRUE;
                }

            } while (exit == FALSE);
        }




        public static int GetLifes(int lenght)
        {
            int lifes = lenght * 2;
            return lifes;
        }

        public static void Print(string value)
        {
            Console.WriteLine(value);
        }
        public static void PrintEmptyWord(char[] emptyWord)
        {
            Console.Write($"{ string.Join(" ", emptyWord)}");
            Console.Write("\n");
        }
    }

    

    
}
