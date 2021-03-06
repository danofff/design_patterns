﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iterator
{
    class Program
    {
        class Book
        {
            public string Name { get; set; }
        }
        interface IBookIterator
        {
            bool HasNext();
            Book Next();
        }

        interface IBookNumerable
        {
            IBookIterator CreateNumerator();
            int Count { get; }
            Book this[int index] { get; }
        }

        class LibraryNumerator : IBookIterator
        {
            IBookNumerable aggregate;
            int index = 0;
            public LibraryNumerator(IBookNumerable a)
            {
                aggregate = a;
            }

            public bool HasNext()
            {
                return index < aggregate.Count;
            }

            public Book Next()
            {
                return aggregate[index++];
            }
        }

        class Library : IBookNumerable
        {
            private Book[] books;
            public Library()
            {
                books = new Book[]
                {
                    new Book {Name="Война и мир" },
                    new Book {Name="Отцы и дети" },
                    new Book {Name="Вишневый сад" }
                };
            }

            public Book this[int index]
            {
                get
                {
                    return books[index] ;
                }
            }

            public int Count
            {
                get
                {
                    return books.Length;
                }
            }

            public IBookIterator CreateNumerator()
            {
                return new LibraryNumerator(this);
            }
        }

        class Reader
        {
            public void SeeBooks(Library library)
            {
                IBookIterator iterator = library.CreateNumerator();
                while (iterator.HasNext())
                {
                    Book book = iterator.Next();
                    Console.WriteLine(book.Name);
                }
            }
        }

        static void Main(string[] args)
        {
            Library lib = new Library();
            Reader reader = new Reader();
            reader.SeeBooks(lib);
            Console.ReadKey();
        }


    }
}
