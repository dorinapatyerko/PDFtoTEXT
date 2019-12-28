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

            //List<string> IDnumbers = new List<string>(); // Useless

            

            //List<string> current = null;
            string text = ExtractTextFromPdf("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/doresz.pdf");
            
            /*{
                if (text.Contains("CustomerEN") && current == null)
                    current = new List<string>();
                else if (text.Contains("CustomerCh") && current != null)
                {
                    groups.Add(current);
                    current = null;
                }
                if (current != null)
                    current.Add(text);
            }*/

            string start = "foglalkoztatási sorszám: ";
            string end = " /";


            //String IDnum = "NaN"; //text kezdeti értéke...

            // az éppen megtalált stringre vonatkozik -> listába fűzés
            /*
            if (text = "")
            {
                Break; //kilép a ciklusból...
            }
            else */
            
            String IDnumber = getBetween(text, start, end);
            //IDnumbers.Add(text);  // Szerintem ehhez nem is kell lista
            
            

            
            
            //for ciklus
            System.IO.File.WriteAllText("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/Fileslist.txt", IDnumber);
            
            System.IO.File.WriteAllText("C:/Users/patydor/Google Drive/WORK/Notes/Progger Projects/PDFtoTEXT/Files/done.txt", text);
            Console.WriteLine(text);
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
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                Console.WriteLine(Start);
                Console.WriteLine(End);
                Console.WriteLine(strSource.Substring(Start, End - Start));
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}
