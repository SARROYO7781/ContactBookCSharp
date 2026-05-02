namespace ContactBook;

public class ContactBook
{
    public const string YES = "Y";
    public const string NO = "N";

    public readonly string[] YES_NO = new string[] {YES, NO};

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

    private List<Contact> allContacts;
    private int page;
    private int size;

    public ContactBook(List<Contact>? contacts = null)
    {
        allContacts = contacts ?? new List<Contact>();
        page = 1;
        size = 10;
    }

    public void Start()
    {
        ShowWelcomeScreen();

        string input;
        do
        {
            do
            {   
                ShowContacts();
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
        ShowContacts(allContacts, page, size);
    }

    private void ShowContacts(List<Contact> contacts, int page, int size)
    {
         Console.Clear();
        if(contacts.Count <= 0)

        {
            Console.WriteLine("No Contacts Found");
        }
        else
        {
            int indexCol = Math.Max("#".Length, allContacts.Count.ToString().Length);
            int fnameCol = Math.Max("First Name".Length, allContacts.Max(c => c.GetFName()?.Length ?? 0));
            int lnameCol = Math.Max("Last Name".Length, allContacts.Max(c => c.GetLName()?.Length ?? 0));
            int phoneCol = Math.Max("Phone".Length, allContacts.Max(c => c.GetPhone()?.Length ?? 0));
            int emailCol = Math.Max("Email".Length, allContacts.Max(c => c.GetEmail()?.Length ?? 0));


            Console.WriteLine(""
     + "{0, " + -indexCol + "} "
             + "{1, " + -fnameCol + "} "
             + "{2, " + -lnameCol + "} "
             + "{3, " + -phoneCol + "} "
             + "{4, " + -emailCol + "} ",
             "#", "First Name", "Last Name", "Phone", "Email");
            Console.WriteLine(new string('-', (indexCol + 2 + fnameCol + 2 + lnameCol + 2 + phoneCol + 2 + emailCol)));

            int n = allContacts.Count;
            int pageCount = PageCount(contacts, size);
            int s = Math.Clamp((page - 1) * size, 0, n);
            int e = Math.Clamp(s + size, 0, n);

            for (int i = s; i < e; i++)
            {
                Contact c = allContacts[i];

                Console.WriteLine(""
                 + "{0, " + indexCol + "} "
                 + "{1, -" + fnameCol + "} "
                 + "{2, -" + lnameCol + "} "
                 + "{3, -" + phoneCol + "} "
                 + "{4, -" + emailCol + "} ",
                (i + 1), c.GetFName(), c.GetLName(), c.GetPhone(), c.GetEmail());

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
            PressEnterToContinue();
            return false;

        }
        else
        {
            return true;
        }
    }

    private void ProcessInput(string input)
    {
        switch (input)
        {
            case NEXT_PAGE: NextPage(); break;
            case PREV_PAGE:PrevPage(); break;
            case GOTO_PAGE:GotoPage(); break;
            case PAGE_SIZE:PageSize(); break;
            case CREATE_CONTACT:CreateConatact(); break;
            case REVIEW_CONTACT:ReviewContact(); break;
            case UPDATE_CONTACT:UpadateContact(); break;
            case DELETE_CONTACT:Deletecontact(); break;
            case FIND_CONTACT:FindContact(); break;
            case ORDER_CONTACT:OrderContact(); break;
            case DEDUPLICATE_CONTACT:DeduplicateContact(); break;
            case EXIT:Exit(); break;
            default: break;
        }
    }
    private bool ConfirmExit()
    {
        return Confirm("Do you want to exit?", NO);
    }

    private void ShowExitScreen()
    {
        Console.Clear();
        Console.WriteLine("Thank you for using Sebastian's Contact Book!");
    }

    private void PressEnterToContinue()
    {
        Console.Write("Press ENTER to continue...");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        
    }

    private void Exit()
    {
       Console.WriteLine("Exit");
    }
    private string GetOption(string prompt, string[] validOptions, string defaultOption)
    {
        string options = string.Join('/',validOptions);
        Console.Write(prompt + $" [{options}] ({default}) ");
        string option = Console.ReadLine()!.ToUpper();

        if (string.IsNullOrWhiteSpace(option))  { option = defaultOption;}
        
        while (validOptions.Contains(option))
        {
            Console.WriteLine("ERROR: Invalid Option. Please try again.");
             Console.Write(prompt + $" [{options}] ({default}) ");
            option = Console.ReadLine()!.ToUpper();

            if(string.IsNullOrWhiteSpace(option)) { option = defaultOption; }
        }
        return option;
        }
        
        private bool Confirm(string prompt, string defaultOption)
    {
        return GetOption(prompt, YES_NO, defaultOption) == YES;
    }

        private static int PageCount(List<Contact> contacts, int size)
    {
        return (int) Math.Max(1, Math.Ceiling(contacts.Count/ (double) size));
    }

    private void DeduplicateContact()
    {
        Console.WriteLine("Deduplicate Contact");
    }

    private void OrderContact()
    {
        Console.WriteLine("Order Contact");
    }

    private void FindContact()
    {
        Console.WriteLine("Find Contact");
    }

    private void Deletecontact()
    {
         Console.WriteLine("Delete Contact");
    }

    private void UpadateContact()
    {
       Console.WriteLine("Update Contacts");
    }

    private void ReviewContact()
    {
         Console.WriteLine("Review Contact");
    }

    private void CreateConatact()
    {
         Console.WriteLine("Create Contact");
    }

    private void PageSize()
    {
         Console.WriteLine("Page Size");
    }

    private void GotoPage()
    {
         Console.WriteLine("Goto Page");
    }

    private void PrevPage()
    {
        PrevPage(allContacts, ref page, size);
       
    }
    private void PrevPage(List<Contact> contacts, ref int page, int size)
    {
         page = Math.Clamp(page - 1, 1, PageCount(contacts, size));
    }

    private void NextPage()
    {
        NextPage(allContacts, ref page, size);
       
    }
    private void NextPage(List<Contact> contacts, ref int page, int size)
    {
         page = Math.Clamp(page + 1, 1, PageCount(contacts, size));
    }

}