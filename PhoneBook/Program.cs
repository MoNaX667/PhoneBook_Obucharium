// <copyright file="Program.cs" company="Some Company">
// Copyright (c) Sprocket Enterprises. All rights reserved.
// </copyright>
// <author>Vitalit Belyakov</author>

namespace PhoneBook
{
    using System;

    /// <summary>
    /// Program class
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
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
