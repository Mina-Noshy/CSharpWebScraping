using System;
using HtmlAgilityPack;

namespace ScrapingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.Write("uri : ");
            string uri = Console.ReadLine();

            Console.WriteLine();
            Console.Write("tag : ");
            string tag = Console.ReadLine();

            Console.WriteLine();
            Console.Write("prop : ");
            string prop = Console.ReadLine();

            Console.WriteLine();
            Console.Write("save to : ");
            string saveTo = Console.ReadLine();

            GetWebLinks(uri, tag, prop, saveTo);
        }

        public static void GetWebLinks(string uri, string tag, string prop, string saveTo)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = web.Load(uri);

            if(string.IsNullOrEmpty(saveTo))
                saveTo = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/scraping.txt";
                
                System.IO.File.AppendAllText(saveTo, $"\nURI: {uri}\nTAG: {tag}\nPROP: {prop}"+
                        $"\nDATE: {DateTime.UtcNow.ToString("yyyy-MM-dd")}\n----------------------------\n\n");

            int counter = 1;

            foreach (HtmlNode item in doc.DocumentNode.SelectNodes($"//{tag}"))
            {

                HtmlAttribute att = item.Attributes[$"{prop}"];
                if(att.Value.Contains("#"))
                    continue;
                else
                    System.IO.File.AppendAllText(saveTo, counter.ToString() + "- " + att.Value + "\n");
                                
                counter ++;

            }

                System.IO.File.AppendAllText(saveTo, "================================================");

            Console.WriteLine("finished successfully");
        }

    }
}
