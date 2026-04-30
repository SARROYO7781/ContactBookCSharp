
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

    public readonly string[] COMMANDS = new string[]
    {
    NEXT_PAGE, PREV_PAGE, GOTO_PAGE, PAGE_SIZE, CREATE_CONTACT,
    REVIEW_CONTACT, UPDATE_CONTACT, DELETE_CONTACT, FIND_CONTACT,
    ORDER_CONTACT, DEDUPLICATE_CONTACT,EXIT
    };

    public ContactBook()
    {
        
    }

    public void Start()
    {
        ShowWelcomScreen();

        string input;
        do
        {
            ShowContacts();

            do
            {
                ShowInputOptions();
                input = GetInput();
            }
            while(!IsValidInput(input));

            ProcessInput(input);

            do
            {
                ShowInputOptions();
            }
            while(!ConfirmExit());

            ShowExitScreen();
        }
        while(false);
    }

    private void ShowWelcomScreen()
    {
        Console.WriteLine("Welcome to Sebastian's Contact Book!");
        PressEnterToContinue();
    }

    private void ShowContacts()
    {
        
    }

    private void ShowInputOptions()
    {
        
    }

    private string GetInput()
    {
        return "";
    }

    private bool IsValidInput(string input)
    {
        return true;
    }

    private void ProcessInput(string input)
    {
      
    }

    private bool ConfirmExit()
    {
       return true;
    }

    private void ShowExitScreen()
    {
        
    }
    private void PressEnterToContinue()
    {
        Console.Write("Press ENTER to continue");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
            ;
        }
    }
}