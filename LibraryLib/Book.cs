using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibraryLib
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Theme { get; set; }
        public DateTime now;
        public Book(int id)
        {
            string path = @"C:\Users\Софія\source\repos\Library_course3\LibraryLib\BooksFile.txt";
            try
            {
                using StreamReader sr = new StreamReader(path, Encoding.Default);
                string[] atribures = new string[3]; //title, author, theme
                string line;
                line = File.ReadLines(path).Skip(id).First();
                string[] words = line.Split(',');
                Title = words[0];
                Author = words[1];
                Theme = words[2];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public Book(string title)
        {
            for (int i = 0; i < Books.copy.Length; i++)
            {
                if (Books.copy[i].Title == title)
                {
                    Title = title;
                    Author = Books.copy[i].Author;
                    Theme = Books.copy[i].Theme;
                    i = Books.copy.Length - 1;
                }
            }
        }
        public void Show1Book(int i)
        {
            if (Title == null || Author == null || Theme == null)
                throw new Exception("Book isn't defined");
            Console.WriteLine($"№{i+1}\nTitle: {Title}\nAuthor: {Author}\nTheme: {Theme}");
        }
    }
}
