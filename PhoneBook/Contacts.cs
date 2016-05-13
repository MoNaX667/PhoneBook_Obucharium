using System;
using System.Collections.Generic;
using System.IO;

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
        /// Change contact by id
        /// </summary>
        /// <param name="idOfContact">Id of target element</param>
        /// <param name="secondName"></param>
        /// <param name="foreName"></param>
        /// <param name="middleName"></param>
        /// <param name="phoneNumber"></param>
        public Error ChangeContactById(int idOfContact, string surName, string foreName,
            string middleName, string phoneNumber) {

            if (!(idOfContact > 0) && !(idOfContact <= myContacts.Count)){
                return Error.WrongId;
            }

            if (!checkPhoneNumberFormat(phoneNumber)) {
                return Error.WrongPhoneNumberFormat;
            }

            if (!string.IsNullOrEmpty(surName)) {
                myContacts[idOfContact].ChangeSurName(surName);
            }

            if (!string.IsNullOrEmpty(foreName)){
                myContacts[idOfContact].ChangeForeName(foreName);
            }

            if (!string.IsNullOrEmpty(foreName)){
                myContacts[idOfContact].ChangeMiddleName(middleName);
            }

            if (!string.IsNullOrEmpty(foreName))
            {
                myContacts[idOfContact].ChangePhoneNumber(phoneNumber);
            }

            return Error.None;
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

            FileStream contactStream = new FileStream(filename, FileMode.Open);
            StreamReader reader = new StreamReader(contactStream);

            string[] personParams;

            while (!reader.EndOfStream) {
                personParams = reader.ReadLine().Split(' ');
                myContacts.Add(new Person(personParams[0], personParams[1], personParams[2],
                    personParams[3]));
            }

            return Error.None;
        }

    }
}
