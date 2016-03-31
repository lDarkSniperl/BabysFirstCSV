using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Parser_1
{
    class Program
    {
        public const string inputPath = "C:\\Users\\Justin\\Documents\\Visual Studio 2012\\Projects\\CSV Parser 1\\CSV Parser 1\\TestCSV.csv";
        public const string outputPath = "testOutput.csv";

        static void Main(string[] args)
        {
            
            List<string> GUID = new List<string>();
            List<int> sum12 = new List<int>();
            List<int> Value3 = new List<int>();
            List<string> GUIDDupes = new List<string>();
            List<bool> GUIDisDupe = new List<bool>();

            //Initiates the input stream and clears out the first row of column headers
            var reader = new StreamReader(File.OpenRead(inputPath));
            reader.ReadLine();


            while (!reader.EndOfStream)
            {
                //get a row, split, and format into it's proper categories
                var line = reader.ReadLine();
                var values = line.Split(',');
                for (int i = 0; i < values.Length; i++)
                    values[i] = streamTrimmer(values[i]);

                //checks for duplicates, and mark the current appropriately with that information
                if (GUID.Contains(values[0]))
                {
                    GUIDDupes.Add(values[0]);
                    GUIDisDupe.Add(true);
                }
                else
                {
                    GUIDisDupe.Add(false);
                }
                GUID.Add(values[0]);
                
                sum12.Add(Int32.Parse(values[1]) + Int32.Parse(values[2]));
                Value3.Add(values[3].Length);
            }
            reader.Close();



            //Output the requested console output
            Console.Write("There are " + GUID.Count().ToString() + "total entries.\n");
                //find the maximum value of 1+2, and then get its GUID
            int maxSum12 = sum12.Max();
            string maxGUID = GUID[sum12.FindIndex(item => item == maxSum12)];
            Console.Write("The maximum value of Val1 + Val2 is " + maxSum12.ToString() + ".\n");
            Console.Write("This value is located at " + maxGUID + ".\n");
            Console.Write("The average value of Value3 is " + Value3.Average().ToString() + ". \n");
            foreach (string dupe in GUIDDupes)
            {
                Console.Write(dupe + " is a duplicate GUID. \n");
            }



            //Create a new CSV line for each row in the data table and write it
            var streamOut = new StreamWriter(outputPath);
            for (int i = 0; i < GUID.Count; i++)
            {
                streamOut.WriteLine(GUID[i] + "," + sum12[i].ToString() + "," + GUIDisDupe[i].ToString() + "," + (Value3[i] > Value3.Average()).ToString());
                streamOut.Flush();
            }
            streamOut.Close();   
        }

        //takes an input stream, and removes any extraneous characters that don't belong, mostly from beginning and end formatting
        static string streamTrimmer(string input)
        {
            string[] charsToRemove = new string[] {"\\", "\"", ".", ";", "'"};

            foreach (string c in charsToRemove)
            {
                input = input.Replace(c, string.Empty);
            }
            return input;
        }

    }
}
