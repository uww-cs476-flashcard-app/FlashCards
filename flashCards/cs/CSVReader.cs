using System;
using System.IO;
using System.Collections.Generic;

namespace flashCards.cs
{
    public class CSVReader
    {

        //Reading CSV Contents via StreamReader and a File Titled "flashcards.csv"
        public static List<FlashCard> CSVRead(string path)
        {
            List<FlashCard> flashCards = new List<FlashCard>();
            try
            {

                using (StreamReader sr = new StreamReader(path, true))
                {

                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(",");
                        FlashCard newCard = new FlashCard();
                        newCard.Question = values[0];
                        newCard.Answer = values[1];
                        flashCards.Add(newCard);
                    }
                }

            }
            catch (Exception e)
            {

                Console.WriteLine("An error occured trying to read a new flash card.");
                Console.WriteLine(e.Message);

            }

            return flashCards;

        }

        //Writing to a CSV Titled "flashcards.csv" via StreamWriter
        public static void CSVWrite(string question, string ans1, string path)
        {


            try
            {

                using (StreamWriter sw = new StreamWriter(path, append: true))
                {

                    sw.WriteLine(question + "," + ans1);

                }

            }
            catch (Exception e)
            {

                Console.WriteLine("An error occured trying to create a new flash card.");
                Console.WriteLine(e.Message);

            }

        }
    }
}