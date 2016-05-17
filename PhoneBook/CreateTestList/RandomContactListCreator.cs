namespace PhoneBook
{
    using System;
    using System.Collections.Generic;

    internal class RandomContactListCreator
    {
        // Members
        private readonly Random random = new Random();
        private readonly List<string> myNames = new List<string>();

        // Men random parametrs
        private readonly string[] menSurNames = new string[] 
        {
            "Иванов", "Петров", "Бородинов", "Кутузов", "Самойлов", "Албутов", "Викторов",
            "Шаров", "Ушаков",
        };

        private readonly string[] menForeNames = new string[]
        {
            "Алексей", "Александр", "Тимофей", "Илья", "Владимир","Виктор", "Михаил", "Петр",
            "Николай"
        };

        private readonly string[] menMiddleNames = new string[] 
        {
            "Иванович", "Александрович", "Ильич", "Владимирович", "Петрович", "Николаевич",
            "Анатольевич", "Алексеевич", "Константинович"
        };

        // Women random parametr
        private readonly string[] womenSurNames = new string[]
        {
            "Ушакова", "Иванова", "Смирнова", "Петрова", "Шарова", "Самойлова", "Марженко", "Потапова"
        };

        private readonly string[] womenForeNames = new string[] 
        {
            "Елена", "Анжелика", "Екатерина", "Мария", "Маргарита", "Юлия", "Анна", "Елизавета"
        };

        private readonly string[] womenMiddleNames = new string[] 
        {
            "Ивановна", "Александровна", "Владимировна", "Петровна", "Николаевна", "Анатольевна", 
            "Алексеевна", "Константиновна"
        };

        // Contructor
        public RandomContactListCreator()
        {
        }

        /// <summary>
        /// Create random contact list in second fotmat(Surname_Forename_Middlename_PhoneNumber)
        /// </summary>
        /// <param name="count"></param>
        /// <returns>COunt of contact list</returns>
        public List<string> CreateContactList(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var sex = (Sex)this.random.Next(2);

                if (sex == Sex.Female)
                {
                    myNames.Add(
                        string.Format("{0} {1} {2} {3}",
                        this.womenSurNames[this.random.Next(this.womenSurNames.Length)], 
                        this.womenForeNames[this.random.Next(this.womenForeNames.Length)], 
                        this.womenMiddleNames[this.random.Next(this.womenMiddleNames.Length)], 
                        this.GeneratePhoneNumber()));
                }
                else if (sex == Sex.Male) {
                    myNames.Add(
                        string.Format("{0} {1} {2} {3}", 
                        this.menSurNames[this.random.Next(this.womenSurNames.Length)], 
                        this.menForeNames[this.random.Next(this.womenForeNames.Length)], 
                        this.menMiddleNames[this.random.Next(this.womenMiddleNames.Length)], 
                        this.GeneratePhoneNumber()));
                }
            }

            return this.myNames;
        }

        /// <summary>
        /// GeneratePhoneNumber
        /// </summary>
        /// <returns></returns>
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
