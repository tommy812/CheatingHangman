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
         
        public static int nGuess,lifes,nWords,nLetters;
        public static IList words = System.IO.File.ReadLines("dictionary.txt").ToList();


        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            //for the basic difficulty:
            //generate a random n between 0 and words.count
            nWords = words.Count;
            Random ran = new Random();
            int word = ran.Next(nWords);
            Console.WriteLine(words[word]);
            string s = words[word].ToString();
            char[] wordArray = s.ToCharArray();



        }
    }
}
