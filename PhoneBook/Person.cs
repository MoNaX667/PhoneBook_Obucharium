// <copyright file="Person.cs" company="Some Company">
// Copyright (c) Sprocket Enterprises. All rights reserved.
// </copyright>
// <author>Vitalit Belyakov</author>

namespace PhoneBook
{
    /// <summary>
    /// Person class
    /// </summary>
    internal class Person
    {
        // Members

        /// <summary>
        /// Surname field
        /// </summary>
        private string surName;

        /// <summary>
        /// Forename field
        /// </summary>
        private string foreName;

        /// <summary>
        /// Middle name field
        /// </summary>
        private string middleName;

        /// <summary>
        /// Phone number field
        /// </summary>
        private string phoneNumber;

        // Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class
        /// </summary>
        /// <param name="surName">surname of person</param>
        /// <param name="foreName">fore name of person</param>
        /// <param name="middleName">middle name of person</param>
        /// <param name="phoneNumber">Must be in second format sample ---> 8-892-283-1243</param>
        public Person(string surName, string foreName, string middleName, string phoneNumber)
        {
            this.surName = surName;
            this.foreName = foreName;
            this.middleName = middleName;
            this.phoneNumber = phoneNumber;
        }

        // Properties

        /// <summary>
        /// Gets or sets surname
        /// </summary>
        public string SurName
        {
            get { return this.surName; }
            set { this.surName = value; }
        }

        /// <summary>
        /// Gets or sets forename
        /// </summary>
        public string ForeName
        {
            get { return this.foreName; }
            set { this.foreName = value; }
        }

        /// <summary>
        /// Gets or sets middle name
        /// </summary>
        public string MiddleName
        {
            get { return this.middleName; }
            set { this.middleName = value; }
        }

        /// <summary>
        /// Return string with surname, forename, middle name by spaces
        /// </summary>
        public string Name => string.Format(
            "{0} {1} {2}", 
            this.surName,
            this.foreName,
            this.middleName);

        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber => this.phoneNumber;
    }
}
