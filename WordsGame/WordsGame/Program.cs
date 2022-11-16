using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsGame
{
    class MainClass
    { 
        public const int EASY = 0, HARD =1,NOCHEAT =2 ,FALSE = 0, TRUE =1;
        
        public static int nGuess,lifes,nWords,nLetters, end,wordPosition,wordLenght, exit;

        public static Char[] wordArray, emptyWord;

        public static string input,word;

        public static Random ran = new Random();

        public static string[] words = System.IO.File.ReadLines("dictionary.txt").ToArray();

        public static ArrayList letterUsed = new ArrayList(), lettersFound = new ArrayList();

        //Menu 
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello There");
            
            exit = FALSE;
            do
            {
                Console.WriteLine("Choose game mode: \n0 = Easy; 1 = Hard!;\n Any other number to exit.");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case EASY:
                        EasyMode();
                        break;
                    case HARD:
                        //HardMode();
                        break;
                    case NOCHEAT:
                        EasyModeNotCheat();
                        break;
                    default:
                        Console.WriteLine("Thanks For Playing! =)");
                        exit = TRUE;
                        break;
                }
                
            } while (exit == FALSE);
        }

        public static void EasyMode()
        {
            end = 0;


            //for the basic difficulty:
            //find all words of a random lenghts
            wordLenght = ran.Next(4,12);

            lifes = wordLenght * 2;
            string[] wordFamily = Array.FindAll(words, element => element.Length == wordLenght);

            //TODELETE
            Console.Write($"{ string.Join(", ", wordFamily)}");

            //print lifes
            Console.WriteLine("lifes available = " + lifes);

            //create dashed word
            emptyWord = Enumerable.Repeat('_', wordLenght).ToArray();
            Boolean exists;
            do
            {
                //print the empty words.
                Console.Write($"{ string.Join(" ", emptyWord)}");

                //ask for a letter
               
                int check = FALSE;
                do
                {
                    Console.WriteLine("\ninsert a new letter:");
                    input = Console.ReadLine();
                    if(input.Length != 1)
                    {
                        check = TRUE;
                    }
                    else
                    {
                        check = FALSE;
                    }
                } while(check == TRUE);
                



                letterUsed.Add(input);
                int found = TRUE;


                GetFamilyWord(wordFamily, input,ref found,ref wordFamily, ref wordPosition);

                if(found == TRUE)
                {
                    lettersFound.Add(input);
                    emptyWord[wordPosition] = char.Parse(input);
                }
                //Console.Write($"{ string.Join(", ", family)}");
                Console.WriteLine("letter is found "+found+", letter: "+input+"position"+wordPosition);
              
                lifes--;

            }while (wordFamily.Length >1);
        }

        public static void GetFamilyWord(string[] array1, string usedLetter, ref int found, ref string[] wordFamily, ref int wordPosition)
        {
            
            //words with 0 occurance 
            ArrayList family = new ArrayList();
            //words with one occurance 
            ArrayList family1 = new ArrayList();
            //words with two occurances
            ArrayList family2 = new ArrayList();
            //words with 3 or more occurances
            ArrayList family3 = new ArrayList();

            //for each word

            //identify words containing letter
            //devide between words with 1 letter two letters or more
            //choose family with more elements

            //for each word
            for (int i = 0; i < array1.Count(); i++)
            {
                int count = 0;
                //for each letter

                char letter = char.Parse(usedLetter.ToString());
                    
                //compare each char
                foreach (var ch in array1[i])
                {
                    if (ch == letter)
                    {
                        count++;
                        Console.WriteLine(array1[i]);
                    }
                }
                
                
                if (count == 0)
                {
                    family.Add(array1[i]);
                }
                else if (count == 1)
                {
                    family1.Add(array1[i]);
                }
                else if (count == 2)
                {
                    family2.Add(array1[i]);
                }
                else if (count > 3)
                {
                    family3.Add(array1[i]);
                }
                

                
            }


            //family word with largest n of words is returned
            //letter not found
            //should find family where is at same position
            if (family.Count > family1.Count && family.Count > family2.Count && family.Count > family3.Count)
            {
                found = FALSE;
               //Console.Write($"Family0:{ string.Join(", ", (string[])family.ToArray(typeof(string)))}");
               wordFamily = (string[])family.ToArray(typeof(string));
            }
            //letter found
            else if (family1.Count > family.Count && family1.Count > family2.Count && family1.Count > family3.Count)
            {
                found = TRUE;
                //Console.Write($" Family1: { string.Join(", ", (string[])family1.ToArray(typeof(string)))}");
                wordPosition = GetLetterPosition(family1,usedLetter);
                wordFamily = (string[])family1.ToArray(typeof(string));
            }
            else if (family2.Count > family.Count && family2.Count > family1.Count && family2.Count > family3.Count)
            {
                found = TRUE;
                //Console.Write($"Family2:{ string.Join(", ", (string[])family2.ToArray(typeof(string)))}");
                wordPosition = GetLetterPosition(family2, usedLetter);
                wordFamily = (string[])family2.ToArray(typeof(string));
            }
            else
            {
                found = TRUE;
                //Console.Write($"Family3:{ string.Join(", ", (string[])family3.ToArray(typeof(string)))}");
                wordPosition = GetLetterPosition(family3, usedLetter);

                wordFamily = (string[])family3.ToArray(typeof(string));
            }
            
        }

        public static int GetLetterPosition(ArrayList family,string usedLetter)
        {
            //how many times the letter is found in the same position
            ArrayList positions = new ArrayList();
            for (int i = 0; i < family.Count; i++)
            {
                char letter = char.Parse(usedLetter.ToString());
               
                for (int counter = 0; counter < family[i].ToString().Length; counter++)
                {
                    if (family.ToString()[counter] == letter)
                    {
                        positions.Add(counter);
                    }
                }

            }
            int count = 1, tempCount;


            int frequentNumber = (int)positions[0];
            for (int i = 0; i < positions.Count; i++)
            {
                int tempNumber = (int)positions[i];
                tempCount = 0;
                for (int j = 0; j < positions.Count; j++)
                {
                    if (tempNumber == (int)positions[j])
                    {
                        tempCount++;
                    }
                }
                if (tempCount > count)
                {
                    frequentNumber = tempNumber;
                    count = tempCount;
                }
            }

            int position = frequentNumber;
            return position;
        }




        //easy game algorithm
        public static void EasyModeNotCheat()
        {
            end = 0;


            //for the basic difficulty:
            //generate a random n between 0 and words.count
            //nWords = words.Count;
            wordPosition = ran.Next(0,words.Length);
            Console.WriteLine(words[wordPosition]);

            //get and print lifes
            wordLenght = words[wordPosition].ToString().Length;
            lifes = wordLenght * 2;
            Console.WriteLine("lifes available = " + lifes);


            //get the choosen word
            word = words[wordPosition].ToString();
            wordArray = word.ToCharArray();
            emptyWord = Enumerable.Repeat('_', wordLenght).ToArray();

            //print the empty words.
            Console.Write($"{ string.Join("", emptyWord)}");


            for (int j = 0; j <= lifes; j++)
            {
                Console.WriteLine("\ninsert a new letter:");
                input = Console.ReadLine();

                for (int i = 0; i < wordLenght; i++)
                {

                    if (input == wordArray[i].ToString())
                    {
                        emptyWord[i] = char.Parse(wordArray[i].ToString());
                    }
                }
                Console.Write($" { string.Join(" ", emptyWord)}");

                if (emptyWord.Contains('_') == false)
                {
                    Console.WriteLine("\nCongratulations");
                    j = lifes + 1;
                }
                if (j == lifes)
                {
                    Console.WriteLine("unfortunaly you lost");
                }
            }
        }


        public static void minimax()
        {

        }


    }
}
