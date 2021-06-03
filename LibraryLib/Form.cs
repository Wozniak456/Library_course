using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryLib
{
    public class Form : IForm
    {
        internal event FormStateHandler TookABookFromLibrary;
        internal event FormStateHandler ReturnABookToALib;
        //to open a new form
        internal virtual event FormStateHandler Opened;
        //to close a form
        internal virtual event FormStateHandler Closed;
        private int _id; //unique id
        static public int counter = 0; //counter of users
        public List<Book> list = new List<Book>(); //books which are in a form
        internal int _numOfBooks;//number of books which are in form
        private string _user; //name of new user
        public Form(string user)
        {
            _id = ++counter;
            _numOfBooks = 0;
            _user = user;
        }
        public int Id
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _user; }
        }
        public void OnOpend()
        {
            if (Opened != null)
            {
                Opened(this, new FormEventArgs($"Successfully registred: {_user}. Id: {Id}"));
            }
            else throw new Exception("you're not registred");
        }
        public void AddABook(Book book)
        {
            if (book != null)
            {
                Book bookToAdd = Books.IsInLibrary(book.Title);
                if (bookToAdd != null)
                {
                    list.Add(book);
                    _numOfBooks++;
                    if (TookABookFromLibrary != null)
                    {
                        TookABookFromLibrary(this, new FormEventArgs($"From library was taken: {book.Title}. Book goes to {_user}"));
                        book.now = DateTime.Now;
                    }
                }
            }
            else
            {
                throw new NullReferenceException("There isn't any such book in a library.");
            }
        }
        public void DeleteABook(Book book)
        {
            bool isDeleted = false;
            if (book != null)
            {
                for (int i = 0; i < _numOfBooks; i++)
                {
                    if (list[i].Title == book.Title && ReturnABookToALib != null)
                    {
                        Books.ReturnToLib(book.Title);
                        list.RemoveAt(i);
                        i = _numOfBooks - 1;
                        if (ReturnABookToALib != null)
                        {
                            ReturnABookToALib(this, new FormEventArgs($"Book was returned to library: {book.Title}"));
                        }
                        _numOfBooks--;
                        isDeleted = true;
                    }
                }
                if (!isDeleted)
                    throw new NullReferenceException("You have not such book");
            }
            else
                throw new NullReferenceException("You have not such book");
        }
        public void Close()
        {
            if (Closed != null)
            {
                Closed(this, new FormEventArgs($"Form {_id} is closed."));
                for (int i = 0; i < _numOfBooks; i++)
                {
                    DeleteABook(list[i]);
                    i--;
                }
            }
        }
        public void ShowForm()
        {
            if (_numOfBooks == 0)
                throw new Exception("is empty...");
            for (int i = 0; i < _numOfBooks; i++)
            {
                list[i].Show1Book(i);
                Console.WriteLine($"Date of issue: {list[i].now}");
            }
        }
    }
}
