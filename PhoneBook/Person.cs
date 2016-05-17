namespace PhoneBook
{
    class Person
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

        public string SurName {
            get { return this.surName; }
            set { this.surName = value; }
        }
        public string ForeName
        {
            get { return this.foreName; }
            set { this.foreName = value; }
        }
        public string MiddleName{
            get { return this.middleName; }
            set { this.middleName = value; }
        }

        // Properties

        /// <summary>
        /// Return string with surname, forename, middlename seporated by spaces
        /// </summary>
        public string Name => string.Format("{0} {1} {2}", this.surName, this.foreName, this.middleName);

        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string PhoneNumber => this.phoneNumber;
    }
}
