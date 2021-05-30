namespace LibraryLib
{
    public delegate void FormStateHandler(object sender, FormEventArgs e);
    public class FormEventArgs 
    {
        public string Message { get; private set; }
        public FormEventArgs(string mes)
        {
            Message = mes;
        }
    }
}