using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using _1._3;

namespace _1._3
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteToFile();
            ReadFromFile();
        }

        public static void WriteToFile()
        {
            Database writeToDatabase = new Database(45021, "Hello Database", DateTime.Now);

            using (StreamWriter file = new StreamWriter(@"C:\Users\nguld\IS\13.txt"))
            {
                string[] data = writeToDatabase.getData();

                foreach(string stringData in data)
                {
                    file.WriteLine(stringData);
                }

                Console.WriteLine("Wrote the following to file");

                foreach (string stringData in data)
                {
                    Console.WriteLine(stringData);
                }
            }
        }

        private static void ReadFromFile()
        {
            string[] data = File.ReadAllLines(@"C:\Users\nguld\IS\13.txt");

            Console.WriteLine("\nRead the following from file");
            foreach (string stringData in data)
            {
                Console.WriteLine(stringData);
            }

            int number;
            DateTime date;
            bool isAnInt = int.TryParse(data[0], out number);
            bool isADateTime = DateTime.TryParse(data[2], out date);
            if (isAnInt && isADateTime)
            {
                Database readFromDatabase = new Database(number, data[1], date);
                Console.WriteLine("\nValidation passed, wrote the database to object");

            }
            else
            {
                if (isAnInt)
                {
                    Console.WriteLine("\nValidation Failed! Date is not in the correct format");
                }
                else
                {
                    Console.WriteLine("\nValidation Failed! Id is not in the correct format");
                }
            }

            Console.ReadKey();
        }

        
    }
}
