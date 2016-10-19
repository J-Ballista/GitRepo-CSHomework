using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_2_BankApp;

namespace Homework_2_BankApp
{
    enum AccountType { Checking, Savings } //enumeration is a set of named integer constants (storage size == an int)
                                           //Thus by index 0 = Checking & 1 = Savings
                                           //Comparable to doing a #Define in C but more flexible
    class Program
    {
        static void Main(string[] args) //Entry point of the program
        {
            //Default accounts with different account types
            List<Account> acc = new List<Account>(); //List<> collection to store accounts
            acc.Add(new Account("Mr.Monopoly Guy", AccountType.Checking, 8000523));
            acc.Add(new Account("Cecil", AccountType.Savings, 10000));
            acc.Add(new Account("Dylan TackOoOor", AccountType.Checking, 3.50)); //Because Dylan doesn't have savings
            //
            Start: //Label for returning to the main menu
            int line = 1; // starting position of the terminal line
            ConsoleKeyInfo c; // this variable holds Key information
            Console.Clear(); //clearing window
            Console.WriteLine("Select one of the next cases: ('Esc' to exit)");
            Console.WriteLine("Add an account *");
            Console.WriteLine("Delete an account");
            Console.WriteLine("Select user");
            Console.WriteLine("Bank Administration");

            Console.SetCursorPosition(26, 1); //Set cursor position for menu selecting

            //menu loop for 2 selections, 1 on each row
            do
            {
                c = Console.ReadKey(true); //getting key
                switch (c.Key) // identifying pressed key
                {
                    case ConsoleKey.UpArrow: //UpArrow was pressed
                        Console.SetCursorPosition(26, line);
                        Console.Write(" ");                     // Erases existing '*' at console location
                        if (line == 1) line = 4;                // Moves up     to row 1 if on row 2
                        else line--;                            // Moves down   to row 2 if on row 1
                        Console.SetCursorPosition(26, line);
                        Console.Write("*");                     // writes a new '*' at location
                        break;


                    case ConsoleKey.DownArrow://DownArrow was pressed
                        Console.SetCursorPosition(26, line);
                        Console.Write(" ");
                        if (line == 4) line = 1;
                        else line++;
                        Console.SetCursorPosition(26, line);
                        Console.Write("*");
                        break;

                    case ConsoleKey.Escape: Environment.Exit(0); break; //Exit the Program by pressing 'Esc'
                }

            } while (c.Key != ConsoleKey.Enter); //end of main menu loop

            if (line == 1) // Creating new user
            {
                Console.Clear();
                string name = "";
                int type = 0;               // Account types : 1 = 'Checking' & 2 = 'Savings'
                double mon = 0;             // Account money
                Console.WriteLine("Enter registrants name:");
                name = Console.ReadLine();  // Grab user input string name
                Console.WriteLine("Enter account type:(1,2)");
                Console.WriteLine("1.Checking \n2.Savings");
                c = Console.ReadKey(true);
                if ((byte)c.Key < 51 && (byte)c.Key > 48) //49 = code of the "1" button, 50 = "2" //was 52
                {
                    type = ((byte)c.Key) - 49; //checking the input information to place into correct account Type #
                    Console.WriteLine("Enter deposit sum:");
                    try
                    {
                        mon = Double.Parse(Console.ReadLine()); //converting string to double
                    }
                    catch //in the case of incorrect input
                    {
                        mon = 0;
                    }

                    if (mon < 0) mon = 0;

                    acc.Add(new Account(name, (AccountType)type, mon)); //adding new acccount to the collection
                    Console.WriteLine("Successfully done. Press any key...");
                    Console.ReadKey(true);
                    goto Start; // goto to the start label
                }
            }
            else if (line == 2)// Delete previously created users
            {
                do // clean console output users and allow user selection like in main menu
                {
                    Console.Clear(); //Clean window until spotless
                    Console.WriteLine("Choose from {0} users to Delete (ESC - return)", acc.Count);

                    //Cycle through our 'list' and output all users to console
                    for (int i = 0; i < acc.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + acc[i].AcctHolderName);
                    }

                    // Menu navigation again
                    Console.SetCursorPosition(25, 1);
                    Console.Write("*");                 // base point *

                    line = 1;
                    do
                    {
                        c = Console.ReadKey(true);
                        if (acc.Count == 0)
                        {
                            Console.WriteLine("\nThere are currently no users");
                            Console.WriteLine("\nPress any key to go to Main Menu...");
                            Console.ReadLine();
                            goto Start;
                        }
                        switch (c.Key)
                        {
                            case ConsoleKey.UpArrow:
                                Console.SetCursorPosition(25, line);
                                Console.Write(" ");
                                if (line == 1) line = acc.Count;
                                else line--;
                                Console.SetCursorPosition(25, line);
                                Console.Write("*");
                                break;

                            case ConsoleKey.DownArrow:
                                Console.SetCursorPosition(25, line);
                                Console.Write(" ");
                                if (line == acc.Count) line = 1;
                                else line++;
                                Console.SetCursorPosition(25, line);
                                Console.Write("*");
                                break;

                            case ConsoleKey.Escape: goto Start;
                        }
                    } while (c.Key != ConsoleKey.Enter);

                    //Menu end
                    line--; // line = line -1 -> Reset the console line Index back to selection (Enter added 1)
                    Console.Clear();

                    ////////////////////////////////////////////////////////////////////////////////

                    // Deletes user based on line / ID 
                    try
                    {
                        Console.WriteLine("\n{0}'s {1} account has been removed", acc[line].AcctHolderName, acc[line].AcctType);
                        acc.Remove(acc[line]);
                        Console.ReadLine();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        line += 1;
                        Console.Clear(); //Clean window until spotless
                        Console.WriteLine("Choose from {0} users to Delete (ESC - return)", acc.Count);
                        Console.WriteLine("\n{0}'s {1} account has been removed", acc[line].AcctHolderName, acc[line].AcctType);
                        acc.Remove(acc[line]);
                        Console.ReadLine();
                    }
                    

                } while (true); //Press esc to return to the main menu
            }
            else if (line == 3 && acc.Count == 0)
            {
                Console.Beep();
                Console.WriteLine("\n\n\nThere are no users to browse through :( !!! \n\nPress Any Key to Continue...");
                Console.ReadLine();
                goto Start;
            }
            else if (line == 3 && acc.Count > 0)// Selecting created users
            {
                do // another loop for the holding a window
                {
                    Console.Clear(); //Clearing window again
                    Console.WriteLine("Choose from {0} users (ESC - return):", acc.Count); //Printing & Counting accounts

                    // Going through list and printing out each user's Name
                    for (int i = 0; i < acc.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + acc[i].AcctHolderName);
                    }

                    //Menu navigation same as the main menu (repeated)
                    Console.SetCursorPosition(25, 1);
                    Console.Write("*");
                        
                    line = 1;
                    do
                    {
                        c = Console.ReadKey(true);
                        switch (c.Key)
                        {
                            case ConsoleKey.UpArrow:
                                Console.SetCursorPosition(25, line);
                                Console.Write(" ");
                                if (line == 1) line = acc.Count;
                                else line--;
                                Console.SetCursorPosition(25, line);
                                Console.Write("*");
                                break;

                            case ConsoleKey.DownArrow:
                                Console.SetCursorPosition(25, line);
                                Console.Write(" ");
                                if (line == acc.Count) line = 1;
                                else line++;
                                Console.SetCursorPosition(25, line);
                                Console.Write("*");
                                break;

                            case ConsoleKey.Escape: goto Start;
                        }
                    } while (c.Key != ConsoleKey.Enter);

                    //Menu end
                    line--; // line = line -1 -> Reset the console line Index back to selection (Enter added 1)
                    Console.Clear();

                    //calling functions from the UI class
                    UI.Display(acc[line]);
                    UI.Balance(acc[line]);

                    ////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("\nEnter deposit sum:");
                    double mon = 0; //money
                    try //construction for the input validation
                    {
                        mon = Double.Parse(Console.ReadLine());
                    }
                    catch //in the case of wrong input
                    {
                        mon = 0;
                    }
                    if (mon < 0) mon = 0;

                    //calling function from the UI class
                    UI.Deposit(acc[line], mon);

                    Console.WriteLine("\nEnter WithDraw sum:");
                    mon = 0; //money
                    try//construction for the input validation
                    {
                        mon = Double.Parse(Console.ReadLine());
                    }
                    catch//in the case of wrong input
                    {
                        mon = 0;
                    }
                    if (mon < 0) mon = 0;

                    //calling function from the UI class
                    UI.WithDraw(acc[line], mon);
                    Console.WriteLine("\n Press any key...");
                    Console.ReadKey();
                    
                } while (true); //Press esc to return to the main menu //Continues until internal 'if' conditions are met
            }
            else if (line == 4)
            {
                // Grabs the total of all accounts in the bank and displays the total
                Console.WriteLine("\nAccount Summary For Entire Bank\n\n");
                Console.WriteLine("Users :\n");
                // Acquires total investments in bank
                double bankTotal = 0;
                foreach (var user in acc)
                {
                    bankTotal += user.CurBalance();
                    Console.WriteLine("{0,0}|{1,23}",user.AcctNumber,user.AcctHolderName);
                }
                Console.WriteLine("__________________________________________________");
                Console.WriteLine("\nTotal number of clients : {0,23}", acc.Count);
                Console.WriteLine("\nTotal Investment Amount : {0,23:###,###,###.##}$",bankTotal);
                Console.WriteLine("__________________________________________________");
                Console.ReadLine();
                goto Start;
            }
        }
    } 
        


    class Account
    {
        double money = 0; // your deposit money closed from the program(private)
        static int UserNum = 0; // general counter for accounts quantity (private)

        // Fields:
        public int AcctNumber { get; private set; }
        public string AcctHolderName { get; private set; }
        public AccountType AcctType { get; private set; }

        // Constructor without parameters
        public Account() // If no input into account upon creation it will create a Savings Account due to this Constructor
        {
            money = 0;
            AcctNumber = ++UserNum;
            AcctHolderName = "Guest " + AcctNumber;
            AcctType = AccountType.Savings;
        }

        //Constructor with parameteters to set up default values for inputs
        public Account(string name, AccountType accType, double balance)
        {
            AcctHolderName = name;
            if (name.Length == 0) AcctHolderName = "Guest " + AcctNumber; //Guest defaults if no name is input

            AcctType = accType;

            if (balance < 0)
            {
                money = 0;
            }
            else money = balance;

            AcctNumber = ++UserNum;
        }

        //Methods
        public Account(Account acc, double balance = 0) //Creating new account for existing user
        {
            this.AcctHolderName = acc.AcctHolderName;
            this.AcctNumber = ++UserNum;
            this.AcctType = acc.AcctType;
            this.money = balance;
        }

        public double Deposit(double money, out bool pass)  //returns money quantity after transaction
                                                            //flag "pass" shows correct or incorrect result of transaction
        {
            if (money <= 0)
            {
                pass = false;

            }
            else
            {
                this.money += money;
                pass = true;
            }

            return this.money;
        }

        public double WithDraw(double money, out bool pass) //Parameters are used like in Deposit() method
        {
            if (money <= 0 || money > this.money)
            {
                pass = false;
            }
            else
            {
                this.money -= money;
                pass = true;
            }

            return this.money;
        }

        public double CurBalance() //returns current balance 
        {
            return this.money;
        }

        public void Display() //dispays the main information
        {
            Console.WriteLine("Holder name: {0,20}", AcctHolderName);
            Console.WriteLine("Account number: {0,17}", AcctNumber);
            Console.WriteLine("Account type: {0,19}", AcctType.ToString());
        }
    }


    class UI //contains static methods
    {

        public static void Deposit(Account acc, double money)
        {
            bool pass; //flag of the correct transaction
            double N_money = acc.Deposit(money, out pass);
            Console.WriteLine("\n{0}`s Deposit information +({1}):", acc.AcctHolderName, money);

            if (!pass)
            {
                Console.WriteLine("Action revoken. Wrong input information.");
            }
            else
            {
                Console.WriteLine("Deposit successfully renewed. The sum:\t{0:###,###.##}$", N_money);
            }
        }

        public static void WithDraw(Account acc, double money)
        {
            bool pass;//flag of the correct transaction
            money = acc.WithDraw(money, out pass);
            Console.WriteLine("\n{0}`s WithDraw information:", acc.AcctHolderName);
            if (!pass)
            {
                Console.WriteLine("Action revoken. Wrong input information or not enough money.");
            }
            else
            {
                Console.WriteLine("Deposit successfully renewed. The sum:\t{0:###,###.##}$", money);
            }
        }

        public static void Balance(Account acc)
        {
            if (acc.CurBalance() == 0)
            {
                Console.WriteLine("Balance: {0,23}$", 0); return;
            }
            Console.WriteLine("Balance: {0,23:###,###.##}$", acc.CurBalance());
        }

        public static void Display(Account acc)
        {
            acc.Display();
        }

    }
}
