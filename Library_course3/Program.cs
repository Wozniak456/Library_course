using LibraryLib;
using System;

namespace Library_course3
{
    class Program
    {
        static void Main(string[] args)
        {
            Library lib = new Library("BAnk");
            //_ = new Books();
            bool alive = true;
            while (alive)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. To registrate\t2. To search a book\t3. To return a book to library\n" +
                    "4. To delete your form\t5. Show form\t\t6. To close a programm\t\t");
                Console.Write("Choose an action: ");
                Console.ResetColor();
                try
                {
                    int command = int.Parse(Console.ReadLine());
                    if (command < 1 || command > 6)
                        throw new ArgumentException("Please choose number from 1 to 6");
                    switch (command)
                    {
                        case 1:
                            OpenForm(lib);
                            break;
                        case 2:
                            Search(lib);
                            break;
                        case 3:
                            ReturnABook(lib);
                            break;
                        case 4:
                            CloseForm(lib);
                            break;
                        case 5:
                            ShowForm(lib);
                            break;
                        case 6:
                            alive = false;
                            continue;
                        
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.WriteLine("Press any key..");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (NullReferenceException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue..");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private static void OpenForm(Library lib)
        {
            Console.Write("Input your name: ");
            string name = Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            lib.Open(name, TakeBookFromLibraryHandler, ReturnBookToLibraryHandler, openAccountHandler, closeAccountHandler);
            Console.ResetColor();
        }
        private static void ReturnABook(Library lib)
        {
            Console.WriteLine("Input your id");
            int id = int.Parse(Console.ReadLine());
            try
            {
                lib.FindForm(id);
                lib.ShowForm(id);
                Console.Write("\nInput a num of book, you want to return: ");
                int num = int.Parse(Console.ReadLine());
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                lib.DeleteABook1(lib.ChooseABookFromFormList(num - 1, id), id);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine("Press any key to continue..");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private static void CloseForm(Library lib)
        {
            Console.WriteLine("Input your id");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            lib.Close(id);
            Console.ResetColor();
        }
        private static void ShowForm(Library lib)
        {
            Console.WriteLine("Input your id");
            int id = int.Parse(Console.ReadLine());
            try
            {
                Console.Clear();
                lib.ShowForm(id);
                Console.WriteLine("\nPress any key to continue..");
                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue..");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private static void Search(Library lib)
        {
            Console.WriteLine("How do you want to search a book?\n1 - by title\t2 - by author\t3 - by theme");
            int choice = int.Parse(Console.ReadLine());
            if (choice > 3 || choice < 0)
                Console.WriteLine("Please choose 1, 2 or 3");
            while (choice != 1 && choice != 2 && choice != 3)
            {
                Console.WriteLine("How do you want to search a book?\n1 - by title\t2 - by author\t3 - by theme");
                choice = int.Parse(Console.ReadLine());
                if (choice > 3 || choice < 0)
                    Console.WriteLine("Please choose 1, 2 or 3");
            }
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Input a title");
                    string title = Console.ReadLine();
                    lib.FindBookByTitle(title);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{lib.count} book(-s) found");
                    Console.ResetColor();
                    Console.WriteLine("Do you want to add any book to your form?\n1 - yes\t2 - no");
                    int choice2 = int.Parse(Console.ReadLine());
                    if (choice2 > 2 || choice2 < 0)
                        Console.WriteLine("Please choose 1 or 2");
                    while (choice2 != 1 && choice2 != 2)
                    {
                        Console.WriteLine("Do you want to add any book to your form?\n1 - yes\t2 - no");
                        choice2 = int.Parse(Console.ReadLine());
                        if (choice2 > 2 || choice2 < 0)
                            Console.WriteLine("Please choose 1 or 2");
                    }
                    switch (choice2)
                    {
                        case 1:
                            try
                            {
                                Console.WriteLine("Input your id: ");
                                int id_ = int.Parse(Console.ReadLine());
                                lib.FindForm(id_);
                                Console.Write("input a num of the book: ");
                                int num = int.Parse(Console.ReadLine());
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                lib.AddABook(lib.ChooseABookFromLibraryList(num - 1), id_);
                                Console.ResetColor();
                            }
                            catch (ArgumentException ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }
                            catch (NullReferenceException ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }
                            break;
                        case 2:
                            Console.Clear();
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine("Input an author");
                    string author = Console.ReadLine();
                    lib.FindBookByAuthor(author);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{lib.count} book(-s) found");
                    Console.ResetColor();
                    Console.WriteLine("Do you want to add any book to your form?\n1 - yes\t2 - no");
                    int choice3 = int.Parse(Console.ReadLine());
                    if (choice3 > 2 || choice3 < 0)
                        Console.WriteLine("Please choose 1 or 2");
                    while (choice3 != 1 && choice3 != 2)
                    {
                        Console.WriteLine("Do you want to add any book to your form?\n1 - yes\t2 - no");
                        choice3 = int.Parse(Console.ReadLine());
                        if (choice3 > 2 || choice3 < 0)
                            Console.WriteLine("Please choose 1 or 2");
                    }
                    switch (choice3)
                    {
                        case 1:
                            try
                            {
                                Console.WriteLine("Input your id: ");
                                int id_ = int.Parse(Console.ReadLine());
                                lib.FindForm(id_);
                                Console.Write("input a num of the book: ");
                                int num = int.Parse(Console.ReadLine());
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                lib.AddABook(lib.ChooseABookFromLibraryList(num - 1), id_);
                                Console.ResetColor();
                            }
                            catch (ArgumentException ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }
                            catch (NullReferenceException ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }
                            break;
                        case 2:
                            Console.Clear();
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine("Input a theme");
                    string theme = Console.ReadLine();
                    lib.FindBookByTheme(theme);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{lib.count} book(-s) found");
                    Console.ResetColor();
                    Console.WriteLine("Do you want to add any book to your form?\n1 - yes\t2 - no");
                    int choice4 = int.Parse(Console.ReadLine());
                    if (choice4 > 2 || choice4 < 0)
                        Console.WriteLine("Please choose 1 or 2");
                    while (choice4 != 1 && choice4 != 2)
                    {
                        Console.WriteLine("Do you want to add any book to your form?\n1 - yes\t2 - no");
                        choice4 = int.Parse(Console.ReadLine());
                        if (choice4 > 2 || choice4 < 0)
                            Console.WriteLine("Please choose 1 or 2");
                    }
                    switch (choice4)
                    {
                        case 1:
                            try
                            {
                                Console.WriteLine("Input your id: ");
                                int id_ = int.Parse(Console.ReadLine());
                                lib.FindForm(id_);
                                Console.Write("input a num of the book: ");
                                int num = int.Parse(Console.ReadLine());
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                lib.AddABook(lib.ChooseABookFromLibraryList(num - 1), id_);
                                Console.ResetColor();
                            }
                            catch (ArgumentException ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }
                            catch (NullReferenceException ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }
                            break;
                        case 2:
                            Console.Clear();
                            break;
                    }
                    break;
            }
        }

        private static void openAccountHandler(object sender, FormEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void closeAccountHandler(object sender, FormEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void ReturnBookToLibraryHandler(object sender, FormEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void TakeBookFromLibraryHandler(object sender, FormEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
