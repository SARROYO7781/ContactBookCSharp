
using System.Diagnostics;
using System.Drawing;
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
            int indexCol = Math.Max("#".Length, allContacts.Count.ToString().Length);
            int fnameCol = Math.Max("First Name".Length, allContacts.Max(c => c.GetFName()?.Length ??0));
            int lnameCol = Math.Max("Last Name".Length, allContacts.Max(c => c.GetLName()?.Length ??0));
            int phoneCol = Math.Max("Phone".Length, allContacts.Max(c => c.GetPhone()?.Length ??0));
            int emailCol = Math.Max("Email".Length, allContacts.Max(c => c.GetEmail()?.Length ??0));

            Console.WriteLine(""
            + "{0, "+ -indexCol + "} "
            + "{1, "+ -fnameCol + "} "
            + "{2, "+ -lnameCol + "} "
            + "{3, "+ -phoneCol + "} "
            + "{4, "+ -emailCol + "} ",
            "#", "First Name", "Last Name", "Phone", "Email");
            Console.WriteLine(new string('-', (indexCol+2+fnameCol+2+lnameCol+2+phoneCol+2+emailCol)));

            int n = allContacts.Count;
            int page = 1;
            int size = 10;
            int pageCount = (int) Math.Max(1, Math.Ceiling(n/(double)size));
            int s = Math.Clamp((page - 1) * size, 0, n);
            int e = Math.Clamp(s + size, 0, n);

            for(int i = s; i < e; i++)
            {
                Contact.Contact c = allContacts[i];

                Console.WriteLine(""
            + "{0, "+ indexCol + "} "
            + "{1, "+ fnameCol + "} "
            + "{2, "+ lnameCol + "} "
            + "{3, "+ phoneCol + "} "
            + "{4, "+ emailCol + "} ",
            (i +1), c.GetFName(), c.GetFName(), c.GetLName(), c.GetPhone(), c.GetEmail());
            
            }
            Console.WriteLine();
            Console.WriteLine($"Page{page} of {pageCount} ({s + 1}-{e} of {n})");
        }

    }
        


    private void ShowInputOptions()
    {
        string inputOption = ""
        + $"[{NEXT_PAGE}] Next Page | [{CREATE_CONTACT}] Create Contact | [{DELETE_CONTACT}] Delete Contact | [{DEDUPLICATE_CONTACT}] Deduplicate Contacts\n"
        + $"[{PREV_PAGE}] Prev Page | [{REVIEW_CONTACT}] Review Contact | [{FIND_CONTACT  }] Find Contacts  | [{PAGE_SIZE          }] Change page Size\n"
        + $"[{GOTO_PAGE}] Goto Page | [{UPDATE_CONTACT}] Update contact | [{ORDER_CONTACT }] Order Contacts | [{EXIT               }] Exit\n"
        + $"\n> ";

        Console.WriteLine();
        Console.Write(inputOption);
    }    

    private string GetInput()
    {
        return Console.ReadLine()!.ToUpper();
    }

    private bool IsValidInput(string input)
    {
        if (!COMMANDS.Contains(input))
        {
            Console.WriteLine("ERROR: Invalid input. Please try again.");
            return false;
        }
        else
        {
            return true;
        }
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