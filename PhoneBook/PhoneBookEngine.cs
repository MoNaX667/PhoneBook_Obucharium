namespace PhoneBook
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal static class PhoneBookEngine
    {
        /// <summary>
        /// Starting application work
        /// </summary>
        public static Error StartWork(string filename)
        {
            int contactStartIndex = 0;
            int contactEndIndex = 0;
            PhoneBook myPhoneBook = new PhoneBook();
            List<Person> searchList = new List<Person>();
            ConsoleDateBuilder.ClearTerminalFrame();

            // If file not found work will start with clean contact list
            TryReadPhoneBook(filename, myPhoneBook);
            myPhoneBook.Sort(1);
            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);

            // App loop
            while (true)
            {
                ConsoleDateBuilder.ClearTerminalFrame();

                switch (WaitUserCommand())
                {
                    // Add new contact
                    case Commands.Add:
                        {
                            Add(ref myPhoneBook);
                            ConsoleDateBuilder.ClearInfoFrame();

                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;

                    // Delete By id
                    case Commands.DeleteById:
                        {
                            ConsoleDateBuilder.ClearTerminalFrame();
                            DeleteById(ref myPhoneBook);

                            ConsoleDateBuilder.ClearInfoFrame();
                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;

                    // Move to next page
                    case Commands.NextPage:
                        {
                            MoveNextPage(ref myPhoneBook, ref contactStartIndex, 
                                ref contactEndIndex);
                        }
                        break;

                    // Move to Previosly page
                    case Commands.PrevioslyPage:
                        {
                            MovePrevioslyPage(ref myPhoneBook, ref contactStartIndex,
                                ref contactEndIndex);
                        }
                        break;

                    // Search
                    case Commands.Search:
                        {
                            int startPos = 0;

                            // Search target information
                            ConsoleDateBuilder.ClearTerminalFrame();
                            Search(ref myPhoneBook, ref searchList);

                            // Craete new phoneBook list
                            PhoneBook temp = new PhoneBook(searchList);
                            var endPos = searchList.Count;

                            // Output information
                            ConsoleDateBuilder.ClearInfoFrame();
                            ConsoleDateBuilder.LoadStartInfo(temp, 
                                ref startPos, ref endPos);
                            ConsoleDateBuilder.LoadCommandInfo(temp);
                        }
                        break;

                    // Sort
                    case Commands.Sort:
                        {
                            Sort(ref myPhoneBook);

                            // Load previosly page with new index
                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;

                    // Exit from app
                    case Commands.ExitAndSaveChanged:
                        {
                            SaveFile(filename, myPhoneBook);
                            return Error.None;
                        }

                    // Return to start page
                    case Commands.ReturnToStartPage:
                        {
                            // Load previosly page with new index
                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;

                    // Clear list
                    case Commands.ClearList:
                        {
                            ClearList(filename,ref myPhoneBook);

                            // Load previosly page with new index
                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;

                    // Create test list
                    case Commands.CreateTestList:
                        {
                            CreateTestContactList(filename, ref myPhoneBook);

                            // Load previosly page with new index
                            LoadGeneralPage(myPhoneBook, ref contactStartIndex, ref contactEndIndex);
                        }
                        break;
                    default:
                        {
                            continue;
                        }
                }
            }
        }

        /// <summary>
        /// Output first page with command menu
        /// </summary>
        /// <param name="phoneBook"></param>
        /// <param name="contactStartIndex"></param>
        /// <param name="contactEndIndex"></param>
        private static void LoadGeneralPage(
            PhoneBook phoneBook, 
            ref int contactStartIndex,
            ref int contactEndIndex)
        {
            // Common frame
            ConsoleFrameBuilder.DrawFrame(0, 0, 27, 98);

            // Info frame
            ConsoleFrameBuilder.DrawInfoFrame(1, 1, 26, 64);
            ConsoleDateBuilder.LoadStartInfo(phoneBook, ref contactStartIndex,
                ref contactEndIndex);

            // Command frame
            ConsoleFrameBuilder.DrawCommandFrame(1, 72, 26, 96);
            ConsoleDateBuilder.LoadCommandInfo(phoneBook);
           
            // Terminal Frame
            ConsoleFrameBuilder.DrawTerminalFrame(28, 0, 38, 98);
        }   
        
        private static void TryReadPhoneBook(string filename, PhoneBook myPhoneBook)
        {
            // If file not found you start with clean list
            myPhoneBook.LoadContactList(filename);
        }

        private static void SaveFile(string filename, PhoneBook myPhoneBook)
        {
            myPhoneBook.SaveContactList(filename);
        }

        /// <summary>
        /// Wait command input
        /// </summary>
        /// <returns></returns>
        private static Commands WaitUserCommand()
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            // Output Start position
            Console.SetCursorPosition(3, 31);
            Console.CursorVisible = true;
            Console.Write(">> ");
            string tempCommand = Console.ReadLine();
            Commands userCommand = Commands.ReturnToStartPage;

            // Check user command
            for (int i = 0; i <= 10; i++)
            {
                if (tempCommand != null && ((Commands)i).ToString().ToLower()
                    == tempCommand.ToLower())
                {
                    userCommand = (Commands)i;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = oldColor;
                    return userCommand;
                }
            }

            Console.SetCursorPosition(3, 32);
            Console.Write("Command not identified");
            Console.SetCursorPosition(3, 33);
            Console.ReadKey();
            Console.CursorVisible = false;
            Console.ForegroundColor = oldColor;
            return userCommand;
        }

        /// <summary>
        /// Add some element in PhoneBook
        /// </summary>
        /// <param name="myPhoneBook">Reference </param>
        private static void Add(ref PhoneBook myPhoneBook)
        {
            string middleName = "";

            // Clear user teminal
            ConsoleDateBuilder.ClearTerminalFrame();
            Console.SetCursorPosition(3, 29);
            Console.Write("Input surname >> ");

            // Input surName
            var surName = Console.ReadLine();

            // Check surName
            foreach (char value in middleName)
            {
                if (!char.IsLetter(value))
                {
                    Console.SetCursorPosition(3, 30);
                    Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                    Console.ReadKey();
                }
            }

            // Input foreName
            ConsoleDateBuilder.ClearTerminalFrame();
            Console.SetCursorPosition(3, 29);
            Console.Write("Input foreName >> ");

            var foreName = Console.ReadLine();

            // Check foreName
            if (foreName != null)
            {
                foreach (char value in foreName)
                {
                    if (!char.IsLetter(value))
                    {
                        Console.SetCursorPosition(3, 30);
                        Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                        Console.ReadKey();
                    }
                }

                // Input foreName
                ConsoleDateBuilder.ClearTerminalFrame();
                Console.SetCursorPosition(3, 29);
                Console.Write("Input middleName >> ");
                middleName = Console.ReadLine();

                // Check middleName
                if (middleName != null)
                {
                    foreach (char value in middleName)
                    {
                        if (!char.IsLetter(value))
                        {
                            Console.SetCursorPosition(3, 30);
                            Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                            Console.ReadKey();
                        }
                    }

                    // Input phoneNumber
                    ConsoleDateBuilder.ClearTerminalFrame();
                    Console.SetCursorPosition(3, 29);
                    Console.Write("Input phoneNumber (in second format x-xxx-xxx-xxxx) >> ");
                    var phoneNumber = Console.ReadLine();

                    // PhoneNumber Check
                    if (!CheckPhoneNumber(phoneNumber))
                    {
                        Console.SetCursorPosition(3, 30);
                        Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                        Console.ReadKey();
                        return;
                    }

                    if (!myPhoneBook.Add(surName, foreName, middleName, phoneNumber))
                    {
                        Console.SetCursorPosition(3, 30);
                        Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                        Console.ReadKey();
                    }
                }
            }

            myPhoneBook.Sort(1);
        }

        /// <summary>
        /// Delete phoneBook element by ID
        /// </summary>
        /// <param name="myPhoneBook"></param>
        private static void DeleteById(ref PhoneBook myPhoneBook)
        {
            Console.SetCursorPosition(3, 29);
            Console.Write("Input id >> ");
            int contactId;

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
        private static void MoveNextPage(
            ref PhoneBook myPhoneBook,
            ref int contactStartIndex,
            ref int contactEndIndex)
        {
            if (contactStartIndex <= 0) throw new ArgumentOutOfRangeException(nameof(contactStartIndex));
            ConsoleDateBuilder.ClearTerminalFrame();
            ConsoleDateBuilder.ClearInfoFrame();
            contactStartIndex = contactEndIndex;

            // Try get next index
            if ((myPhoneBook.GetLenght - contactStartIndex) < 20)
            {
                contactEndIndex += myPhoneBook.GetLenght - contactStartIndex;
            }
            else
            {
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
        private static void MovePrevioslyPage(
            ref PhoneBook myPhoneBook, 
            ref int contactStartIndex,
            ref int contactEndIndex)
        {
            ConsoleDateBuilder.ClearTerminalFrame();
            ConsoleDateBuilder.ClearInfoFrame();

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
        private static void Sort(ref PhoneBook myPhoneBook)
        {
            ConsoleDateBuilder.ClearTerminalFrame();

            int typeOfSorting;

            Console.SetCursorPosition(3, 29);
            Console.Write("Input index of sorting (1-bySurName; 2-ByForeName; 3-ByMiddleName;" +
            "4-ByPhoneNumber) >> ");

            if (int.TryParse(Console.ReadLine(), out typeOfSorting))
            {
                myPhoneBook.Sort(typeOfSorting);
            }
            else
            {
                Console.SetCursorPosition(3, 29);
                Console.Write("Bad sorting index... operation will be crashed");
            }
        }

        /// <summary>
        /// Search some contact by some params
        /// </summary>
        private static void Search(
            ref PhoneBook myPhoneBook,
            ref List<Person> searchList)
        {
            int indexOfSearchParam;

            // Clear user teminal
            ConsoleDateBuilder.ClearTerminalFrame();
            Console.SetCursorPosition(3, 29);
            Console.Write("Input index of param for searching (1-surName; 2- foreName;" +
                "3- middleName; 4- phoneNumber) >> ");


            if (!int.TryParse(Console.ReadLine(), out indexOfSearchParam))
            {
                Console.SetCursorPosition(3, 30);
                Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                return;
            }

            string temp;

            switch ((SortEnum)indexOfSearchParam-1)
            {
                case SortEnum.BySurName:
                {
                        // surName search
                        Console.SetCursorPosition(3, 30);
                        Console.Write("Input target surName >> ");

                        // If string is null or Empthy than break 
                        if (!string.IsNullOrEmpty(temp = Console.ReadLine()))
                        {
                            // searchList contains result
                            searchList = myPhoneBook.SearchBySurname(temp);
                            return;
                        }
                        else
                        {
                            Console.SetCursorPosition(3, 31);
                            Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                            return;
                        }
                    }
                case SortEnum.ByForeName:
                {
                        // foreName search
                        Console.SetCursorPosition(3, 30);
                        Console.Write("Input target foreName >> ");

                        // If string is null or Empthy than break 
                        if (!string.IsNullOrEmpty(temp = Console.ReadLine()))
                        {
                            // searchList contains result
                            searchList = myPhoneBook.SearchByForeName(temp);
                            return;
                        }
                        else
                        {
                            Console.SetCursorPosition(3, 31);
                            Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                            return;
                        }
                    }
                case SortEnum.ByMiddleName:
                {
                        // middleName search
                        Console.SetCursorPosition(3, 30);
                        Console.Write("Input target middleName >> ");

                        // If string is null or Empthy than break 
                        if (!string.IsNullOrEmpty(temp = Console.ReadLine()))
                        {
                            // searchList contains result
                            searchList = myPhoneBook.SearchByMiddleName(temp);
                            return;
                        }
                        else
                        {
                            Console.SetCursorPosition(3, 31);
                            Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                            return;
                        }
                    }
                case SortEnum.ByPhoneNumber:
                {
                        // phoneNumber search
                        Console.SetCursorPosition(3, 30);
                        Console.Write("Input target phoneNumber (x-xxx-xxx-xxxx) >> ");

                        // If string is null or Empthy than break 
                        if (!string.IsNullOrEmpty(temp = Console.ReadLine()))
                        {
                            // searchList contains result
                            searchList = myPhoneBook.SearchByPhone(temp);
                            return;
                        }
                        else
                        {
                            Console.SetCursorPosition(3, 31);
                            Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                            return;
                        }
                    }
                default:
                {
                    break;
                }
            }

            Console.SetCursorPosition(3, 30);
            Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
        }

        private static bool CheckPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Any(temp => (!char.IsDigit(temp) && (temp != '-'))))
            {
                return false;
            }

            string[] blocksOfPhoneNumber = phoneNumber.Split('-');

            if (blocksOfPhoneNumber.Length < 4)
            {
                return false;
            }

            if ((blocksOfPhoneNumber[0].Length != 1) &&
                (blocksOfPhoneNumber[1].Length != 3) &&
                (blocksOfPhoneNumber[2].Length != 3) &&
                 (blocksOfPhoneNumber[3].Length != 4))
            {
                return false;
            }

            return true;
        }

        private static void CreateTestContactList(string filename, ref PhoneBook myPhoneBook)
        {
            RandomContactListCreator creator = new RandomContactListCreator();

            Console.SetCursorPosition(3, 29);
            Console.Write("Input count of test list >> ");
            int countTestList;

            if (!int.TryParse(Console.ReadLine(), out countTestList))
            {
                Console.SetCursorPosition(3, 30);
                Console.Write("Bad input. Operation crashed ... Press any key to return at terminal");
                return;
            }

            File.WriteAllLines(filename, creator.CreateContactList(countTestList));

            // If file not found work will start with clean contact list
            TryReadPhoneBook(filename, myPhoneBook);
            myPhoneBook.Sort(1);

            ConsoleDateBuilder.ClearInfoFrame();
            ConsoleDateBuilder.ClearTerminalFrame();
        }

        private static void ClearList(string filename, ref PhoneBook myPhoneBook)
        {
            myPhoneBook = new PhoneBook();

            ConsoleDateBuilder.ClearInfoFrame();
            ConsoleDateBuilder.ClearTerminalFrame();
        }
    }
}
