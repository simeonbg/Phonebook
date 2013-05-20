namespace Phonebook
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class PhonebookRepository : IPhonebookRepository
    {
        private readonly OrderedSet<UserName> sortedUsers = new OrderedSet<UserName>();
        private readonly Dictionary<string, UserName> users = new Dictionary<string, UserName>();
        private readonly MultiDictionary<string, UserName> phonebook = new MultiDictionary<string, UserName>(false);

        public bool AddPhone(string name, IEnumerable<string> phones)
        {
            string nameToLowerInvariant = name.ToLowerInvariant();
            UserName user;
            bool isEmpty = !this.users.TryGetValue(nameToLowerInvariant, out user);

            if (isEmpty)
            {
                user = new UserName();
                user.Name = name;
                user.Strings = new SortedSet<string>();
                this.users.Add(nameToLowerInvariant, user);
                this.sortedUsers.Add(user);
            }

            foreach (var phone in phones)
            {
                this.phonebook.Add(phone, user);
            }

            user.Strings.UnionWith(phones);

            return isEmpty;
        }

        public int ChangePhone(string oldNumber, string newNumber)
        {
            var foundPhonebookList = this.phonebook[oldNumber].ToList();

            foreach (var user in foundPhonebookList)
            {
                user.Strings.Remove(oldNumber);
                this.phonebook.Remove(oldNumber, user);
                user.Strings.Add(newNumber);
                this.phonebook.Add(newNumber, user);
            }

            return foundPhonebookList.Count;
        }

        public UserName[] List(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex + count > this.users.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid start index or count.");
            }

            UserName[] listOfSelectedUsers = new UserName[count];

            for (int currentIndex = startIndex; currentIndex <= startIndex + count - 1; currentIndex++)
            {
                UserName currentUser = this.sortedUsers[currentIndex];
                listOfSelectedUsers[currentIndex - startIndex] = currentUser;
            }

            return listOfSelectedUsers;
        }
    }
}