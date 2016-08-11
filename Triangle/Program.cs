using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Triangle
{
    class Program
    {
        static int[][] triangle;
        static int rowCount;
        static readonly char[] separator = {' '};
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid parameters. Use: Triangle.exe [-p]\n-p\tpath to text file with triangle");
                return;
            }
            else if (!File.Exists(args[0]))
            {
                Console.WriteLine("File '{0}' does not exist.", args[0]);
                return;
            }

            /* Read triangle from text file */
            StreamReader sr = new StreamReader(args[0]);
            
            rowCount = 0;
            while (!sr.EndOfStream)         // Get quantuty of lines
            {
                sr.ReadLine();
                rowCount++;
            }
            sr.BaseStream.Position = 0;

            triangle = new int[rowCount][]; // Fill array with numbers
            string[] temp;
            for (int i = 0; i < rowCount; i++)
            {
                temp = sr.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                triangle[i] = new int[temp.Length];
                for (int j = 0; j < temp.Length; j++)
                {
                    try
                    {
                        triangle[i][j] = Convert.ToInt32(temp[j]);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Input data has invalid format. Please make sure that you have a triangle of numbers separated by single spaces.");
                        sr.Close();
                        sr.Dispose();
                        return;
                    }
                }
            }

            sr.Close();
            sr.Dispose();

            /* Calculating the maximal value of the path */

            for (int i = rowCount - 1; i > 0; i--)
            {
                for (int j = 0; j < triangle[i - 1].Length; j++)
                {
                    triangle[i - 1][j] += triangle[i][(triangle[i][j] > triangle[i][j + 1]) ? (j) : (j + 1)];
                }
            }

            Console.WriteLine("Maximal sum is: {0}", triangle[0][0]);
        }
    }
}
