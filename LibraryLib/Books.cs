using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryLib
{
    public static class Books
    {
        public static int numOfBooks = 88; //initial number of books in library
        public static List<Book> books;//books in library
        public static Book[] copy;
        static Books()
        {
            books = new List<Book>();
            for (int i = 0; i < numOfBooks; i++)
            {
                Book book = new Book(i);
                books.Add(book);
            }
            numOfBooks = GetLength();
            copy = new Book[88];
            books.CopyTo(copy);
        }
        public static Book FindByAuthor(string author, int indexTransfered, out int indexCalc)
        {
            indexCalc = indexTransfered;
            if (author != null)
            {
                for (int i = indexCalc; i < numOfBooks; i++)
                {
                    if (books[i].Author.ToLower().Contains(author.ToLower()))
                    {
                        Book tempBook = books[i];
                        indexCalc = i;
                        i = numOfBooks - 1;
                        return tempBook;
                    }
                }
            }
            return null;
        }
        public static Book FindByTheme(string theme, int indexTransfered, out int indexCalc)
        {
            indexCalc = indexTransfered;
            if (theme != null)
            {
                for (int i = indexCalc; i < numOfBooks; i++)
                {
                    if (books[i].Theme.ToLower().Contains(theme.ToLower()))
                    {
                        Book tempBook = books[i];
                        indexCalc = i;
                        i = numOfBooks - 1;
                        return tempBook;
                    }
                }
            }
            return null;
        }
        public static Book FindByTitle(string title, int indexTransfered, out int indexCalc)
        {
            indexCalc = indexTransfered;
            if (title != null)
            {
                for (int i = indexCalc; i < numOfBooks; i++)
                {
                    if (books[i].Title.ToLower().Contains(title.ToLower()))
                    {
                        Book tempBook = books[i];
                        indexCalc = i;
                        i = numOfBooks - 1;
                        return tempBook;
                    }
                }
            }
            return null;
        }
        public static void RemoveBookFromLib(int index)
        {
            books.RemoveAt(index);
            numOfBooks = GetLength();
        }
        public static void ReturnToLib(string title)
        {
            Book book = new Book(title);
            books.Add(book);
            numOfBooks = GetLength();
        }
        public static int GetLength()
        {
            int length = 0;
            foreach (var item in books)
            {
                if (item != null)
                {
                    length++;
                }
            }
            return length;
        }
        public static Book IsInLibrary(string title)
        {
            for (int i = 0; i < numOfBooks; i++)
            {
                if (books[i].Title == title)
                {
                    return books[i];
                }
            }
            return null;
        }
    }
}
