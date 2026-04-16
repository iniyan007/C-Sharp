using System;
using System.IO;
using System.Linq;

namespace filehandling
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "input.txt";
            string outputFilePath = "output.txt";

            try
            {
                if (!File.Exists(inputFilePath))
                {
                    throw new FileNotFoundException("Input file not found.");
                }

                string content = File.ReadAllText(inputFilePath);

                int lineCount = File.ReadAllLines(inputFilePath).Length;

                int wordCount = content
                                .Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                                .Length;

                int charCount = content.Length;

                string result = $"File Analysis Result:\n" +
                                $"Lines: {lineCount}\n" +
                                $"Words: {wordCount}\n" +
                                $"Characters: {charCount}";

                File.WriteAllText(outputFilePath, result);

                Console.WriteLine("Processing complete. Results written to output.txt");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("I/O Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Program execution finished.");
            }
        }
    }
}
