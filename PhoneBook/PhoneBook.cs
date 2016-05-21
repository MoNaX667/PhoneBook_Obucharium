namespace PhoneBook
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Contact list collection with some tool for work with it
    /// </summary>
    internal class PhoneBook
    {
        // Members
        private List<Person> contactList = new List<Person>();

        // C-tors
        /// <summary>
        /// Default c-tor
        /// </summary>
        public PhoneBook()
        {
        }

        /// <summary>
        /// Parametric c-tor
        /// </summary>
        /// <param name="searchList"></param>
        public PhoneBook(List<Person> searchList)
        {
            this.contactList = searchList;
        }

        public int GetLenght => this.contactList.Count;

        ////////////////////            Public           ////////////////////

        /// <summary>
        /// Add new string to contact book.
        /// If phone number format if wrong return false
        /// </summary>
        /// <param name="surName"> string sur name</param>
        /// <param name="foreName">string fore name</param>
        /// <param name="middleName">string middle name</param>
        /// <param name="phoneNumber">Phone number string must be in x-xxx-xxx-xxxx format</param>
        /// <returns>return false if phone number format is wrong</returns>
        public bool Add(string surName, string foreName, string middleName, string phoneNumber)
        {
            if (this.CheckPhoneNumberFormat(phoneNumber))
            {
                return false;
            }

            this.contactList.Add(new Person(surName, foreName, middleName, phoneNumber));

            return true;
        }

        /// <summary>
        /// Delete some element in contact book by id
        /// If id not founded return false
        /// </summary>
        /// <param name="id">Contact id . It's must be bigger than 0 and less 
        /// than max id in contact book</param>
        /// <returns></returns>
        public Error RemoveContactById(int id)
        {
            if (id > 0 && id <= this.contactList.Count)
            {
                this.contactList.RemoveAt(id - 1);
                return Error.None;
            }
            else
            {
                return Error.WrongId;
            }
        }

        /// <summary>
        /// Method must return list of string , than saved names and phone numbers seporated 
        /// by char '/'
        /// </summary>
        /// <returns>Error message</returns>
        public List<string> GetListOfContacts()
        {
            List<string> tempContactsList = new List<string>();

            foreach (Person value in contactList)
            {
                tempContactsList.Add(string.Format("{0}/{1}", value.Name, value.PhoneNumber));
            }

            return tempContactsList;
        }

        /// <summary>
        /// Load contact list
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Filename to reading</returns>
        public Error LoadContactList(string filename)
        {
            if (!File.Exists(filename))
            {
                return Error.FileNotFound;
            }

            using (FileStream contactStream = new FileStream(filename, FileMode.Open))
            using (StreamReader reader = new StreamReader(contactStream))
            {
                while (!reader.EndOfStream)
                {
                    var personParams = reader.ReadLine().Split(' ');
                    this.contactList.Add(new Person(personParams[0], personParams[1],
                        personParams[2], personParams[3]));
                }
            }

            return Error.None;
        }

        /// <summary>
        /// Save phoneBook to filename.txt file
        /// </summary>
        /// <param name="filename">Filename or full path</param>
        public void SaveContactList(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Person t in contactList)
                {
                    writer.WriteLine("{0} {1}", t.Name, t.PhoneNumber);
                }
            }
        }

        /// <summary>
        /// Sorting by surName
        /// </summary>
        /// <param name="surName"></param>
        /// <returns></returns>
        public List<Person> SearchBySurname(string surName)
        {
            List<Person> resultList = new List<Person>();

            foreach (var value in this.contactList)
            {
                if (value.SurName.Contains(surName))
                {
                    resultList.Add(value);
                }
            }

            return resultList;
        }

        /// <summary>
        /// Sorting by forename
        /// </summary>
        /// <param name="foreName"></param>
        /// <returns></returns>
        public List<Person> SearchByForeName(string foreName)
        {
            List<Person> resultList = new List<Person>();

            foreach (var value in this.contactList)
            {
                if (value.ForeName.Contains(foreName))
                {
                    resultList.Add(value);
                }
            }

            return resultList;
        }

        /// <summary>
        /// Sorting by middleName
        /// </summary>
        /// <param name="middleName"></param>
        /// <returns></returns>
        public List<Person> SearchByMiddleName(string middleName)
        {
            List<Person> resultList = new List<Person>();

            foreach (var value in this.contactList)
            {
                if (value.MiddleName.Contains(middleName))
                {
                    resultList.Add(value);
                }
            }

            return resultList;
        }

        /// <summary>
        /// Sorting by phoneNumber
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public List<Person> SearchByPhone(string phone)
        {
            List<Person> resultList = new List<Person>();

            foreach (var value in this.contactList)
            {
                if (value.PhoneNumber.Contains(phone))
                {
                    resultList.Add(value);
                }
            }

            return resultList;
        }

        /// <summary>
        /// Sort phoneBook by any of contact parametrs if type of sorting different from 
        /// inside  sorting crashed
        /// </summary>
        /// <param name="type">type of operation 1- By surName; 2- By foreName; 3- By middleName;
        /// 4- By phoneNumber
        /// </param>
        public void Sort(int type)
        {
            if ((type < 1) || (type > 4))
            {
            }
            else if (type == 1)
            {
                this.contactList = this.contactList.OrderBy(element => element.SurName).ToList();
            }
            else if (type == 2)
            {
                this.contactList = this.contactList.OrderBy(n => n.ForeName).ToList();
            }
            else if (type == 3)
            {
                this.contactList = this.contactList.OrderBy(n => n.MiddleName).ToList();
            }
            else if (type == 4)
            {
                this.contactList = this.contactList.OrderBy(n => n.PhoneNumber).ToList();
            }
        }

        ////////////////////       Private         ///////////////////////////

        /// <summary>
        /// Check string with phone number for phone format
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <returns></returns>
        private bool CheckPhoneNumberFormat(string phoneNumber)
        {
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (!char.IsDigit(phoneNumber[i]) || phoneNumber[i] != '-')
                {
                    return false;
                }
            }

            return true;
        }
    }
}