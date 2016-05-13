using System;
using System.Collections.Generic;
using System.IO;

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
            List<Person> searchList = new List<Person>();
            ConsoleDateBuilder.clearTerminalFrame();

            // If file not found work will start with clean contact list
            TryReadPhoneBook(filename, myPhoneBook);
            myPhoneBook.Sort(1);
            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);

            // App loop
            while (true) {
                ConsoleDateBuilder.clearTerminalFrame();

                switch (WaitUserCommand()){
                    // Add new contact
                    case Commands.Add: {
                            Add(ref myPhoneBook);

                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;
                        // Delete By id
                    case Commands.DeleteByID: {
                            ConsoleDateBuilder.clearTerminalFrame();
                            DeleteByID(ref myPhoneBook);

                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;
                        // Move to next page
                    case Commands.NextPage: {
                            MoveNextPage(ref myPhoneBook, ref contactStartIndex, 
                                ref contactEndIndex);
                        }
                        break;
                        // Move to Previosly page
                    case Commands.PrevioslyPage: {
                            MovePrevioslyPage(ref myPhoneBook, ref contactStartIndex,
                                ref contactEndIndex);
                        }
                        break;
                        // Search
                    case Commands.Search:{
                            ConsoleDateBuilder.clearTerminalFrame();
                            
                        }
                        break;
                    // Sort
                    case Commands.Sort: {
                            Sort(ref myPhoneBook);

                            // Load previosly page with new index
                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;
                        // Exit from app
                    case Commands.ExitAndSaveChanged: {
                            SaveFile(filename, myPhoneBook);
                            return Error.None;
                        }
                        break;
                        // Return to start page
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

            // If file not found you start with clean list
            myPhoneBook.LoadContactList(filename);
        }

        private static void SaveFile(string filename,Contacts myPhoneBook){
            myPhoneBook.SaveContactList(filename);
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


        /// <summary>
        /// Add some element in PhoneBook
        /// </summary>
        /// <param name="myPhoneBook">Reference </param>
        private static void Add(ref Contacts myPhoneBook) {
            string surName = "", foreName = "", middleName = "", phoneNumber = "";

            // Clear user teminal
            ConsoleDateBuilder.clearTerminalFrame();
            Console.SetCursorPosition(3, 29);
            Console.Write("Input surname >> ");

            // Input surName
            surName = Console.ReadLine();

            // Check surName
            foreach (char value in middleName)
            {
                if (!char.IsLetter(value))
                {
                    Console.SetCursorPosition(3, 30);
                    Console.Write("Bad input. Operation crashed");
                    Console.ReadKey();
                }
            }

            // Input foreName
            ConsoleDateBuilder.clearTerminalFrame();
            Console.SetCursorPosition(3, 29);
            Console.Write("Input foreName >> ");

            foreName = Console.ReadLine();

            // Check foreName
            foreach (char value in foreName)
            {
                if (!char.IsLetter(value))
                {
                    Console.SetCursorPosition(3, 30);
                    Console.Write("Bad input. Operation crashed");
                    Console.ReadKey();
                }
            }

            // Input foreName
            ConsoleDateBuilder.clearTerminalFrame();
            Console.SetCursorPosition(3, 29);
            Console.Write("Input middleName >> ");
            middleName = Console.ReadLine();

            // Check middleName
            foreach (char value in middleName)
            {
                if (!char.IsLetter(value))
                {
                    Console.SetCursorPosition(3, 30);
                    Console.Write("Bad input. Operation crashed");
                    Console.ReadKey();
                }
            }

            // Input phoneNumber
            ConsoleDateBuilder.clearTerminalFrame();
            Console.SetCursorPosition(3, 29);
            Console.Write("Input phoneNumber (in second format x-xxx-xxx-xxxx) >> ");
            phoneNumber = Console.ReadLine();

            if (!myPhoneBook.Add(surName, foreName, middleName, phoneNumber)) {
                Console.SetCursorPosition(3, 30);
                Console.Write("Bad input. Operation crashed");
                Console.ReadKey();
            }

            myPhoneBook.Sort(1);
        }

        /// <summary>
        /// Delete phoneBook element by ID
        /// </summary>
        /// <param name="myPhoneBook"></param>
        private static void DeleteByID(ref Contacts myPhoneBook) {
            Console.SetCursorPosition(3, 29);
            Console.Write("Input id >> ");
            int contactId = 0;

            // Try get id for delete operation
            if (!int.TryParse(Console.ReadLine(), out contactId))
            {
                Console.SetCursorPosition(3, 30);
                Console.Write("Seceed");
            }
            // if id is not found show message
            else if (Error.WrongId == myPhoneBook.RemoveContactById(contactId))
            {
                Console.SetCursorPosition(3, 30);
                Console.Write("Wrong Id");
            }
        }

        /// <summary>
        /// Move next page
        /// </summary>
        /// <param name="myPhoneBook"></param>
        /// <param name="contactStartIndex"></param>
        /// <param name="contactEndIndex"></param>
        private static void MoveNextPage(ref Contacts myPhoneBook,ref int contactStartIndex,
            ref int contactEndIndex) {
            ConsoleDateBuilder.clearTerminalFrame();
            ConsoleDateBuilder.infoFrameClear();
            contactStartIndex = contactEndIndex;

            // Try get next index
            if ((myPhoneBook.GetLenght - contactStartIndex) < 20)
            {
                contactEndIndex += myPhoneBook.GetLenght - contactStartIndex;
            }
            else {
                contactEndIndex += 20;
            }

            // Load next page with new index
            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
        }

        /// <summary>
        /// Move to previosly page
        /// </summary>
        /// <param name="myPhoneBook"></param>
        /// <param name="contactStartIndex"></param>
        /// <param name="contactEndIndex"></param>
        private static void MovePrevioslyPage(ref Contacts myPhoneBook,ref int contactStartIndex,
            ref int contactEndIndex) {
            ConsoleDateBuilder.clearTerminalFrame();
            ConsoleDateBuilder.infoFrameClear();

            // Try get previosly index
            if (contactStartIndex == 0)
            {
                return;
            }
            // if index can be taken , get new index and load new page
            else if (contactEndIndex > 20)
            {
                contactStartIndex -= 20;
                contactEndIndex -= 20;
            }

            // Load previosly page with new index
            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
        }

        /// <summary>
        /// Sorting phone book with some params 1-Surname, 2-ForName, 3-MiddleName, 4-PhoneNumber
        /// </summary>
        /// <param name="myPhoneBook"></param>
        private static void Sort(ref Contacts myPhoneBook) {
            ConsoleDateBuilder.clearTerminalFrame();

            int typeOfSorting = 0;

            Console.SetCursorPosition(3, 29);
            Console.Write("Input index of sorting (1-bySurName; 2-ByForeName; 3-ByMiddleName;"+
            "4-ByPhoneNumber) >> ");

            if (int.TryParse(Console.ReadLine(), out typeOfSorting)){
                myPhoneBook.Sort(typeOfSorting);
            }
            else {
                Console.SetCursorPosition(3, 29);
                Console.Write("Bad sorting index... operation will be crashed");
            }
            
        }

        private static void SaveFile() {

        }
    }
}
