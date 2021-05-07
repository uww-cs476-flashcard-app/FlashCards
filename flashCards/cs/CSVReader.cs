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
                        FlashCard newCard = new FlashCard
                        {
                            Question = values[0],
                            Answer = values[1]
                        };
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

        //Writing to a CSV at "path" via StreamWriter
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

        //Updates a line in a csv
        public static void CSVWrite(string question, string ans1, string path, int lineNum)
        {
            //create new file to hold cardset with modification
            string destinationFile = path.Substring(0, path.Length - 4) + "_modified.CSV";
            try
            {
                FileStream fs = File.Create(destinationFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //verify that the cardset exists
            if (!File.Exists(path))
            {
                throw new Exception(String.Format("Source:{0} does not exsists", path));
            }

            // Read from the target file and write to a new file.
            try
            {
                int currentLine = 0;
                string line = null;
                using (StreamReader reader = new StreamReader(path))
                using (StreamWriter writer = new StreamWriter(destinationFile))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (currentLine == lineNum)
                        {
                            writer.WriteLine(question + "," + ans1);
                        }
                        else
                        {
                            writer.WriteLine(line);
                        }
                        currentLine++;
                    }
                }
            } catch(Exception e)
            {
                Console.WriteLine("An error occured trying to modify a new flash card.");
                Console.WriteLine(e.Message);
            }

            //replace old file with modified file
            File.Delete(path);
            File.Move(destinationFile, path);
            
        }
    }
}