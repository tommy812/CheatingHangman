using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static WordGameOO.Letter;

namespace WordGameOO
{
    public class WordFamily
    {
        public List<WordFamily> wordFamilies = new List<WordFamily>();

        private List<string> family0 = new List<string>();
        private List<string> family1 = new List<string>();
        private List<string> family2 = new List<string>();
        private List<string> family3 = new List<string>();
        public int chosenFamily;
        public char[] emptyWord;


       

        public string[] GetWords(int length)
        {
            
            string[] words = System.IO.File.ReadLines("dictionary.txt").ToArray();

            words = Array.FindAll(words, element => element.Length == length);

            return words;
        }

        public int GetLenght()
        {
            Random ran = new Random();
            int lenght = ran.Next(4, 12);
            return lenght;
        }

        public char[] GetEmptyWord(int lenght,WordFamily families)
        {
            char[] emptyWord = Enumerable.Repeat('_', lenght).ToArray();
            families.emptyWord = emptyWord;
            return families.emptyWord;
        }

        public void GetWordFamilies(char letterUsed,string[] dictionary,WordFamily families)
        {
            //for each word

            //identify words containing letter
            //devide between words with 1 letter two letters or more
            //choose family with more elements

            //for each word
            Clear(families);

            for (int i = 0; i < dictionary.Count(); i++)
            {
                int count = 0;
                
                //compare each char
                foreach (byte ch in dictionary[i])
                {
                    //ch()eck how many times the letter is reapeated in a word
                    if (ch == (byte)letterUsed)
                    {

                        count++;
                        //Console.WriteLine(array1[i]);
                    }
                }

                //if the letter is not repeated
                if (count == 0)
                {
                    families.family0.Add(dictionary[i]);
                }//if the letter is repeated once
                else if (count == 1)
                {
                    families.family1.Add(dictionary[i]);
                }//if the letter is repeated twice 
                else if (count == 2)
                {
                    families.family2.Add(dictionary[i]);
                }//if the letter is repeated 3 or more times.
                else if (count == 3)
                {
                    families.family3.Add(dictionary[i]);
                }
            }



            wordFamilies.Insert(0,families);

            
            
        }

        internal void Clear(WordFamily family)
        {
            family.wordFamilies.Clear();
            family.family0.Clear();
            family.family1.Clear();
            family.family2.Clear();
            family.family3.Clear();
        }

        internal string[] GetLargestFamily()
        {
            //family word with largest n of words is returned
            //letter not found
            //should find family where is at same position

            int n0 = wordFamilies[0].family0.Count;
            int n1 = wordFamilies[0].family1.Count;
            int n2 = wordFamilies[0].family2.Count;
            int n3 = wordFamilies[0].family3.Count;
            int tot = n1 + n2 + n3 + n0;

            Console.WriteLine("family 0: {0}, family1: {1}, family2: {2}, family3: {3}, tot: {4}.", n0, n1, n2, n3, tot);

            if (wordFamilies[0].family0.Count >= wordFamilies[0].family1.Count && wordFamilies[0].family0.Count >= wordFamilies[0].family2.Count && wordFamilies[0].family0.Count >= wordFamilies[0].family3.Count)
            {
                //wordFamilies[0].family0.ForEach(Console.WriteLine);
                Program.Print("Family: 0");
                wordFamilies[0].chosenFamily = 0;
                
            } 
            //letter found
            else if (wordFamilies[0].family1.Count >= wordFamilies[0].family0.Count && wordFamilies[0].family1.Count >= wordFamilies[0].family2.Count && wordFamilies[0].family1.Count >= wordFamilies[0].family3.Count)
            {
                Program.Print("Family: 1");
                wordFamilies[0].chosenFamily = 1;
                
            }
            else if (wordFamilies[0].family2.Count >= wordFamilies[0].family0.Count && wordFamilies[0].family2.Count >= wordFamilies[0].family1.Count && wordFamilies[0].family2.Count >= wordFamilies[0].family3.Count)
            {
                Program.Print("Family: 2");
                wordFamilies[0].chosenFamily = 2;
            }
            else if (wordFamilies[0].family3.Count >= wordFamilies[0].family0.Count && wordFamilies[0].family3.Count >= wordFamilies[0].family1.Count && wordFamilies[0].family3.Count >= wordFamilies[0].family2.Count)
            {
                Program.Print("Family: 3");
                wordFamilies[0].chosenFamily = 3;
            }
            switch (wordFamilies[0].chosenFamily)
            {
                case 0:

                    return wordFamilies[0].family0.ToArray();
                    
                case 1:
                    return wordFamilies[0].family1.ToArray();
                    
                case 2:
                    return wordFamilies[0].family2.ToArray();
                    
                default:
                    return wordFamilies[0].family3.ToArray();
                   
            }
        }

        internal string[] GetLetterPosition(string[] dictionary, char letterUsed, int familyNumber, WordFamily families)

        {
            if(familyNumber == 1)
            {
                ArrayList positions = new ArrayList();
                List<string> newDict = new List<string>();



                char letter = char.Parse(letterUsed.ToString());

                //save letters positions and word index 
                for (int i = 0; i < dictionary.Length; i++)
                {
                    int counter = 0;
                    foreach(byte ch in dictionary[i])
                    {
                        if (ch == (int)letterUsed)
                        {
                            positions.Add(counter);
                        }
                        counter++;
                    }
                }
                //get most reccuring position
                positions.Sort();
                int letterPosition = (int)positions[0];
                char[] emptyWord = families.emptyWord;
                emptyWord[letterPosition] = letterUsed;
                wordFamilies[0].emptyWord = emptyWord;

                //return dictionary with only letters at position
                for (int i = 0; i < dictionary.Length; i++)
                {
                    
                    foreach (byte ch in dictionary[i])
                    {
                        if (ch == (int)letterUsed )
                        {
                            if(dictionary[i].IndexOf((char)ch) == letterPosition)
                            {
                                newDict.Add(dictionary[i]);
                            }
                        }
                        
                    }
                }
                return newDict.ToArray();
            }
            else if (familyNumber == 2)
            {

                List<int> position1 = new List<int>();
                List<int> position2 = new List<int>();
                
                List<string> newDict = new List<string>();


                //save first appearence on position1
                //save second appearence on position2 for every word
                //save word index in position word index1 and wordindex2 
                for (int word = 0; word < dictionary.Length; word++)
                {
                    int letterIndex = 0;
                    bool found = false;
                    bool found2 = false;

                    foreach (byte ch in dictionary[word])
                    {
                        
                        if (ch == (int)letterUsed && found == false)
                        {
                            position1.Add(letterIndex);
                            
                            
                            found = true;

                        }else if (ch == (int)letterUsed && found == true)
                        {

                            position2.Add(letterIndex);
                            found2 = true;
                            
                        }
                        letterIndex++;
                    }
                    if (found2 == false)
                    {
                        position1.RemoveAt(position1.Count);
                    }

                }


                int appereance;
                int maxIndex = 0;
                int letterPosition1 = 0;
                int letterPosition2 = 0;

                for (int i = 0; i < position1.Count; i++)
                {
                    appereance = 0;

                    for (int j=0; j < position1.Count; j++)
                    {
                        //compare each couple to the remaining items
                        if(position1[i] == position1[j] && position2[i] == position2[j])
                        {
                            
                            appereance++;
                            
                        }
                    }
                    if (maxIndex < appereance)
                    {
                        maxIndex = appereance;
                        letterPosition1 = position1[i];
                        letterPosition2 = position2[i];
                    }

                }// letterpositions 1 and 2 are the most common positions

                
                //return dictionary with only letters at position
                for (int i = 0; i < dictionary.Length; i++)
                {
                    bool firstFound = false;
                    foreach (byte ch in dictionary[i])
                    {
                        
                        if (ch == letterUsed)
                        {
                            if (firstFound == false && dictionary[i].IndexOf((char)ch) == letterPosition1)
                            {
                                firstFound = true;
                                //Console.WriteLine("1 found");
                            }else if (firstFound == true && dictionary[i].IndexOf((char)ch,letterPosition1+1) == letterPosition2)
                            {
                                //Console.WriteLine("2 found");
                                firstFound = false;
                                newDict.Add(dictionary[i]);
                            }
                        }

                    }
                }
                char[] emptyWord = families.emptyWord;
                emptyWord[letterPosition1] = letterUsed;
                emptyWord[letterPosition2] = letterUsed;
                wordFamilies[0].emptyWord = emptyWord;
                return newDict.ToArray();
            }
            else if (familyNumber == 3)
            {

                List<int> position1 = new List<int>();
                List<int> position2 = new List<int>();
                List<int> position3 = new List<int>();
            
                List<string> newDict = new List<string>();


                //save first appearence on position1
                //save second appearence on position2 for every word
                //save word index in position word index1 and wordindex2 
                for (int i = 0; i < dictionary.Length; i++)
                {
                    int letterIndex = 0;
                    bool found = false;
                    bool found2 = false;

                    foreach (byte ch in dictionary[i])
                    {

                        if (ch == (int)letterUsed && found == false)
                        {
                            position1.Add(letterIndex);
                           

                            found = true;

                        }
                        else if (ch == (int)letterUsed && found == true)
                        {
                            position2.Add(letterIndex);
                            found2 = true;
                            
                        }
                        else if (ch == (int)letterUsed && found2 == true)
                        {
                            position3.Add(letterIndex);

                        }
                        letterIndex++;
                    }
                }


                int appereance = 0;
                int maxIndex = 0;
                int letterPosition1 = 0;
                int letterPosition2 = 0;
                int letterPosition3 = 0;

                for (int i = 0; i < position1.Count; i++)
                {
                    appereance = 0;

                    for (int j = 1; j < position1.Count - 1; j++)
                    {
                        //compare each couple to the remaining items
                        if (position1[i] == position1[j] && position2[i] == position2[j] && position3[i] == position3[j])
                        {

                            appereance++;
                        }
                    }
                    if (maxIndex < appereance)
                    {
                        maxIndex = appereance;
                        letterPosition1 = position1[i];
                        letterPosition2 = position2[i];
                        letterPosition3 = position3[i];
                    }

                }

                bool firstFound = false;
                bool secondFound = false;
                //return dictionary with only letters at position
                for (int i = 0; i < dictionary.Length; i++)
                {
                    firstFound = false;
                    secondFound = false;

                    foreach (byte ch in dictionary[i])
                    {

                        if (ch == (int)letterUsed)
                        {
                            if (firstFound == false && dictionary[i].IndexOf((char)ch) == letterPosition1)
                            {
                                firstFound = true;
                            }
                            else if (firstFound == true && dictionary[i].IndexOf((char)ch,letterPosition1 + 1) == letterPosition2)
                            {
                                secondFound = true;
                            }
                            else if (firstFound == true && secondFound == true && dictionary[i].IndexOf((char)ch,letterPosition2+1) == letterPosition3)
                            {
                                firstFound = false;
                                newDict.Add(dictionary[i]);
                            }
                        }

                    }
                }
                char[] emptyWord = families.emptyWord;
                emptyWord[letterPosition1] = letterUsed;
                emptyWord[letterPosition2] = letterUsed;
                emptyWord[letterPosition3] = letterUsed;
                wordFamilies[0].emptyWord = emptyWord;
                return newDict.ToArray();
            }else { return dictionary; }

        }


        public String[] GetFamilyHard(char letter, string[] dictionary, WordFamily family)
        {

            Console.WriteLine("Words: {0}", dictionary.Length);

            family.family0.Clear();
            family.family1.Clear();
            family.family2.Clear();
            family.family3.Clear();

            List<int> positions = new List<int>();
            //find the most occuring position letter
            //find the most occuring positions letter

            wordFamilies.Insert(0, family);
            for (int word = 0; word < dictionary.Length; word++)
            {
                int count = 0;
                foreach (byte ch in dictionary[word])
                {
                    //ch()eck how many times the letter is reapeated in a word
                    if (ch == (byte)letter)
                    {
                        count++;
                        //Console.WriteLine(array1[i]);
                    }
                }
                
                //if the letter is not repeated
                if (count == 0)
                {
                    family.family0.Add(dictionary[word]);
                }//if the letter is repeated once
                else if (count == 1)
                {
                    wordFamilies[0].family1.Add(dictionary[word]);
                }//if the letter is repeated twice 
                else if (count == 2)
                {
                    wordFamilies[0].family2.Add(dictionary[word]);
                }//if the letter is repeated 3 or more times.
                else if (count == 3)
                {
                    wordFamilies[0].family3.Add(dictionary[word]);
                }
            }

            if (wordFamilies[0].family1.Count != 0) family.family1 = GetLetterPositionHard(wordFamilies[0].family1, letter, 1, family);
            if (wordFamilies[0].family2.Count != 0) family.family2 = GetLetterPositionHard(wordFamilies[0].family2, letter, 2, family);
            if(wordFamilies[0].family3.Count != 0) family.family3 = GetLetterPositionHard(wordFamilies[0].family3, letter, 3, family);

            GetLargestFamilyHard();


            if (wordFamilies[0].chosenFamily == 1)
            {
                int letterPosition = Int32.Parse(family.family1[family.family1.Count]);
                char[] emptyWord = family.emptyWord;
                emptyWord[letterPosition] = letter;
                wordFamilies[0].emptyWord = emptyWord;
                return family.family1.Select(x => x.ToString()).ToArray();

            }
            else if (wordFamilies[0].chosenFamily == 2)
            {
                int letterPosition = Int32.Parse(family.family2[family.family2.Count]);
                int letterPosition2 = Int32.Parse(family.family2[family.family2.Count-1]);
                char[] emptyWord = family.emptyWord;
                emptyWord[letterPosition] = letter;
                emptyWord[letterPosition] = letter;
                wordFamilies[0].emptyWord = emptyWord;
                return family.family2.Select(x => x.ToString()).ToArray();
            }
            else
            {
                return family.family0.Select(x => x.ToString()).ToArray();
            }
            

        }

        internal List<string> GetLetterPositionHard(List<string> dictionary, char letterUsed, int familyNumber, WordFamily families)         {
            if (dictionary.Count < 3) { dictionary.ForEach(p => Console.WriteLine(p)); }
            if(familyNumber == 1)
            {
                ArrayList positions = new ArrayList();
                List<string> newDict = new List<string>();



                char letter = char.Parse(letterUsed.ToString());
                Console.WriteLine("Words: {0}", dictionary.Count);
                //save letters positions and word index 
                for (int i = 0; i < dictionary.Count; i++)
                {
                    int counter = 0;
                    foreach(byte ch in dictionary[i])
                    {
                        if (ch == (int)letterUsed)
                        {
                            positions.Add(counter);
                        }
                        counter++;
                    }
                }
                //get most reccuring position
                positions.Sort();

                int letterPosition = (int)positions[0];


                //return dictionary with only letters at position
                for (int i = 0; i < dictionary.Count; i++)
                {
                    
                    foreach (byte ch in dictionary[i])
                    {
                        if (ch == (int)letterUsed )
                        {
                            if(dictionary[i].IndexOf((char)ch) == letterPosition)
                            {
                                newDict.Add(dictionary[i]);
                            }
                        }
                        
                    }
                }
                newDict.Add(letterPosition.ToString());
                return newDict;
            }
            else if (familyNumber == 2)
            {

                List<int> position1 = new List<int>();
                List<int> position2 = new List<int>();
                
                List<string> newDict = new List<string>();


                //save first appearence on position1
                //save second appearence on position2 for every word
                //save word index in position word index1 and wordindex2 
                for (int word = 0; word < dictionary.Count; word++)
                {
                    int letterIndex = 0;
                    bool found = false;
                    bool found2 = false;

                    foreach (byte ch in dictionary[word])
                    {
                        
                        if (ch == (int)letterUsed && found == false)
                        {
                            position1.Add(letterIndex);
                            
                            
                            found = true;

                        }else if (ch == (int)letterUsed && found == true)
                        {

                            position2.Add(letterIndex);
                            found2 = true;
                            
                        }
                        letterIndex++;
                    }
                    if (found2 == false && found == true)
                    {
                        position1.RemoveAt(position1.Count-1);
                    }

                }


                int appereance;
                int maxIndex = 0;
                int letterPosition1 = 0;
                int letterPosition2 = 0;

                for (int i = 0; i < position1.Count; i++)
                {
                    appereance = 0;

                    for (int j=0; j < position1.Count; j++)
                    {
                        //compare each couple to the remaining items
                        if(position1[i] == position1[j] && position2[i] == position2[j])
                        {
                            
                            appereance++;
                            
                        }
                    }
                    if (maxIndex < appereance)
                    {
                        maxIndex = appereance;
                        letterPosition1 = position1[i];
                        letterPosition2 = position2[i];
                    }

                }// letterpositions 1 and 2 are the most common positions

                
                //return dictionary with only letters at position
                for (int i = 0; i < dictionary.Count; i++)
                {
                    bool firstFound = false;
                    foreach (byte ch in dictionary[i])
                    {
                        
                        if (ch == letterUsed)
                        {
                            if (firstFound == false && dictionary[i].IndexOf((char)ch) == letterPosition1)
                            {
                                firstFound = true;
                                //Console.WriteLine("1 found");
                            }else if (firstFound == true && dictionary[i].IndexOf((char)ch,letterPosition1+1) == letterPosition2)
                            {
                                //Console.WriteLine("2 found");
                                firstFound = false;
                                newDict.Add(dictionary[i]);
                            }
                        }

                    }
                }
                newDict.Add(letterPosition1.ToString());
                newDict.Add(letterPosition2.ToString());
                return newDict;
            }
            else { return dictionary.ToList(); }

        }

        internal void GetLargestFamilyHard()
        {
            //family word with largest n of words is returned
            //letter not found
            //should find family where is at same position

            int n0 = wordFamilies[0].family0.Count;
            int n1 = wordFamilies[0].family1.Count-1;
            int n2 = wordFamilies[0].family2.Count-2;
            int n3 = wordFamilies[0].family3.Count;
            int tot = n1 + n2 + n3 + n0;

            int percn1;
            try
            {
                percn1 = n1 / n0 * 100;
            }
            catch (DivideByZeroException e)
            {
                percn1 = 81;
            }


            if (percn1>80 || wordFamilies[0].family0.Count ==0 )
            {
                wordFamilies[0].chosenFamily = 1;
            }
            else 
            {
                wordFamilies[0].chosenFamily = 0;
            }
            Console.WriteLine("family 0: {0}, family1: {1}, family2: {2}, family3: {3}, tot: {4}.", n0, n1, n2, n3, tot);
            
        }

    }
}
