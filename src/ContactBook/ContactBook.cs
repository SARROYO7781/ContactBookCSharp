
using System.Diagnostics;
using Contact;

namespace ContactBook;

public class ContactBook
{
    public const string NEXT_PAGE = "+";
    public const string PREV_PAGE = "-";
    public const string GOTO_PAGE = "G";
    public const string PAGE_SIZE = "S";
    public const string CREATE_CONTACT = "C";
    public const string REVIEW_CONTACT = "R";
    public const string UPDATE_CONTACT = "U";
    public const string DELETE_CONTACT = "D";
    public const string FIND_CONTACT = "F";
    public const string ORDER_CONTACT = "O";
    public const string DEDUPLICATE_CONTACT = "M";
    public const string EXIT = "X";

    public readonly string[] COMMANDS =
    {
        NEXT_PAGE, PREV_PAGE, GOTO_PAGE, PAGE_SIZE, CREATE_CONTACT,
        REVIEW_CONTACT, UPDATE_CONTACT, DELETE_CONTACT, FIND_CONTACT,
        ORDER_CONTACT, DEDUPLICATE_CONTACT, EXIT
    };

    private List<Contact.Contact> allContacts;

    public ContactBook(List<Contact.Contact>? contacts = null)
    {
        allContacts = contacts ?? new List<Contact.Contact>();
    }

    public void Start()
    {
        ShowWelcomeScreen();

        string input;

        do
        {
            ShowContacts();

            do
            {
                ShowInputOptions();
                input = GetInput();
            }
            while (!IsValidInput(input));

            ProcessInput(input);

            if (input == EXIT)
            {
                if (ConfirmExit())
                {
                    ShowExitScreen();
                    break;
                }
            }
        }
        while (true);
    }

    private void ShowWelcomeScreen()
    {
        Console.WriteLine("Welcome to Sebastian's Contact Book!");
        PressEnterToContinue();
    }

    private void ShowContacts()
    {
         Console.Clear();
        if(allContacts.Count <= 0)
        {
            Console.WriteLine("No Contacts Found");
        }
        else
        {
            int indexCol = allContacts.Count.ToString().Length;
            int fnameCol = allContacts.Max(c => c.GetFName()?.Length ??0);
            int lnameCol = allContacts.Max(c => c.GetLName()?.Length ??0);
            int phoneCol = allContacts.Max(c => c.GetPhone()?.Length ??0);
            int emailCol = allContacts.Max(c => c.GetEmail()?.Length ??0);

            for(int i = 0; i < allContacts.Count; i++)
            {
                Contact.Contact c = allContacts[i];
                Console.WriteLine(""
            + "{0, "+ indexCol + "}"
            + "{1, "+ fnameCol + "}"
            + "{2, "+ lnameCol + "}"
            + "{3, "+ phoneCol + "}"
            + "{4, "+ emailCol + "}",
            i, c.GetFName(), c.GetFName(), c.GetLName(), c.GetPhone(), c.GetEmail());
            
            }
        }

    }
        


    private void ShowInputOptions()
    {
        
    }

    private string GetInput()
    {
        return Console.ReadLine()?.Trim().ToUpper() ?? "";
    }

    private bool IsValidInput(string input)
    {
        return COMMANDS.Contains(input);
    }

    private void ProcessInput(string input)
    {
        Console.WriteLine($"You selected: {input}");
    }

    private bool ConfirmExit()
    {
        Console.Write("Are you sure you want to exit? (Y/N): ");
        string answer = Console.ReadLine()?.Trim().ToUpper() ?? "";

        return answer == "Y";
    }

    private void ShowExitScreen()
    {
        Console.WriteLine("Thank you for using Sebastian's Contact Book!");
    }

    private void PressEnterToContinue()
    {
        Console.Write("Press ENTER to continue...");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        
    }
}