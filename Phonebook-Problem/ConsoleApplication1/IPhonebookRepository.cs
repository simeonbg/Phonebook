namespace Phonebook
{
    using System.Collections.Generic;

    public interface IPhonebookRepository
    {
        /// <summary>
        /// Add the new user and phone number or numbers.
        /// </summary>
        /// <returns>Is add new user and phone number.</returns>
        bool AddPhone(string name, IEnumerable<string> phoneNumbers);

        /// <summary>
        /// Update phone number of existed user.
        /// </summary>
        /// <returns>How phone numbers was changed.</returns>
        int ChangePhone(string oldPhoneNumber, string newPhoneNumber);

        /// <summary>
        /// Create sorted list of users.
        /// </summary>
        /// <param name="startIndex">Strted index of sorted phonebook.</param>
        /// <param name="count">Count of user, who add in the list.</param>
        /// <returns>Array of users.</returns>
        UserName[] List(int startIndex, int count);
    }
}