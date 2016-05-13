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
            ConsoleDateBuilder.clearTerminalFrame();

            // If file not found work will start with clean contact list
            TryReadPhoneBook(filename, myPhoneBook);
            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);

            // App loop
            while (true) {
                ConsoleDateBuilder.clearTerminalFrame();

                switch (WaitUserCommand()){
                    case Commands.Add: {
                            string surname = "", forename = "", middlename = "", phoneNumber = "";

                            // Clear user teminal
                            ConsoleDateBuilder.clearTerminalFrame();
                            Console.SetCursorPosition(3, 29);
                            Console.Write("Input surname >> ");

                            myPhoneBook.Add(surname,forename,middlename,phoneNumber);
                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;
                    case Commands.DeleteByID: {
                            ConsoleDateBuilder.clearTerminalFrame();
                            Console.SetCursorPosition(3, 29);
                            Console.Write("Input id >> ");
                            int contactId = 0;

                            // Try get id for delete operation
                            if (!int.TryParse(Console.ReadLine(), out contactId)){
                                Console.SetCursorPosition(3, 30);
                                Console.Write("Seceed");
                            }
                            // if id is not found show message
                            else if (Error.WrongId == myPhoneBook.RemoveContactById(contactId)) {
                                Console.SetCursorPosition(3, 30);
                                Console.Write("Wrong Id");
                            }

                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;
                    case Commands.NextPage: {
                            ConsoleDateBuilder.clearTerminalFrame();
                            ConsoleDateBuilder.infoFrameClear();
                            contactStartIndex = contactEndIndex;

                            // Try get next index
                            if ((myPhoneBook.GetLenght - contactStartIndex) < 20) {
                                contactEndIndex += myPhoneBook.GetLenght - contactStartIndex;
                            }
                            else {
                                contactEndIndex += 20;
                            }

                            // Load next page with new index
                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;
                    case Commands.PrevioslyPage: {
                            ConsoleDateBuilder.clearTerminalFrame();
                            ConsoleDateBuilder.infoFrameClear();

                            // Try get previosly index
                            if (contactStartIndex == 0){
                                break;
                            }
                            else if(contactEndIndex>20){
                                contactStartIndex-=20;
                                contactEndIndex -= 20;
                            }

                            // Load previosly page with new index
                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;
                    case Commands.Search:{
                            ConsoleDateBuilder.clearTerminalFrame();
                        }
                        break;
                    case Commands.Exit: {
                            return Error.None;
                        }
                        break;
                    case Commands.ReturnToStartPage: {
                            // Return to start page
                            ConsoleDateBuilder.clearTerminalFrame();
                            contactStartIndex = contactEndIndex = 0;
                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;
                    default: {
                            continue;
                        }
                }
            }

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

        /// <summary>
        /// Wait command input
        /// </summary>
        /// <returns></returns>
        private static Commands WaitUserCommand() {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            // Output Start position
            Console.SetCursorPosition(3, 31);
            Console.CursorVisible = true;
            Console.Write(">> ");
            string temp = Console.ReadLine();
            Commands userCommand=Commands.ReturnToStartPage;

            // Parse input string user command
            try{
                 userCommand= (Commands)Enum.Parse(typeof(Commands), temp);
            }
            catch (Exception excep){
                Console.SetCursorPosition(3, 32);
                Console.Write("Command not identified");
                Console.SetCursorPosition(3, 33);
                Console.ReadKey();
            }

            Console.CursorVisible = false;
            Console.ForegroundColor = oldColor;
            return userCommand ;
        }
    }
}
