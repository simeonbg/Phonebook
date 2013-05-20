namespace Phonebook
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NewPhonebookRepository : IPhonebookRepository
    {
        private readonly List<UserName> users = new List<UserName>();
        private int changedPhones = 0;

        public bool AddPhone(string name, IEnumerable<string> phones)
        {
            var usersWithSameName = from user in this.users
                                    where user.Name.ToLowerInvariant() == name.ToLowerInvariant()
                                    select user;

            bool isAddNewUser;

            if (usersWithSameName.Count() == 0)
            {
                UserName user = new UserName();
                user.Name = name;
                user.Strings = new SortedSet<string>();

                foreach (var phone in phones)
                {
                    user.Strings.Add(phone);
                }

                this.users.Add(user);

                isAddNewUser = true;
            }
            else if (usersWithSameName.Count() == 1)
            {
                UserName user = usersWithSameName.First();

                foreach (var phone in phones)
                {
                    user.Strings.Add(phone);
                }

                isAddNewUser = false;
            }
            else
            {
                Console.WriteLine("Duplicated name in the phonebook found: " + name);
                return false;
            }

            return isAddNewUser;
        }

        public int ChangePhone(string oldNumber, string newNumber)
        {
            var usersWithOldNumber = from user in this.users
                                     where user.Strings.Contains(oldNumber)
                                     select user;

            foreach (var user in usersWithOldNumber)
            {
                user.Strings.Remove(oldNumber);
                user.Strings.Add(newNumber);
                this.changedPhones++;
            }

            return this.changedPhones;
        }

        public UserName[] List(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex + count > this.users.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid start index or count.");
            }

            this.users.Sort();

            UserName[] listOfSelectedUsers = new UserName[count];
            for (int currentIndex = startIndex; currentIndex <= startIndex + count - 1; currentIndex++)
            {
                UserName currentUser = this.users[currentIndex];
                listOfSelectedUsers[currentIndex - startIndex] = currentUser;
            }

            return listOfSelectedUsers;
        }
    }
}