// <copyright file="RandomContactListCreator.cs" company="Some Company">
// Copyright (c) Sprocket Enterprises. All rights reserved.
// </copyright>
// <author>Vitalit Belyakov</author>

namespace PhoneBook
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Create random contact list for phone book
    /// </summary>
    internal class RandomContactListCreator
    {
        // Members

        /// <summary>
        /// Random instance for input
        /// </summary>
        private readonly Random random = new Random();

        /// <summary>
        /// Input list 
        /// </summary>
        private readonly List<string> myNames = new List<string>();

        /// <summary>
        /// Collection of men surnames
        /// </summary>
        private readonly string[] menSurNames = new string[] 
        {
            "Иванов", "Петров", "Бородинов", "Кутузов", "Самойлов", "Албутов", "Викторов",
            "Шаров", "Ушаков",
        };

        /// <summary>
        /// Collection of men forenames
        /// </summary>
        private readonly string[] menForeNames = new string[]
        {
            "Алексей", "Александр", "Тимофей", "Илья", "Владимир", "Виктор", "Михаил", "Петр",
            "Николай"
        };

        /// <summary>
        /// Collection of men middle names
        /// </summary>
        private readonly string[] menMiddleNames = new string[] 
        {
            "Иванович", "Александрович", "Ильич", "Владимирович", "Петрович", "Николаевич",
            "Анатольевич", "Алексеевич", "Константинович"
        };

        /// <summary>
        /// Collection of women surnames
        /// </summary>
        private readonly string[] womenSurNames = new string[]
        {
            "Ушакова", "Иванова", "Смирнова", "Петрова", "Шарова", "Самойлова", "Марженко", "Потапова"
        };

        /// <summary>
        /// Collection of women forenames
        /// </summary>
        private readonly string[] womenForeNames = new string[] 
        {
            "Елена", "Анжелика", "Екатерина", "Мария", "Маргарита", "Юлия", "Анна", "Елизавета"
        };

        /// <summary>
        /// Collection of women middle names
        /// </summary>
        private readonly string[] womenMiddleNames = new string[] 
        {
            "Ивановна", "Александровна", "Владимировна", "Петровна", "Николаевна", "Анатольевна", 
            "Алексеевна", "Константиновна"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomContactListCreator"/> class
        /// </summary>
        public RandomContactListCreator()
        {
        }

        /// <summary>
        /// Create random contact list in second format (Surname_Forename_Middle name_PhoneNumber)
        /// </summary>
        /// <param name="count">Count of the contact</param>
        /// <returns>Count of contact list</returns>
        public List<string> CreateContactList(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var sex = (Sex)this.random.Next(2);

                if (sex == Sex.Female)
                {
                    this.myNames.Add(string.Format(
                        "{0} {1} {2} {3}",
                        this.womenSurNames[this.random.Next(this.womenSurNames.Length)], 
                        this.womenForeNames[this.random.Next(this.womenForeNames.Length)], 
                        this.womenMiddleNames[this.random.Next(this.womenMiddleNames.Length)], 
                        this.GeneratePhoneNumber()));
                }
                else if (sex == Sex.Male)
                {
                    this.myNames.Add(string.Format(
                        "{0} {1} {2} {3}", 
                        this.menSurNames[this.random.Next(this.womenSurNames.Length)], 
                        this.menForeNames[this.random.Next(this.womenForeNames.Length)], 
                        this.menMiddleNames[this.random.Next(this.womenMiddleNames.Length)], 
                        this.GeneratePhoneNumber()));
                }
            }

            return this.myNames;
        }

        /// <summary>
        /// Generate Phone Number
        /// </summary>
        /// <returns>Return new phone number</returns>
        private string GeneratePhoneNumber()
        {   
            string phoneNumber = string.Format(
                "8-{0}-{1}-{2}",
                this.random.Next(910, 980),
                this.random.Next(456, 999),
                this.random.Next(1234, 6789));

            return phoneNumber;
        }
    }
}
