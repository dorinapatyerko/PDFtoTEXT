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
            //String nameComponent = "MINE";
            //Rename the files
            //System.IO.File.Move("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/vmi.pdf", "C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/asd"+ nameComponent + ".pdf");

            //Use that functions
            string fullText = Transform("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/20200107_example.pdf");
            string mergedIDs = IDNumberizator(fullText);
            List<string> listedIDs = mergedIDs.Split('\n').ToList();

            for (int i=0; i<listedIDs.Count; i++)
            {
                Console.WriteLine(listedIDs[i]);
            }


            //Console.WriteLine(mergedIDs);
            System.IO.File.WriteAllText("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/done.txt", mergedIDs);
            Console.ReadLine();
        }

        //ezek nem a mainben fognak szerepelni, hanem egy külön fileban

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

        public static string IDNumberizator(string text)
        {
            string mergeIDs="";
            string startID = "adóazonosító jele: ";
            string endID = " TAJ";
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
