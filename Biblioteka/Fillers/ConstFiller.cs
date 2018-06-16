using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;

namespace Fillers
{
    public class ConstFiller : IFiller
    {

        public void Fill(KontekstDanych kontekstDanych)
        {
            kontekstDanych.clientList.Add(new Reader("Jan", "Kowalski", 1));
            kontekstDanych.clientList.Add(new Reader("Tomasz", "Kowalski", 2));
            kontekstDanych.clientList.Add(new Reader("Janusz", "Kowalski", 3));

            kontekstDanych.BooksDictionary.Add(1, new Book(1, "Ksiega Dzungli"));
            kontekstDanych.BooksDictionary.Add(2, new Book(2, "Pasja C++"));
            kontekstDanych.BooksDictionary.Add(3, new Book(3, "Apokalipsa"));

            kontekstDanych.statesList.Add(new State(kontekstDanych.BooksDictionary[1], 10));
            kontekstDanych.statesList.Add(new State(kontekstDanych.BooksDictionary[2], 1));
            kontekstDanych.statesList.Add(new State(kontekstDanych.BooksDictionary[3], 110));

            kontekstDanych.borrowingsCollection.Add(new Borrowing(kontekstDanych.clientList[1], new DateTime(2018, 3, 4), kontekstDanych.statesList[0]));
            kontekstDanych.borrowingsCollection.Add(new Borrowing(kontekstDanych.clientList[2], new DateTime(2018, 3, 5), kontekstDanych.statesList[1]));
            kontekstDanych.borrowingsCollection.Add(new Borrowing(kontekstDanych.clientList[0], new DateTime(2018, 3, 6), kontekstDanych.statesList[2]));
        }
    }
}
