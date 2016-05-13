using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    static class PhoneBookEngine
    {

        /// <summary>
        /// Starting application work
        /// </summary>
        public static Error StartWork(string filename){
            int contactStartIndex = 0;
            int contactEndIndex = 0;
            Contacts myPhoneBook = new Contacts();

            // If file not found work will start with clean contact list
            TryReadPhoneBook(filename, myPhoneBook);


            LoadGeneralPage(myPhoneBook, ref contactStartIndex,ref contactEndIndex);
            WaitUserCommand();

            return Error.None;
        }

        /// <summary>
        /// Output first page with command menu
        /// </summary>
        /// <param name="phoneBook"></param>
        private static void LoadGeneralPage(Contacts phoneBook,ref int contactStartIndex,
            ref int contactEndIndex) {
            // Common frame
            ConsoleFrameBuilder.DrawFrame(0,0,27,98);

            // Info frame
            ConsoleFrameBuilder.DrawInfoFrame(1, 1, 26, 64);
            ConsoleDateBuilder.LoadStartInfo(phoneBook,ref contactStartIndex,
                ref contactEndIndex);

            // Command frame
            ConsoleFrameBuilder.DrawCommandFrame(1, 72, 26, 96);
            ConsoleDateBuilder.LoadCommandInfo(phoneBook);
           
            // Terminal Frame
            ConsoleFrameBuilder.DrawTerminalFrame(28, 0, 38, 98);
        }   
        
        private static void TryReadPhoneBook(string filename,Contacts myPhoneBook){

        // If file not found app crashed
            if (myPhoneBook.LoadContactList(filename) == Error.FileNotFound)
            {
                Console.WriteLine("File with contacts not found");
                Console.ReadKey();
            }

        }

        private static string WaitUserCommand() {
            var oldColor = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(3, 31);
            Console.CursorVisible = true;
            Console.Write(">> ");
            string userCommand = Console.ReadLine();

            Console.ForegroundColor = oldColor;
            return userCommand;
        }
    }
}
