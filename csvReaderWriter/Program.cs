using System;
using System.IO;
using CsvHelper;

namespace csvReaderWriter {

    public class Program {
        static void Main(string[] args) {

            string question = null;
            string answer = null;
            string value = null;
            string ans = null;
            string path = "flashcards.csv";

            //If the file exists prompt user
            if (File.Exists(path)) {

                Console.WriteLine("Current File Contents: ");

                //Read Contents of CSV
                csvRead(); 

                Console.WriteLine("\nType 'Exit' once you're done. \n");

                //While the User Wants to Continue Making FlashCards
                while (value != "N") {

                    //Question Prompt
                    Console.WriteLine("Type in your question for the flash card: ");
                    question = Console.ReadLine();

                    //Answer Prompt
                    Console.WriteLine("Type in the correct answer to the question: ");
                    answer = Console.ReadLine();

                    //Write the Question and Answer to the CSV
                    csvWrite(question, answer, path);

                    //If the User Wants to Continue Making Another Card
                    Console.WriteLine("Would you like to create another card? (Y/N): ");
                    value = Console.ReadLine();

                    //If the User Wants to See Current File Contents
                    Console.WriteLine("Would you like to read to see the current flash cards? (Y/N): ");
                    ans = Console.ReadLine();

                    if (ans == "Y") {

                        csvRead();

                    }else {

                        continue;

                    }

                } 

            }else {

                Console.WriteLine("Requested File Path Doesn't Exist.");

            }
        }

           
        //Reading CSV Contents via StreamReader and a File Titled "flashcards.csv"
        public static void csvRead() {

            string path = "flashcards.csv";

            try {

                using (StreamReader sr = new StreamReader(path, true)) {

                    string line;

                    while ((line = sr.ReadLine()) != null) {

                        Console.WriteLine(line);

                    }
                }

            } catch (Exception e){

                Console.WriteLine("An error occured trying to read a new flash card.");
                Console.WriteLine(e.Message);

            }

        }

        //Writing to a CSV Titled "flashcards.csv" via StreamWriter
        public static void csvWrite(string question, string ans1, string filepath) {

            string path = "flashcards.csv";   
                            
            try {

                using (StreamWriter sw = new StreamWriter(path, append: true)) {

                    sw.WriteLine(question + "," + ans1);

                }

            } catch (Exception e) {

                Console.WriteLine("An error occured trying to create a new flash card.");
                Console.WriteLine(e.Message);

            }

        }

    }
}
