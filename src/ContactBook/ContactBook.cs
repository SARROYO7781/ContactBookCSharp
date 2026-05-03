namespace ContactBook;

using contactsBook;
using static contactsBook.ContactComparer;
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

    private List<Contact> filteredContacts;
    private List<Contact> allContacts;
    private int page;
    private int size;
    private bool isExit;

    public ContactBook(List<Contact>? contacts = null)
    {
        allContacts = (contacts == null) ? new List<Contact>() : contacts;
        filteredContacts = allContacts;
        page = 1;
        size = 10;
        isExit = false;
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
        Console.Clear();
        ShowContacts(filteredContacts, page, size);
    }

    private void ShowContacts(List<Contact> contacts, int page, int size)
    {
        if(contacts.Count <= 0)

        {
            Console.WriteLine("No Contacts Found");
        }
        else
        {
            int indexCol = Math.Max("#".Length, filteredContacts.Count.ToString().Length);
            int fnameCol = Math.Max("First Name".Length, filteredContacts.Max(c => c.GetFName()?.Length ?? 0));
            int lnameCol = Math.Max("Last Name".Length, filteredContacts.Max(c => c.GetLName()?.Length ?? 0));
            int phoneCol = Math.Max("Phone".Length, filteredContacts.Max(c => c.GetPhone()?.Length ?? 0));
            int emailCol = Math.Max("Email".Length, filteredContacts.Max(c => c.GetEmail()?.Length ?? 0));


            Console.WriteLine(""
     + "{0, " + -indexCol + "} "
             + "{1, " + -fnameCol + "} "
             + "{2, " + -lnameCol + "} "
             + "{3, " + -phoneCol + "} "
             + "{4, " + -emailCol + "} ",
             "#", "First Name", "Last Name", "Phone", "Email");
            Console.WriteLine(new string('-', (indexCol + 2 + fnameCol + 2 + lnameCol + 2 + phoneCol + 2 + emailCol)));

            int n = filteredContacts.Count;
            int pageCount = PageCount(contacts, size);
            int s = Math.Clamp((page - 1) * size, 0, n);
            int e = Math.Clamp(s + size, 0, n);

            for (int i = s; i < e; i++)
            {
                Contact c = filteredContacts[i];

                Console.WriteLine(""
                 + "{0, " + indexCol + "} "
                 + "{1, -" + fnameCol + "} "
                 + "{2, -" + lnameCol + "} "
                 + "{3, -" + phoneCol + "} "
                 + "{4, -" + emailCol + "} ",
                (i + 1), c.GetFName(), c.GetLName(), c.GetPhone(), c.GetEmail());
            }

            for(int i = 0; i < size - e + s; i++)
            {
                Console.WriteLine();
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
            case UPDATE_CONTACT:UpdateContact(); break;
            case DELETE_CONTACT:DeleteContact(); break;
            case FIND_CONTACT:FindContact(); break;
            case ORDER_CONTACT:OrderContact(); break;
            case DEDUPLICATE_CONTACT:DeduplicateContact(); break;
            case EXIT:Exit(); break;
            default: break;
        }
    }
    private bool ConfirmExit()
    {
        return (isExit) ? isExit = Confirm("Do you want to exit?", NO): false;
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

    private int GetInt(string prompt, int min, int max)
    {
        string options = $"{min}-{max}";

        Console.Write(prompt + $" [{options}] ");
        string answer = Console.ReadLine()!;
        int value;
        
        while (!int.TryParse(answer, out value) || value < min || value > max)
        {
            Console.WriteLine("ERROR: Invalid Option. Please try again.");
             Console.Write(prompt + $" [{options}] ");
            answer = Console.ReadLine()!.ToUpper();
        }
        return value;
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

    private void NextPage()
    {
        NextPage(filteredContacts, ref page, size);
       
    }
    private void NextPage(List<Contact> contacts, ref int page, int size)
    {
         page = Math.Clamp(page + 1, 1, PageCount(contacts, size));
    }

      private void PrevPage()
    {
        PrevPage(filteredContacts, ref page, size);
       
    }
    private void PrevPage(List<Contact> contacts, ref int page, int size)
    {
         page = Math.Clamp(page - 1, 1, PageCount(contacts, size));
    }

    private void GotoPage()
    {
        GotoPage(filteredContacts, ref page, size);
    }
     private void GotoPage(List<Contact> contacts, ref int page, int size)
    {
        page = GetInt("Enter page", 1, PageCount(contacts, size));
    }

     private void PageSize()
    {
        PageSize(ref page, ref size);
    }

    private void PageSize(ref int page,ref int size)
    {
        int max = Console.WindowHeight - 10;
        size = GetInt("Enter page size", 1, max);
        page = 1;
    }

    private void CreateConatact()
    {
         Console.Clear();
         Console.WriteLine(new string('#', 80));
         Console.WriteLine("Create Contact");
         Console.WriteLine(new string('#', 80));
         Console.WriteLine();

          Console.Write("Enter fist name");
          string fname = Console.ReadLine()!;
           Console.Write("Enter last name");
          string lname = Console.ReadLine()!;
           Console.Write("Enter phone number");
          string phone = Console.ReadLine()!;
           Console.Write("Enter email");
          string email = Console.ReadLine()!;

           Console.WriteLine();

          if(Confirm("Do you wnat to create this contact?", YES))
        {
            Contact c = new Contact(fname, lname, phone, email);
            filteredContacts.Add(c);
            page = PageCount(filteredContacts, size);
            Console.WriteLine("Operation successfull: Contact Created");
        }
        else
        {
            Console.WriteLine("Operation cancelled: Contact not craeted");
        }
        Console.WriteLine();
        PressEnterToContinue();
    }

    private void ReviewContact()
    {
        int index = GetInt("Enter index", 1, filteredContacts.Count) - 1;

        Console.Clear();
         Console.WriteLine(new string('#', 80));
         Console.WriteLine("Review Contact");
         Console.WriteLine(new string('#', 80));
         Console.WriteLine();

        ReviewContact(index);

        Console.WriteLine();
        PressEnterToContinue();
    }

     private void ReviewContact(int index)
    {
        Contact c =filteredContacts[index];

        Console.WriteLine($"First Name: {c.GetFName}");
        Console.WriteLine($"   Last name: {c.GetLName}");
        Console.WriteLine($"       Phone: {c.GetPhone}");
        Console.WriteLine($"       Email: {c.GetEmail}");
        Console.WriteLine();
    }

     private void UpdateContact()
    {
       int index = GetInt("Enter index", 1, filteredContacts.Count) -1;

        Console.Clear();

         Console.WriteLine(new string('#', 80));
         Console.WriteLine("Update Contact");
         Console.WriteLine(new string('#', 80));
         Console.WriteLine();

        UpdateContact(index);
        Console.WriteLine();
        PressEnterToContinue();
    }

     private void UpdateContact(int index)
    {
        Contact c =filteredContacts[index];

        string fname = c.GetFName();
        string lname = c.GetLName();
        string phone = c.GetPhone();
        string email = c.GetEmail();

         ReviewContact(index);
         
         Console.WriteLine();

        if (Confirm("Do you want to edit the first name? ", NO))
        {
            Console.Write("Enter first name: ");
            fname = Console.ReadLine()!;      
        }
        if (Confirm("Do you want to edit the last name? ", NO))
        {
            Console.Write("Enter last name: ");
            lname = Console.ReadLine()!;      
        }
        if (Confirm("Do you want to edit the phone number? ", NO))
        {
            Console.Write("Enter phone number: ");
            phone = Console.ReadLine()!;      
        }
        if (Confirm("Do you want to edit the email? ", NO))
        {
            Console.Write("Enter email: ");
            email = Console.ReadLine()!;     
        }

         Console.WriteLine();

       if(Confirm("Do you wnat to update this contact?", NO))
        {
            c.SetFName(fname);
            c.SetLName(lname);
            c.SetPhone(phone);
            c.SetEmail(email);
            Console.WriteLine("Operation successfull: Contact updated.");
        }
        else
        {
            Console.WriteLine("Operation cancelled: Contact not updated.");
        }
    }

     private void DeleteContact()
    {
       int index = GetInt("Enter index", 1, filteredContacts.Count) -1;

        Console.Clear();

         Console.WriteLine(new string('#', 80));
         Console.WriteLine("Delete Contact");
         Console.WriteLine(new string('#', 80));
         Console.WriteLine();

        DeleteContact(index);

        Console.WriteLine();
        PressEnterToContinue();
    }

     private void DeleteContact(int index)
    {
        Contact c =filteredContacts[index];

         ReviewContact(index);
         
         Console.WriteLine();

       if(Confirm("Do you wnat to delete this contact?", NO))
        {
            filteredContacts.Remove(c);
            Console.WriteLine("Operation successfull: Contact deleted.");
        }
        else
        {
            Console.WriteLine("Operation cancelled: Contact not deleted.");
        }
    }

    private void FindContact()
    {
        Console.Write("Enter search term (Clear): ");
        string searchTerm = Console.ReadLine()!.ToLower();

        if(Confirm("Do you wnat to search contacts?", YES))
        {
            filteredContacts = allContacts.FindAll(c => 
            (c.GetFName()+ c.GetLName()+c.GetPhone()+c.GetEmail()).ToLower().Contains(searchTerm));
        
            page = 1;

            Console.WriteLine("Operation successfull: Contacts search.");
        }
        else
        {
            Console.WriteLine("Operation cancelled: Contacts not searched.");
        }
        PressEnterToContinue();
    }

    private void OrderContact()
    {
        SortType[] sortTypes = new SortType[]
        {
            SortType.FName, SortType.LName, SortType.Phone, SortType.Email
        };

    int index= GetInt("Sort Contacts by [0] First Name [1] Last Name [2] Phone [3] Email", 0, 3);

    ContactComparer ccp= new ContactComparer(sortTypes[index]);
    allContacts.Sort(ccp);
    filteredContacts.Sort(ccp);
    }

      private void DeduplicateContact()
    {
        List<List<Contact>> duplicateGroups = ContactMerger.FindDuplicates(allContacts);
        List<Contact> temp = new List<Contact>();
        foreach(var group in duplicateGroups)
        {
            if(group.Count > 1)
            {
            Console.Clear();
            ShowContacts(group, 1, group.Count);
            int fnameIndex = GetInt("Enter first name index", 0, group.Count - 1);
            int lnameIndex = GetInt("Enter Last name index", 0, group.Count - 1);
            int phoneIndex = GetInt("Enter phone number index", 0, group.Count - 1);
            int emailIndex = GetInt("Enter email index", 0, group.Count - 1);
            

            if(Confirm("Do you want to merge this contacts?", NO))
            {
                Contact merged = new Contact();
                merged.SetFName(group[fnameIndex].GetFName());
                merged.SetLName(group[lnameIndex].GetLName());
                merged.SetPhone(group[phoneIndex].GetPhone());
                merged.SetEmail(group[emailIndex].GetEmail());

                temp.Add(merged);
                Console.WriteLine("Operation succesfull: Contacts merged.");
            }

            else
            {
                temp.AddRange(group);
                Console.WriteLine("Operation cancelled: Contacts not merged.");
            }

                PressEnterToContinue();
            }

            else
            {
                temp.AddRange(group);
            }
        }

        if(Confirm("Do you want to apply all merges to the contacts?", NO))
        {
         allContacts = temp;
         Console.WriteLine("Operation succesfull: Contacts deduplicated.");
        }
        
        else
            {
            Console.WriteLine("Operation cancelled: Contacts not deduplicated.");
            }

            PressEnterToContinue();
    }

        
    private void Exit()
    {
       isExit = true;
    }

}