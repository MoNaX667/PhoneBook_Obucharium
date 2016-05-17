namespace PhoneBook
{
    using System;

    internal class Program
    {
        public static void Main()
        {
            string filename = "ContactList.txt";

            // Console customization
            Console.Title = "Phone Book";
            Console.CursorVisible = false;
            
            // Set window params
            Console.WindowHeight = 40;
            Console.WindowWidth = 100;

            // StartApp
            PhoneBookEngine.StartWork(filename);
        }
    }
}
