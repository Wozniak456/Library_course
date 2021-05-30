using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryLib
{
    public class Library
    {
        List <Form> allForms;
        public static int numOfForms = 0;
        public string Name { get; private set; }
        public int count = 0;//count of found books
        private List<Book> whatToChoose; //list of found books
        public Library(string name)
        {
            if (name != "")
                Name = name;
            else 
                throw new ArgumentNullException("You haven't inputed title of bank. Please, try again");
        }
        public void Open(string user, FormStateHandler TakeBookFromLibraryHandler, FormStateHandler ReturnBookToLibraryHandler,
            FormStateHandler openAccountHandler, FormStateHandler closeAccountHandler)
        {
            if (user == "")
                throw new Exception("Registration error");
            Form newForm = new Form(user);
            if (newForm == null)
                throw new Exception("Registration error");
            if (allForms == null)
            {
                allForms = new List<Form>();
                allForms.Add(newForm);
                numOfForms++;
            }
            else
            {
                allForms.Add(newForm);
                numOfForms++;
            }
            newForm.TookABookFromLibrary += TakeBookFromLibraryHandler;
            newForm.ReturnABookToALib += ReturnBookToLibraryHandler;
            newForm.Opened += openAccountHandler;
            newForm.Closed += closeAccountHandler;
            newForm.OnOpend();
        }
        //to add a book to form
        public void AddABook(Book book, int id)
        {
            Form form = FindForm(id);
            if (form == null)
                throw new Exception("The user isn't found");
            if (form._numOfBooks < 10)
            {
                form.AddABook(book); 
                RemoveFromLib(book);
            }
            else
                throw new Exception("You can only keep 10 instances in your form");
        }
        public Book ChooseABookFromLibraryList(int i)
        {
            if (i + 1 > count || i < 0)
                throw new ArgumentException("Invalid index. Try Again");
            return whatToChoose[i];
        }
        public Book ChooseABookFromFormList(int i, int id)
        {
            Form form = FindForm(id);
            if (form == null)
                throw new Exception("The user isn't found");
            if (i + 1 > form._numOfBooks || i < 0)
                throw new ArgumentException("Invalid index. Try Again");
            return whatToChoose[i];
        }
        public void RemoveFromLib(Book book)
        {
            for (int i = 0; i < Books.numOfBooks; i++)
            {
                if (Books.books[i].Title == book.Title)
                {
                    Books.RemoveBookFromLib(i); 
                    i = Books.numOfBooks - 1;
                }
            }
        }
        public Form FindForm(int id)
        {
            if (id > numOfForms || id < 1)
                throw new ArgumentException("Form with such num doesn't exist");
            if (numOfForms > 0 && allForms != null)
            {
                for (int i = 0; i < numOfForms; i++)
                {
                    if (allForms.Count > i )
                    {
                        if (allForms[i].Name != "")
                        {
                            if (allForms[i].Id == id)
                            {
                                return allForms[i];
                            }

                        }
                    }
                    else
                        throw new Exception("Form is not found");
                }
                throw new Exception("Form is not found");
            }
            else
            {
                throw new NullReferenceException("Impossible action. Form is not opened");
            }
        }
        public void DeleteABook1(Book book, int id)
        {
            Form form = FindForm(id);
            if (form == null)
                throw new Exception("The user was not found");
            form.DeleteABook(book);
        }
        public void Close(int id)
        {
            Form form = FindForm(id);
            if (form == null)
                throw new Exception("The user was not found");
            form.Close();
            if (allForms.Count <= 1) //щоб почати відлік 
            {
                allForms = null;
            }
            else
            {
                allForms[id - 1] = new Form("");
                Form.counter--;
            }
        }
        public void FindBookByTitle(string title) //треба знайти всі книги з подібною назвою
        {
            if (title == "")
                throw new NullReferenceException("An empty field is specified. Try again");
            List<Book> books1 = new List<Book>();//книги які задовольняють пошуку разом з всіма екземплярами
            whatToChoose = new List<Book>();//книги які задовольняють пошуку тільки в одному екземплярі
            int num = 0; 
            for (int i = 0; i < Books.numOfBooks; i++)
            {
                Book book = Books.FindByTitle(title, i, out int index);
                if (book != null)
                {
                    books1.Add(book);
                    i = index;
                    num++;
                }
            }
            if (num == 0)
                throw new Exception("There is not any book with such attributes.");
            int ind = 0;
            whatToChoose.Add(books1[ind]);
            for (int i = 1; i < num; i++)
            {
                if (books1[ind].Author != books1[i].Author||books1[ind].Theme!=books1[i].Theme)
                {
                    whatToChoose.Add(books1[i]);
                }
                ind++;
            }
            for (int i = 0; i < whatToChoose.Count; i++)
            {
                whatToChoose[i].Show1Book(i);
            }
            count = whatToChoose.Count;
        }
        public void FindBookByAuthor(string author)
        {
            if (author == "")
                throw new NullReferenceException("An empty field is specified. Try again");
            List<Book> books = new List<Book>();//books which satisfy the search just in 1 instance
            whatToChoose = new List<Book>();//books which satisfy the search just in 1 instance
            int num = 0; //count of same books
            for (int i = 0; i < Books.numOfBooks; i++)
            {
                Book book = Books.FindByAuthor(author, i, out int index);
                if (book != null)
                {
                    books.Add(book);
                    i = index;
                    num++;
                }
            }
            if (num == 0)
                throw new Exception("There is not any book with such attributes.");
            int ind = 0;
            whatToChoose.Add(books[ind]);
            for (int i = 1; i < num; i++)
            {
                if (books[ind].Title != books[i].Title || books[ind].Theme != books[i].Theme)
                {
                    whatToChoose.Add(books[i]);
                }
                ind++;
            }
            for (int i = 0; i < whatToChoose.Count; i++)
            {
                whatToChoose[i].Show1Book(i);
            }
            count = whatToChoose.Count;
        }
        public void FindBookByTheme(string theme)
        {
            if (theme == "")
                throw new NullReferenceException("An empty field is specified. Try again");
            List<Book> books = new List<Book>();
            whatToChoose = new List<Book>();//books which satisfy the search just in 1 instance
            int num = 0; 
            for (int i = 0; i < Books.numOfBooks; i++)
            {
                Book book = Books.FindByTheme(theme, i, out int index);
                if (book != null)
                {
                    books.Add(book);
                    i = index;
                    num++;
                }
            }
            if (num == 0)
                throw new Exception("There is not any book with such attributes.");
            int ind = 0;
            whatToChoose.Add(books[ind]);
            for (int i = 1; i < num; i++)
            {
                if (books[ind].Title != books[i].Title || books[ind].Author != books[i].Author)
                {
                    whatToChoose.Add(books[i]);
                }
                ind++;
            }
            for (int i = 0; i < whatToChoose.Count; i++)
            {
                whatToChoose[i].Show1Book(i);
            }
            count = whatToChoose.Count;
        }
        public void ShowForm(int id)
        {
            Form form = FindForm(id);
            if (form == null)
                throw new Exception("The user was not found");
            else
                Console.WriteLine($"\t\t\tForm of user: {form.Name}");
            whatToChoose = new List<Book>();

            for (int i = 0; i < allForms.Count; i++)
            {
                if (allForms[i].Id == id)
                {
                    allForms[i].ShowForm();
                    for (int j = 0; j < allForms[i]._numOfBooks; j++)
                    {
                        whatToChoose.Add(allForms[i].list[j]);
                    }
                }
            }
        }
    }
}
