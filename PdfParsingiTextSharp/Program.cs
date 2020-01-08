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
            // Use that functions
            string fullText = Transform("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/20200107_example.pdf");
            string mergedIDs = IDNumberizator(fullText, "adóazonosító jele: ", " TAJ");
            string mergedDates = IDNumberizator(fullText, "Dátum: ", " Oldal");
            string rightDate = mergedDates[0].ToString() + mergedDates[1].ToString() + mergedDates[2].ToString() + mergedDates[3].ToString() + mergedDates[5].ToString() + mergedDates[6].ToString();

            // Transform the string to a list
            List<string> listedIDs = mergedIDs.Split('\n').ToList();
            for (int i=0; i<listedIDs.Count; i++)
            {
                Console.WriteLine(listedIDs[i]);
            }

            // Final *.pdf name format example: 8198040935_00000000_201912 (~ aka: ID_[0..0]_YYYYMM)
            //TODO string Date from the *.pdf -> format: YYYYMM
            //TODO use the second component (00000000) as serial number. For example: 00000001 -> 
            //     if I have more than 9 file with tha same IDnumber it will run with an error, 
            //     so I must handle that with an Exception.
            int j = 0;
            for (int i = 1; i < listedIDs.Count; i++) 
            {
                System.IO.File.Move("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/Splitted/" + i + "_justafool_20200107_example.pdf", "C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/Splitted/" + listedIDs[i-1] + "_" + "0000000" + j + "_" + rightDate + ".pdf");
                if (i + 1 != listedIDs.Count) 
                { 
                    if (listedIDs[i-1] == listedIDs[i]) j++;
                    if (listedIDs[i-1] != listedIDs[i]) j = 0;
                }
            }

            //String nameComponent = "MINE";
            //Rename the files
            //System.IO.File.Move("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/vmi.pdf", "C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/asd"+ nameComponent + ".pdf");

            Console.WriteLine(rightDate);
            System.IO.File.WriteAllText("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/done.txt", mergedIDs);
            Console.ReadLine();
        }

        //TODO replace these functions an another *.cs file

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

        public static string IDNumberizator(string text, string startID, string endID)
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
    }
}
