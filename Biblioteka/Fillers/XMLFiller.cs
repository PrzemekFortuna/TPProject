using Biblioteka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fillers
{
    public class XMLFiller : IFiller
    {
        private string _XMLpath = string.Empty;

        public XMLFiller(string fileName)
        {
            //_XMLpath = string.Format("pack://application:,,,/XMLFiles/{0}.xml", fileName);
            _XMLpath = string.Format(@"Resources\{0}.xml", fileName);
        }

        public void Fill(KontekstDanych kontekstDanych)
        {
            
            XDocument xDocument = XDocument.Load(_XMLpath);
            int i = 1;
            foreach(XElement element in xDocument.Descendants("Book"))
            {
                int bookID;
                int.TryParse(element.Element("BookID").Value, out bookID);
                string title = element.Element("Name").Value;
                Book book = new Book(bookID, title);

                kontekstDanych.BooksDictionary.Add(i, book);
                i++;
            }

            foreach(XElement element in xDocument.Descendants("Reader"))
            {
                string name = element.Element("Name").Value;
                string surname = element.Element("Surname").Value;
                int personalID;
                int.TryParse(element.Element("PersonalID").Value, out personalID);
                Reader reader = new Reader(name, surname, personalID);

                kontekstDanych.clientList.Add(reader);
            }
        }
    }
}
