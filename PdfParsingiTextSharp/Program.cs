//==========================================================================================================
//INTRO
//      Final *.pdf name format example: 8198040935_00000000_201912 (~ aka: ID_[nulls+j]_YYYYMM)
//==========================================================================================================
//TASKS
//DONE string Date from the *.pdf -> format: YYYYMM
//TEST use the second component (00000000) as serial number. For example: 00000001 -> 
//     if I have more than 9 file with tha same IDnumber it will run with an error, 
//     so I must handle that with an Exception. 
//TEST handle 2 numbers
//DONE use current directories
//TODO *.exe format and userfriendly looking
//TODO merge the *.pdf-s with the same IDnumbers
//TODO replace the functions to an another *.cs file
//==========================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;

namespace PdfParsingiTextSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use THAT method
            FileNameGenerator();
            Console.ReadLine();
        }
    
        // Methods

        public static string Transform(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                return text.ToString();
            }
        }

        public static string Numberizator(string text, string startID, string endID)
        {
            string mergeIDs="";
            //string startID = "adóazonosító jele: ";
            //string endID = " TAJ";
            int startIndex, endIndex;
            bool status = true;

            while (status == true)
            {
                if (text.Contains(startID) && text.Contains(endID))
                {
                    startIndex = text.IndexOf(startID, 0) + startID.Length;
                    endIndex = text.IndexOf(endID, startIndex);
                    mergeIDs += text.Substring(startIndex, endIndex - startIndex) + "\n";
                    text = text.Remove(0, endIndex);
                }
                else
                {
                    status = false;
                }
            }
            return mergeIDs;
        }

        public static void FileNameGenerator()
        {
            // Source file
            string fullText = Program.Transform(Environment.CurrentDirectory + "\\file.pdf");
            // ID numbers' gap
            string mergedIDs = Program.Numberizator(fullText, "adóazonosító jele: ", " TAJ");
            // Date's gap
            string mergedDates = Program.Numberizator(fullText, "Dátum: ", " Oldal");

            //==========================================================================================================

            // Date merge
            string rightDate = mergedDates[0].ToString() + mergedDates[1].ToString() + mergedDates[2].ToString() + mergedDates[3].ToString() + mergedDates[5].ToString() + mergedDates[6].ToString();
            List<string> listedIDs = mergedIDs.Split('\n').ToList();

            /* // Junk
            for (int i = 0; i < listedIDs.Count; i++)
            {
                Console.WriteLine(listedIDs[i]);
            }*/

            // File name generator
            string nulls = "0000000"; // default
            int j = 0;
            for (int i = 1; i < listedIDs.Count; i++)
            {
                // simplified and better method 4 serial number handle system
                // pseudo-like: minj, maxj, nulls -= "0"
                if (j >= 10) // memory-friendly if statement
                {
                    for (int k = 1; k <= 7; k++)
                    {
                        double jmin = Math.Pow(10, k) - 1;
                        double jmax = Math.Pow(10, k + 1);
                        if (j > Convert.ToInt32(jmin) && j < Convert.ToInt32(jmax))
                        {
                            char[] arr = nulls.ToCharArray(0, (7 - k));
                            Console.WriteLine(nulls);
                        }
                    }
                }

                /* another version 4 serial number handle system
                if (j > 9 && j < 100) nulls = "000000";
                if (j > 99 && j < 1000) nulls = "00000";
                if (j > 999 && j < 10000) nulls = "0000";
                if (j > 9999 && j < 100000) nulls = "000";
                if (j > 99999 && j < 1000000) nulls = "00";
                if (j > 999999 && j < 10000000) nulls = "0";
                if (j > 9999999 && j < 100000000) nulls = "";
                */

                File.Move(Environment.CurrentDirectory + "\\Splitted\\" + i + "_file.pdf", Environment.CurrentDirectory + "\\Splitted\\" + listedIDs[i - 1] + "_" + nulls + j + "_" + rightDate + ".pdf");

                if (i + 1 != listedIDs.Count)
                {
                    if (listedIDs[i - 1] == listedIDs[i]) j++;
                    if (listedIDs[i - 1] != listedIDs[i]) j = 0;
                }
            }
            /* //Junk
            Console.WriteLine(rightDate);
            System.IO.File.WriteAllText("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/done.txt", mergedIDs);*/
        }
    }
}
