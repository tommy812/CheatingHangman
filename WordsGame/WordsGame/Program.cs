using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsGame
{
    class MainClass
    { 
        public const int EASY = 0, HARD = 1;
         
        public static int nGuess,lifes,nWords,nLetters, end;

        public static string input;

        public static IList words = System.IO.File.ReadLines("dictionary.txt").ToList();


        public static void Main(string[] args)
        {
            end = 0;
            Console.WriteLine("Hello World!");
            
            //for the basic difficulty:
            //generate a random n between 0 and words.count
            nWords = words.Count;
            Random ran = new Random();
            int word = ran.Next(nWords);
            Console.WriteLine(words[word]);

            int wordLenght = words[word].ToString().Length;
            int lifes = wordLenght * 2;
            Console.WriteLine("lifes available = "+lifes);

            string s = words[word].ToString();

            char[] wordArray = s.ToCharArray();

            char[] emptyWord = Enumerable.Repeat('_', wordLenght).ToArray();


            Console.Write($"{ string.Join("", emptyWord)}");

            
            
            for (int j=0;j <= lifes; j++) { 
                Console.WriteLine("insert a new letter:");
                input = Console.ReadLine();
                
                for (int i=0; i < wordLenght; i++) { 
                    
                    if (input == wordArray[i].ToString())
                    {
                        emptyWord[i] = char.Parse(wordArray[i].ToString());
                    }
                }
                Console.Write($" { string.Join(" ", emptyWord)}");

                if (emptyWord.Contains('_')==false) {
                    Console.WriteLine("\nCongratulations");
                    j = lifes+1;
                }
                if (j == lifes)
                {
                    Console.WriteLine("unfortunaly you lost");
                }
            }
        }
    }
}
