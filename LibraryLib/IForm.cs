using System;

namespace LibraryLib
{
    public interface IForm
    {
        void AddABook(Book book);
        void DeleteABook(Book book);
        void ShowForm();
        void Close();
    }
}
