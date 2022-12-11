using System;
using System.Collections.Generic;

namespace WordGameOO
{
    public class WordFamilyHard
    {

        public List<WordFamily> wordFamilies = new List<WordFamily>();

        private List<string> family0 = new List<string>();
        private List<string> family1 = new List<string>();
        private List<string> family2 = new List<string>();
        private List<string> family3 = new List<string>();
        public int chosenFamily;
        public char[] emptyWord;



        string[] GetFamilyHard (char letter, string[] dictionary)
        {
            family1.Clear();
            family0.Clear();
            int steps = 0;
            for (int i = 0; i < dictionary.Length; i++)
            {
                foreach(byte ch in dictionary[i])
                {
                    if(letter == (byte)letter)
                    {
                        family1.Add(dictionary[i]);

                    }
                    else
                    {
                        family0.Add(dictionary[i]);
                    }
                }
            }

            if(family0.Count == 0)
            {
                return family1.ToArray();
            }
            else {
                return family0.ToArray();
            }
            
        }










        int minV(char letter)
        {
            int min = 0;






            return min;
        }

        int maxV()
        {
            int max = 0;




            return max;
        }
    }
}
