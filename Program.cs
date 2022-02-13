using System;
using System.Collections.Generic;


namespace ISBN
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> isbns = new List<string>();
            isbns.Add("0471958697");
            isbns.Add("0 471 95869 7");
            isbns.Add("0-471-95869-7");
            isbns.Add("9780470059029");
            isbns.Add("978-1788399083");
            isbns.Add("047195869X");
            isbns.Add("007462542X");


            isbns.ForEach(i => { Console.WriteLine(i + " is " + (ISBN.checkISBN(i) ? "VALID" : "INVALID"));  });
            Console.WriteLine();

            isbns.Clear();
            isbns.Add("0471958697");
            isbns.Add("0 471 95869 7");
            isbns.Add("0-471-95869-7");
            isbns.Add("047195869X");
            isbns.Add("007462542X");

            isbns.ForEach(i => { Console.WriteLine(i + " is " + (ISBN.checkIsbn10(i) ? "VALID" : "INVALID")); });

            Console.ReadKey();
        }
    }
}
