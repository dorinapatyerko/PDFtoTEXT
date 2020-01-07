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

            string fullText = ExtractTextFromPdf("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/20200107_example.pdf");

            string startID = "adóazonosító jele: ";
            string endID = "TAJ";
            //int textLength = fullText.Length;
            int startIndex, endIndex;
            bool status = true;

            while(status == true)
                { 
                if (fullText.Contains(startID) && fullText.Contains(endID))
                {
                    startIndex = fullText.IndexOf(startID, 0) + startID.Length;
                    endIndex = fullText.IndexOf(endID, startIndex);
                    Console.WriteLine(startIndex); // Kiirja a megtalált adószám text-béli kezdő indexét
                    Console.WriteLine(endIndex); // Kiirja a megtalált adószám text-béli végződő indexét
                    Console.WriteLine(fullText.Substring(startIndex, endIndex - startIndex)); //Ezt az adatot kell listában eltárolni!
                    Console.WriteLine(fullText.Length);
                    fullText = fullText.Remove(0, endIndex);  //(endIndex);
                    Console.WriteLine(fullText.Length);
                }
                else
                {
                    Console.WriteLine("IDnumber is missing.");
                    status = false;
                }
            }

            //Destroyolt függvény maradékai.
            //String IDnumber = getBetween(text, start, end);
            //System.IO.File.WriteAllText("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/Fileslist.txt", IDnumber);
            //IDnumbers.Add(text);  // Szerintem ehhez nem is kell lista

            System.IO.File.WriteAllText("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/done.txt", fullText);
            //Console.WriteLine(text);
            Console.ReadLine();
        }

        //ezek nem a mainben fognak szerepelni, hanem egy külön fileban

        public static string ExtractTextFromPdf(string path)
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
        /*
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                Console.WriteLine(Start); // Kiirja a megtalált adószám text-béli kezdő indexét
                Console.WriteLine(End); // Kiirja a megtalált adószám text-béli végződő indexét
                Console.WriteLine(strSource.Substring(Start, End - Start));
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }*/
    }
}
