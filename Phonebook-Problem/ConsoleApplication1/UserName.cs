namespace Phonebook
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserName : IComparable<UserName>
    {
        private const char OpeningSquareBracketSymbol = '[';
        private const char ClosingSquareBracketSymbol = ']';
        private const string ColonWithSpace = ": ";
        private const string CommaWithSpace = ", ";

        private string inputName;
        private string nameToLowerInvariant;

        public string Name
        {
            get
            {
                return this.inputName;
            }

            set
            {
                this.inputName = value;
                this.nameToLowerInvariant = value.ToLowerInvariant();
            }
        }

        public SortedSet<string> Strings
        {
            get
            {
                return (SortedSet<string>)this.ToString().Clone();
            }

            set
            {
                this.Strings = value;
            }
        }

        public override string ToString()
        {
            StringBuilder inputName = new StringBuilder();
            inputName.Clear();
            inputName.Append(OpeningSquareBracketSymbol);
            inputName.Append(this.Name);

            bool isOnlyName = true;

            foreach (var phone in this.Strings)
            {
                if (isOnlyName)
                {
                    inputName.Append(ColonWithSpace);
                    isOnlyName = false;
                }
                else
                {
                    inputName.Append(CommaWithSpace);
                }

                inputName.Append(phone);
            }

            inputName.Append(ClosingSquareBracketSymbol);

            return inputName.ToString();
        }

        public int CompareTo(UserName otherUser)
        {
            return this.nameToLowerInvariant.CompareTo(otherUser.nameToLowerInvariant);
        }
    }
}