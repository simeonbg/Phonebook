namespace PhonebookCommon
{
    using System;
    using System.Linq;
    using Phonebook;

    public class PhonebookMain : PhoneNumber
    {
        public static void Main()
        {
            while (true)
            {
                string readCommand = Console.ReadLine();

                if (readCommand == PhoneNumber.EndString || readCommand == null)
                {
                    break;
                }

                int indexOfOpeningParenthesis = readCommand.IndexOf(OpeningParenthesisSymbol);

                if (indexOfOpeningParenthesis == -1)
                {
                    throw new IndexOutOfRangeException("Fatal error in the program!!! Index of opening parenthesisis is equal to -1.");
                }

                string command = readCommand.Substring(0, indexOfOpeningParenthesis);

                if (!readCommand.EndsWith(PhoneNumber.ClosingParenthesis))
                {
                    Main();
                }

                string stringOfCommandParameteres = 
                    readCommand.Substring(indexOfOpeningParenthesis + 1, readCommand.Length - indexOfOpeningParenthesis - 2);

                string[] commandParameters = stringOfCommandParameteres.Split(CommaSeparatorSymbol);

                for (int currentIndex = 0; currentIndex < commandParameters.Length; currentIndex++)
                {
                    commandParameters[currentIndex] = commandParameters[currentIndex].Trim();
                }

                if (command.StartsWith(PhoneNumber.AddPhoneString) && commandParameters.Length >= 2)
                {
                    PhoneNumber.Command(PhoneNumber.AddPhoneString, commandParameters);
                }
                else if ((command == PhoneNumber.ChangePhoneString) && (commandParameters.Length == 2))
                {
                    PhoneNumber.Command(PhoneNumber.ChangePhoneString, commandParameters);
                }
                else if ((command == PhoneNumber.ListString) && (commandParameters.Length == 2))
                {
                    PhoneNumber.Command(PhoneNumber.ListString, commandParameters);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Count of parameters of the command is not correct.");
                }

                PhoneNumber.Print(OutputMessage.ToString());
            }
        }
    }
}
