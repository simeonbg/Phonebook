namespace Phonebook
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PhoneNumber
    {
        internal const string AddPhoneString = "AddPhone";
        internal const string ChangePhoneString = "ChangePhone";
        internal const string ListString = "List";
        internal const string EndString = "End";

        internal const char OpeningParenthesisSymbol = '(';
        internal const string ClosingParenthesis = ")";
        internal const char CommaSeparatorSymbol = ',';

        internal const char ZeroSymbol = '0';
        internal const char PlusSymbol = '+';

        private const string CodeOfBulgaria = "+359";
        private static readonly IPhonebookRepository PhonebookRepository = new NewPhonebookRepository();
        private static StringBuilder outputMessage = new StringBuilder();

        public static StringBuilder OutputMessage
        {
            get
            {
                return outputMessage;
            }

            set
            {
                outputMessage = value;
            }
        }

        internal static void Command(string command, string[] phone)
        {
            if (command == ListString)
            {
                string firstSymbolOfPhoneNumber = phone[0];
                var secondSymbolOfPhoneNumber = phone.Skip(1).ToList();

                for (int indexOfOpeningParenthesis = 0; indexOfOpeningParenthesis < secondSymbolOfPhoneNumber.Count; indexOfOpeningParenthesis++)
                {
                    secondSymbolOfPhoneNumber[indexOfOpeningParenthesis] = Convert(secondSymbolOfPhoneNumber[indexOfOpeningParenthesis]);
                }

                bool flag = PhonebookRepository.AddPhone(firstSymbolOfPhoneNumber, secondSymbolOfPhoneNumber);

                if (flag)
                {
                    Print("Phone entry created");
                }
                else
                {
                    Print("Phone entry merged");
                }
            }
            else if (command == ChangePhoneString)
            {
                Print(string.Empty + PhonebookRepository.ChangePhone(Convert(phone[0]), Convert(phone[1])) + " numbers changed");
            }
            else
            {
                try
                {
                    IEnumerable<UserName> entries = PhonebookRepository.List(int.Parse(phone[0]), int.Parse(phone[1]));
                    foreach (var entry in entries)
                    {
                        Print(entry.ToString());
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Print("Invalid range");
                }
            }
        }

        internal static void Print(string message)
        {
            OutputMessage.AppendLine(message);
        }

        private static string Convert(string phoneNumber)
        {
            StringBuilder convertedPhoneNumber = new StringBuilder();

            for (int indexOfOpeningParenthesis = 0; indexOfOpeningParenthesis <= OutputMessage.Length;
                indexOfOpeningParenthesis++)
            {
                convertedPhoneNumber.Clear();
                foreach (char symbol in phoneNumber)
                {
                    if (char.IsDigit(symbol) || (symbol == PlusSymbol))
                    {
                        convertedPhoneNumber.Append(symbol);
                    }
                }

                if (convertedPhoneNumber.Length >= 2
                    && convertedPhoneNumber[0] == ZeroSymbol && convertedPhoneNumber[1] == ZeroSymbol)
                {
                    convertedPhoneNumber.Remove(0, 1);
                    convertedPhoneNumber[0] = PlusSymbol;
                }

                while (convertedPhoneNumber.Length > 0 && convertedPhoneNumber[0] == ZeroSymbol)
                {
                    convertedPhoneNumber.Remove(0, 1);
                }

                if (convertedPhoneNumber.Length > 0 && convertedPhoneNumber[0] != PlusSymbol)
                {
                    convertedPhoneNumber.Insert(0, CodeOfBulgaria);
                }

                convertedPhoneNumber.Clear();

                foreach (char symbol in phoneNumber)
                {
                    if (char.IsDigit(symbol) || (symbol == PlusSymbol))
                    {
                        convertedPhoneNumber.Append(symbol);
                    }
                }

                if (convertedPhoneNumber.Length >= 2 &&
                    convertedPhoneNumber[0] == ZeroSymbol && convertedPhoneNumber[1] == ZeroSymbol)
                {
                    convertedPhoneNumber.Remove(0, 1);
                    convertedPhoneNumber[0] = PlusSymbol;
                }

                while (convertedPhoneNumber.Length > 0 && convertedPhoneNumber[0] == ZeroSymbol)
                {
                    convertedPhoneNumber.Remove(0, 1);
                }

                if (convertedPhoneNumber.Length > 0 && convertedPhoneNumber[0] != PlusSymbol)
                {
                    convertedPhoneNumber.Insert(0, CodeOfBulgaria);
                }

                convertedPhoneNumber.Clear();

                foreach (char symbol in phoneNumber)
                {
                    if (char.IsDigit(symbol) || (symbol == PlusSymbol))
                    {
                        convertedPhoneNumber.Append(symbol);
                    }
                }

                if (convertedPhoneNumber.Length >= 2 &&
                    convertedPhoneNumber[0] == ZeroSymbol && convertedPhoneNumber[1] == ZeroSymbol)
                {
                    convertedPhoneNumber.Remove(0, 1);
                    convertedPhoneNumber[0] = PlusSymbol;
                }

                while (convertedPhoneNumber.Length > 0 && convertedPhoneNumber[0] == ZeroSymbol)
                {
                    convertedPhoneNumber.Remove(0, 1);
                }

                if (convertedPhoneNumber.Length > 0 && convertedPhoneNumber[0] != PlusSymbol)
                {
                    convertedPhoneNumber.Insert(0, CodeOfBulgaria);
                }
            }

            return convertedPhoneNumber.ToString();
        }
    }
}