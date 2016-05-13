using System;
using System.Collections.Generic;

namespace PhoneBook
{
    class Program
    {
        static void Main(string[] args)
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
