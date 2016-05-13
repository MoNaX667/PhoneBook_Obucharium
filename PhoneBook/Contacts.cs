using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhoneBook
{
    class Contacts
    {
        // Members
        private List<Person> myContacts = new List<Person>();

        public int GetLenght {
            get {
                return myContacts.Count;
            }
        }

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
        public bool Add(string surName, string foreName, string middleName, string phoneNumber) {

            if (checkPhoneNumberFormat(phoneNumber)) {
                return false;
            } 

            myContacts.Add(new Person(surName, foreName, middleName, phoneNumber));

            return true;
        }

        /// <summary>
        /// Delete some element in contact book by id
        /// If id not founded return false
        /// </summary>
        /// <param name="id">Contact id . It's must be bigger than 0 and less 
        /// than max id in contact book</param>
        /// <returns></returns>
        public Error RemoveContactById(int id) {

            if (id > 0 && id <= myContacts.Count){
                myContacts.RemoveAt(id - 1);
                return Error.None;
            }
            else {
                return Error.WrongId;
            }
        }

        /// <summary>
        /// Method must return list of string , than saved names and phone numbers seporated 
        /// by char '/'
        /// </summary>
        /// <returns>Error message</returns>
        public List<string> GetListOfContacts() {
            List<string> myContactsList = new List<string>();

            foreach (Person value in myContacts)
            {
                myContactsList.Add(string.Format("{0}/{1}", value.Name, value.PhoneNumber));
            }

            return myContactsList;
        }
        
        ////////////////////       Private         ///////////////////////////

        /// <summary>
        /// Check string with phone number for phone format
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        private bool checkPhoneNumberFormat(string phoneNumber)
        {
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (!char.IsDigit(phoneNumber[i]) || phoneNumber[i] != '-')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Load contact list
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Filename to reading</returns>
        public Error LoadContactList(string filename) {
            if (!File.Exists(filename)) {
                return Error.FileNotFound;
            }

            string[] personParams;

            using (FileStream contactStream = new FileStream(filename, FileMode.Open))
            using (StreamReader reader = new StreamReader(contactStream)) {
                while (!reader.EndOfStream)
                {
                    personParams = reader.ReadLine().Split(' ');
                    myContacts.Add(new Person(personParams[0], personParams[1], personParams[2],
                        personParams[3]));
                }
            }

            return Error.None;
        }

        /// <summary>
        /// Save phoneBook to filename.txt file
        /// </summary>
        /// <param name="filename">Filename or full path</param>
        public void SaveContactList(string filename) {
            
            using(StreamWriter writer = new StreamWriter(filename)){
                for (int i = 0; i < myContacts.Count; i++)
                {
                    writer.WriteLine(string.Format("{0} {1}", myContacts[i].Name,
                        myContacts[i].PhoneNumber));
                }
            }
        }

        /// <summary>
        /// Sorting by surName
        /// </summary>
        /// <param name="surName"></param>
        /// <returns></returns>
        public IEnumerable<Person> SearchBySurname(string surName)
        {
            return myContacts.Where(n => n.SurName == surName);
        }

        /// <summary>
        /// Sorting by forename
        /// </summary>
        /// <param name="foreName"></param>
        /// <returns></returns>
        public IEnumerable<Person> SearchByForeName(string foreName)
        {
            return myContacts.Where(n => n. ForeName== foreName);
        }

        /// <summary>
        /// Sorting by middleName
        /// </summary>
        /// <param name="middleName"></param>
        /// <returns></returns>
        public IEnumerable<Person> SearchByMiddleName(string middleName)
        {
            return myContacts.Where(n => n.MiddleName == middleName);
        }

        /// <summary>
        /// Sorting by phoneNumber
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public IEnumerable<Person> SearchByPhone(string phone)
        {
            return myContacts.Where(n => n.PhoneNumber == phone);
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
            if (type < 1||type >4) { return; }
            else if (type == 1)
            {
                myContacts=myContacts.OrderBy(element => element.SurName).ToList();
            }
            else if (type == 2)
            {
                myContacts = this.myContacts.OrderBy(n => n.ForeName).ToList();
            }
            else if (type == 3)
            {
                myContacts = this.myContacts.OrderBy(n => n.MiddleName).ToList();
            }
            else if (type == 4)
            {
                myContacts = this.myContacts.OrderBy(n => n.PhoneNumber).ToList();
            }
        }
    }
}
