using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WordGameOO
{
    public class Letter
    {
        public char value;
       
        List<Letter> usedLetters = new List<Letter>();

        

        public void AddLetter(Letter letter)
        {
            usedLetters.Add(letter);
        }

        public void PrintUsedLetters()
        {
            Program.Print("Used Letters:");
            for (int i = 0; i < usedLetters.Count; i++)
            {
                Console.Write(usedLetters[i].Value);
                Console.Write(", ");
            }
            Console.Write("\n");
        }

        public char GetLetter()
        {
            //to be put in a do while loop
            Console.WriteLine("Please Enter a letter:");
            try
            {
                value = Console.ReadLine()[0];
            }
            catch
            {

            }
            
            char.ToLower(value);
            if ((int)value >= 97 & (int)value <= 122)
            {
                bool isEmpty = !usedLetters.Any();
                if (isEmpty)
                {
                    Letter letter = new Letter();
                    letter.value = value;
                    AddLetter(letter);
                    return value;
                    
                }
                else
                {
                    bool exists = false;
                    for (int i = 0; i < usedLetters.Count; i++)
                    {
                        if ((int)value == ((int)usedLetters[i].value))
                        {
                            exists = true;
                            break;
                        }
                        else
                        {
                            exists = false;
                            
                        }
                    }
                    if (exists == true)
                    {
                        Program.Print("You already used this letter");
                        return value = ' ';
                    }
                    else
                    {
                        Letter letter = new Letter();
                        letter.value = value;
                        AddLetter(letter);
                        return value;
                    }
                }
            } 
            else
            {
                Console.WriteLine("Character not Valid!");
                return value = ' ';
            }
            
                
        }



        public char Value { get => value; set => this.value = value; }
        
        
    }
}
