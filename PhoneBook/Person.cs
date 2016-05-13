using System;
using System.Collections.Generic;

namespace PhoneBook
{
    class Person:IComparable<Person>
    {
        // Constructor
        /// <summary>
        /// Create a person contains SFM format of name and phone number in 
        /// special format x-xxx-xxx-xxxx
        /// </summary>
        /// <param name="surName">surname of person</param>
        /// <param name="foreName">fore name of person</param>
        /// <param name="middleName">middle name of person</param>
        /// <param name="phoneNumber">Must be in second format sample ---> 8-892-283-1243</param>
        public Person(string surName, string foreName, string middleName, string phoneNumber) {
            this.surName = surName;
            this.foreName = foreName;
            this.middleName = middleName;
            this.phoneNumber = phoneNumber;
        }

        // Members
        private string surName;
        private string foreName;
        private string middleName;
        private string phoneNumber;

        // Properties
        /// <summary>
        /// Return string with surname, forename, middlename seporated by spaces
        /// </summary>
        public string Name {
            get {
                return string.Format("{0} {1} {2}", surName, foreName, middleName);
            }
        }

        public string PhoneNumber {
            get {
                return phoneNumber;
            }
        }

        // Methods
        /// <summary>
        /// Change surname
        /// </summary>
        /// <param name="surName">new surName</param>
        public void ChangeSurName(string newSurName) {
            this.surName = newSurName;
        }

        /// <summary>
        /// Change forename
        /// </summary>
        /// <param name="foreName">new foreName</param>
        public void ChangeForeName(string newForeName) {
            this.foreName = newForeName;
        }

        /// <summary>
        /// Change middle name
        /// </summary>
        /// <param name="middleName">new middle name</param>
        public void ChangeMiddleName(string newMiddleName) {
            this.middleName = newMiddleName;
        }

        /// <summary>
        /// Change phone number of format x-xxx-xxx-xxxx
        /// </summary>
        /// <param name="newPhoneNumber">string of phone number in format x-xxx-xxx-xxxx</param>
        public void ChangePhoneNumber(string newPhoneNumber) {
            this.phoneNumber = newPhoneNumber;
        }

        public int CompareTo(Person other)
        {
            return 0;
        }
    }
}
