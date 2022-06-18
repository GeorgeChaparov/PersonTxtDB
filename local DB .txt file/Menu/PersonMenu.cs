using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using local_DB.txt_file.Menu;
using local_DB.txt_file;

namespace local_DB.txt_file.Menu
{
    public static class PersonMenu
    {

        public static void ListPersonMenu(string peoplesPath, string jobsPath)
        {
            ListCommands:
            Console.Clear();
            Console.WriteLine("         Person Menu");
            Console.WriteLine();

            Console.WriteLine("1 - Create person");
            Console.WriteLine("2 - Read person");
            Console.WriteLine("3 - Update person");
            Console.WriteLine("4 - Delete person");
            Console.WriteLine("0 - Exit");

            Console.WriteLine();
            int TimeEnterPressed = 0;

            EnterNumberOfCommand:
            Console.Write("Please enter the number of the command that you want to execute: ");
            string input = Console.ReadLine();

            int selection = -1;

            if (int.TryParse(input, out selection))
            {
                if (selection == 0)
                {
                    Environment.Exit(0);
                }
                else if (selection == 1)
                {
                    CreatePerson(peoplesPath, jobsPath);
                }
                else if (selection == 2)
                {
                    ReadPerson(peoplesPath,  jobsPath);
                }
                else if (selection == 3)
                {
                    UpdatePerson(peoplesPath, jobsPath);
                }
                else if (selection == 4)
                {
                    DeletePerson(peoplesPath, jobsPath);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Select a correct number!");
                    Console.ReadLine();
                    goto ListCommands;
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Please select a digit (Press enter on more time to go back)");
                Console.WriteLine();
                Console.WriteLine();
                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    Console.Clear();
                    Program.MainMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto EnterNumberOfCommand;
                }
            }
        }

        static void CreatePerson(string peoplesPath, string jobsPath)
        {
            int TimeEnterPressed = 0;

            string Peoples = File.ReadAllText(peoplesPath);

            firstName:
            Console.WriteLine();
            Console.Write("Enter the first name of the person: ");
            string firstName = Console.ReadLine();

            if (firstName.Length == 0)
            {
                Console.WriteLine();
                Console.WriteLine("The first name can't be \"nothing\"! (Press enter on more time to go back)");
                Console.WriteLine();
                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    ListPersonMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto firstName;
                }
            }

            TimeEnterPressed = 0;
            lastName:
            Console.WriteLine();
            Console.Write("Enter the last name of the person: ");
            string lastName = Console.ReadLine();

            if (lastName.Length == 0)
            {
                Console.WriteLine();
                Console.WriteLine("The last name can't be \"nothing\"! (Press enter on more time to go back)");
                Console.WriteLine();
                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    ListPersonMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto lastName;
                }

            }     

            TimeEnterPressed = 0;
            age:
            Console.WriteLine();
            Console.Write("Enter the age of the person: ");
            int age = 0;
            try
            {
                age = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter only digits! (Press enter on more time to go back)");
                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    ListPersonMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto age;
                }
            }
            
            TimeEnterPressed = 0;
            if (Peoples.Contains($"FirstName: {firstName}; LastName: {lastName}; Age: {age}"))
            {
                Console.WriteLine();
                Console.WriteLine("Person with this name and age already exist!");
                return;
            }

            JobMenu.ReadJob(peoplesPath, jobsPath);

            Console.WriteLine();
            Console.Write("Select the name of the profession of the person: ");

            Job:

            string jobName = Console.ReadLine();

            string Jobs = File.ReadAllText(jobsPath);

            if (Jobs.Contains($"Name: {jobName}"))
            {
                int startOfJobPosition = Jobs.IndexOf($"Name: {jobName}");
                int endOfJobPosition = Jobs.IndexOf("<^>", startOfJobPosition);
                int length = endOfJobPosition - startOfJobPosition;
                string Job = Jobs.Substring(startOfJobPosition, length);
                StreamWriter streamWriter = new StreamWriter(peoplesPath, true);

                streamWriter.WriteLine($"FirstName: {firstName}; LastName: {lastName}; Age: {age}; Profession - ({Job})<^>");
                streamWriter.Close();

                Console.WriteLine();
                Console.WriteLine("The person has been added!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("This job does not exist! (Press enter on more time to go back)");
                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    ListPersonMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto Job;
                }
            }
        }

        static void ReadPerson(string peoplesPath, string jobsPath)
        {
            Console.WriteLine();
            Console.WriteLine("Peoples:");

            string Peoples = File.ReadAllText(peoplesPath);

            string[] Person = Peoples.Split( new string[] {Environment.NewLine}, StringSplitOptions.None);

            foreach (string OnePerson in Person)
            {
                if (OnePerson == "")
                {
                    continue;
                }
                int startOfFirstNamePosition = OnePerson.IndexOf($"FirstName:");
                int endOfFirstNamePosition = OnePerson.IndexOf(';', startOfFirstNamePosition);
                int startOfActualFirstNamePosition = OnePerson.IndexOf(' ', startOfFirstNamePosition);
                int firstNameLength = endOfFirstNamePosition + 1 - startOfActualFirstNamePosition;
                string firstName = OnePerson.Substring(startOfActualFirstNamePosition + 1, firstNameLength - 2);

                int startOfLastNamePosition = OnePerson.IndexOf($"LastName:");
                int endOfLastNamePosition = OnePerson.IndexOf(';', startOfLastNamePosition);
                int startOfActualLastNamePosition = OnePerson.IndexOf(' ', startOfLastNamePosition);
                int lastNameLength = endOfLastNamePosition + 1 - startOfActualLastNamePosition;
                string lastName = OnePerson.Substring(startOfActualLastNamePosition + 1, lastNameLength - 2);

                int startOfAgePosition = OnePerson.IndexOf($"Age:");
                int endOfAgePosition = OnePerson.IndexOf(';', startOfAgePosition);
                int startOfActualAgePosition = OnePerson.IndexOf(' ', startOfAgePosition);
                int ageLength = endOfAgePosition + 1 - startOfActualAgePosition;
                string age = OnePerson.Substring(startOfActualAgePosition + 1, ageLength - 2);

                int startOfDescriptionPosition = OnePerson.LastIndexOf($"Name:");
                int endOfDescriptionPosition = OnePerson.IndexOf(';', startOfDescriptionPosition);
                int startOfActualDescriptionPosition = OnePerson.IndexOf(' ', startOfDescriptionPosition);
                int descriptionLength = endOfDescriptionPosition + 1 - startOfActualDescriptionPosition;
                string job = OnePerson.Substring(startOfActualDescriptionPosition + 1, descriptionLength - 2);

                Console.WriteLine($"\t{firstName} {lastName}, {age} years old. Working as {job}");
                
                Console.WriteLine();
            }
        }

        static void UpdatePerson(string peoplesPath, string jobsPath)
        {
            ReadPerson(peoplesPath, jobsPath);

            int TimeEnterPressed = 0;
            UpdatePerson:
            Console.WriteLine();
            Console.Write("Enter the full name of the person that you want to update and there age separated with comma (Example: Georgi Chaparov, 19): ");


            string nameInput = Console.ReadLine();
            string[] personsInformation;
            string firstName = "";
            int lastNameIndex;
            string lastName = "";
            string age = "";
            try
            {
                personsInformation = nameInput.Split(' ');
                firstName = personsInformation[0];
                lastNameIndex = personsInformation[1].Length;
                lastName = personsInformation[1].Remove(lastNameIndex - 1);
                age = personsInformation[2];
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Input was not in the correct format! (Press enter on more time to go back)");
                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    ListPersonMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto UpdatePerson;
                }
            }


            Console.WriteLine();

            string Peoples = File.ReadAllText(peoplesPath);

            if (Peoples.Contains($"FirstName: {firstName}; LastName: {lastName}; Age: {age};"))
            {
                Console.Clear();

                Console.WriteLine($"         Updating {firstName} {lastName}");
                Console.WriteLine();
                Console.WriteLine($"1 - Update the first name of {firstName} {lastName}");
                Console.WriteLine($"2 - Update the last name of {firstName} {lastName}");
                Console.WriteLine($"3 - Update the age of {firstName} {lastName}");
                Console.WriteLine($"4 - Update the profession that {firstName} {lastName} is working");
                Console.WriteLine();

                Console.Write("Please enter the number of the command that you want to execute (If you want to update more then one thing, write the commands number together. Example: 2143): ");
                string input = Console.ReadLine();
                Console.WriteLine();

                int sellection = -1;

                if (int.TryParse(input, out sellection))
                {
                    if (sellection == 1)
                    {
                        TimeEnterPressed = 0;
                        OnlyFirstName:

                        Console.Write("Enter the new first name of the person: ");
                        string newOnlyFirstNameInput = Console.ReadLine();

                        if (newOnlyFirstNameInput.Length == 0)
                        {
                            Console.WriteLine("The first name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto OnlyFirstName;
                            }
                        }

                        if (Peoples.Contains($"First name: {newOnlyFirstNameInput}; Last name: {lastName}; Age: {age};"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto OnlyFirstName;
                        }

                        Peoples = Peoples.Replace($"First name: {firstName}; Last name: {lastName}; Age: {age};", $"First name: {newOnlyFirstNameInput}; Last name: {lastName}; Age: {age};");

                        StreamWriter onlyFirstNameStreamWriter = new StreamWriter(peoplesPath);
                        onlyFirstNameStreamWriter.Write(Peoples);
                        onlyFirstNameStreamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 2)
                    {
                        TimeEnterPressed = 0;
                        OnlyLastName:
                        Console.WriteLine();
                        Console.Write("Enter the new last name of the person: ");
                        string newOnlyLastNameInput = Console.ReadLine();
                        if (newOnlyLastNameInput.Length == 0)
                        {
                            Console.WriteLine("The last name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto OnlyLastName;
                            }
                        }

                        if (Peoples.Contains($"First name: {firstName}; Last name: {newOnlyLastNameInput}; Age: {age};"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto OnlyLastName;
                        }

                        Peoples = Peoples.Replace($"First name: {firstName}; Last name: {lastName}; Age: {age};", $"First name: {firstName}; Last name: {newOnlyLastNameInput}; Age: {age};");

                        StreamWriter onlyLastNameStreamWriter = new StreamWriter(peoplesPath);
                        onlyLastNameStreamWriter.Write(Peoples);
                        onlyLastNameStreamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 3)
                    {
                        TimeEnterPressed = 0;
                        OnlyAge:
                        Console.WriteLine();
                        Console.Write("Enter the new age of the person: ");
                        int newOnlyAgeInput = 0;
                        try
                        {
                            newOnlyAgeInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter only digits! (Press enter on more time to go back)");

                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto OnlyAge;
                            }
                        }

                        if (Peoples.Contains($"First name: {firstName}; Last name: {lastName}; Age: {newOnlyAgeInput};"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto OnlyAge;
                        }

                        Peoples = Peoples.Replace($"First name: {firstName}; Last name: {lastName}; Age: {age};", $"First name: {firstName}; Last name: {lastName}; Age: {newOnlyAgeInput};");

                        StreamWriter onlyAgeStreamWriter = new StreamWriter(peoplesPath);
                        onlyAgeStreamWriter.Write(Peoples);
                        onlyAgeStreamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 4)
                    {
                        JobMenu.ReadJob(peoplesPath, jobsPath);
                        string onlyJobs = File.ReadAllText(jobsPath);

                        int onlyJobStartOfPersonPosition = Peoples.IndexOf($"FirsName: {firstName}; LastName: {lastName}; Age: {age};");
                        int onlyJobStartOfOldJobPosition = Peoples.IndexOf('(', onlyJobStartOfPersonPosition);
                        int onlyJobEndOfOldJobPosition = Peoples.IndexOf(')', onlyJobStartOfPersonPosition);
                        int onlyJoblength = onlyJobEndOfOldJobPosition - onlyJobStartOfOldJobPosition;
                        string onlyJob = Peoples.Substring(onlyJobStartOfOldJobPosition + 1, onlyJoblength - 1);

                        TimeEnterPressed = 0;
                        onlyJob:
                        Console.Write("Enter the name of the new profession of the person: ");
                        string newOnlyJobName = Console.ReadLine();
                        int startOfOnlyNewJobPosition = onlyJobs.IndexOf($"Name: {newOnlyJobName}");
                        int endOfOnlyJobNewPosition = onlyJobs.IndexOf("<^>", startOfOnlyNewJobPosition);
                        int newOnlyJoblength = endOfOnlyJobNewPosition - startOfOnlyNewJobPosition;
                        string newOnlyJob = onlyJobs.Substring(startOfOnlyNewJobPosition, newOnlyJoblength);

                        if (Peoples.Contains($"FirstName: {firstName}; LastName: {lastName}; Age: {age}; Profession - ({newOnlyJob})<^>"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto onlyJob;
                        }

                        Peoples = Peoples.Replace($"FirsName: {firstName}; LastName: {lastName}; Age: {age}; Profession - ({onlyJob})<^>", $"FirstName: {firstName}; LastName: {lastName}; Age: {age}; Profession - ({newOnlyJob})<^>");

                        StreamWriter onlyJobStreamWriter = new StreamWriter(peoplesPath);
                        onlyJobStreamWriter.Write(Peoples);
                        onlyJobStreamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 12 || sellection == 21)
                    {
                        TimeEnterPressed = 0;
                        OnlyFirstAndLastNameFirstName:
                        Console.Write("Enter the new first name of the person: ");
                        string newOnlyFirstAndLastNameFirstNameInput = Console.ReadLine();

                        if (newOnlyFirstAndLastNameFirstNameInput.Length == 0)
                        {
                            Console.WriteLine("The first name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto OnlyFirstAndLastNameFirstName;
                            }
                        }

                        TimeEnterPressed = 0;
                        OnlyFirstAndLastNameLastName:
                        Console.WriteLine();
                        Console.Write("Enter the new last name of the person: ");
                        string newOnlyFirstAndLastNameLastNameInput = Console.ReadLine();
                        if (newOnlyFirstAndLastNameLastNameInput.Length == 0)
                        {
                            Console.WriteLine("The last name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto OnlyFirstAndLastNameLastName;
                            }
                        }

                        if (Peoples.Contains($"First name: {newOnlyFirstAndLastNameFirstNameInput}; Last name: {newOnlyFirstAndLastNameLastNameInput}; Age: {age};"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto OnlyFirstAndLastNameFirstName;
                        }

                        Peoples = Peoples.Replace($"First name: {firstName}; Last name: {lastName}; Age: {age};", $"First name: {newOnlyFirstAndLastNameFirstNameInput}; Last name: {newOnlyFirstAndLastNameLastNameInput}; Age: {age};");

                        StreamWriter onlyFirstAndLastNameStreamWriter = new StreamWriter(peoplesPath);
                        onlyFirstAndLastNameStreamWriter.Write(Peoples);
                        onlyFirstAndLastNameStreamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 123 || sellection == 132 || sellection == 231 || sellection == 213 || sellection == 321 || sellection == 312)
                    {
                        TimeEnterPressed = 0;
                        OnlyFirstAndLasNameAndAgeFirstName:
                        Console.Write("Enter the new first name of the person: ");
                        string newOnlyFirstAndLasNameAndAgeFirstNameInput = Console.ReadLine();

                        if (newOnlyFirstAndLasNameAndAgeFirstNameInput.Length == 0)
                        {
                            Console.WriteLine("The first name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto OnlyFirstAndLasNameAndAgeFirstName;
                            }
                        }

                        TimeEnterPressed = 0;
                        OnlyFirstAndLasNameAndAgeLastName:
                        Console.WriteLine();
                        Console.Write("Enter the new last name of the person: ");
                        string newOnlyFirstAndLasNameAndAgeLastNameInput = Console.ReadLine();
                        if (newOnlyFirstAndLasNameAndAgeLastNameInput.Length == 0)
                        {
                            Console.WriteLine("The last name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto OnlyFirstAndLasNameAndAgeLastName;
                            }
                        }

                        TimeEnterPressed = 0;
                        OnlyFirstAndLasNameAndAgeAge:
                        Console.WriteLine();
                        Console.Write("Enter the new age of the person: ");
                        int newOnlyFirstAndLasNameAndAgeAgeInput = 0;
                        try
                        {
                            newOnlyFirstAndLasNameAndAgeAgeInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter only digits! (Press enter on more time to go back)");

                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto OnlyFirstAndLasNameAndAgeAge;
                            }
                        }

                        if (Peoples.Contains($"FirstName: {newOnlyFirstAndLasNameAndAgeFirstNameInput}; LastName: {newOnlyFirstAndLasNameAndAgeLastNameInput}; Age: {newOnlyFirstAndLasNameAndAgeAgeInput};"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto OnlyFirstAndLasNameAndAgeFirstName;
                        }

                        Peoples = Peoples.Replace($"FirstName: {firstName}; LastName: {lastName}; Age: {age};", $"FirstName: {newOnlyFirstAndLasNameAndAgeFirstNameInput}; LastName: {newOnlyFirstAndLasNameAndAgeLastNameInput}; Age: {newOnlyFirstAndLasNameAndAgeAgeInput};");

                        StreamWriter onlyFirstAndLasNameAndAgeStreamWriter = new StreamWriter(peoplesPath);
                        onlyFirstAndLasNameAndAgeStreamWriter.Write(Peoples);
                        onlyFirstAndLasNameAndAgeStreamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 1234 || sellection == 1243 || sellection == 1324 || sellection == 1342 || sellection == 1423 || sellection == 1432 ||
                             sellection == 2134 || sellection == 2143 || sellection == 2314 || sellection == 2341 || sellection == 2413 || sellection == 2431 ||
                             sellection == 3124 || sellection == 3142 || sellection == 3214 || sellection == 3241 || sellection == 3412 || sellection == 3421 ||
                             sellection == 4123 || sellection == 4132 || sellection == 4213 || sellection == 4231 || sellection == 4312 || sellection == 4321)
                    {
                        TimeEnterPressed = 0;
                        FirstName:
                        Console.Write("Enter the new first name of the person: ");
                        string newFirstNameInput = Console.ReadLine();

                        if (newFirstNameInput.Length == 0)
                        {
                            Console.WriteLine("The first name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto FirstName;
                            }
                        }

                        TimeEnterPressed = 0;
                        LastName:
                        Console.WriteLine();
                        Console.Write("Enter the new last name of the person: ");
                        string newLastNameInput = Console.ReadLine();
                        if (newLastNameInput.Length == 0)
                        {
                            Console.WriteLine("The last name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto LastName;
                            }
                        }

                        TimeEnterPressed = 0;
                        Age:
                        Console.WriteLine();
                        Console.Write("Enter the new age of the person: ");
                        int newAgeInput = 0;
                        try
                        {
                            newAgeInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter only digits! (Press enter on more time to go back)");

                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto Age;
                            }
                        }

                        Console.WriteLine();

                        JobMenu.ReadJob(peoplesPath, jobsPath);
                        string Jobs = File.ReadAllText(jobsPath);

                        int startOfPersonPosition = Peoples.IndexOf($"First name: {firstName}; Last name: {lastName}; Age: {age};");
                        int startOfOldJobPosition = Peoples.IndexOf('(', startOfPersonPosition);
                        int endOfOldJobPosition = Peoples.IndexOf(')', startOfPersonPosition);
                        int length0 = endOfOldJobPosition - startOfOldJobPosition;
                        string oldJob = Peoples.Substring(startOfOldJobPosition + 1, length0 - 1);

                        TimeEnterPressed = 0;
                        Console.Write("Enter the name of the new profession of the person: ");
                        string newJobName = Console.ReadLine();
                        int startOfNewJobPosition = Jobs.IndexOf($"Name: {newJobName}");
                        int endOfJobNewPosition = Jobs.IndexOf("<^>", startOfNewJobPosition);
                        int length = endOfJobNewPosition - startOfNewJobPosition;
                        string newJob = Jobs.Substring(startOfNewJobPosition, length);

                        if (Peoples.Contains($"First name: {newFirstNameInput}; Last name: {newLastNameInput}; Age: {newAgeInput}; Profession - ({newJob})<^>"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto FirstName;
                        }

                        Peoples = Peoples.Replace($"First name: {firstName}; Last name: {lastName}; Age: {age}; Profession - ({oldJob})<^>", $"First name: {newFirstNameInput}; Last name: {newLastNameInput}; Age: {newAgeInput}; Profession - ({newJob})<^>");

                        StreamWriter streamWriter = new StreamWriter(peoplesPath);
                        streamWriter.Write(Peoples);
                        streamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 23 || sellection == 32)
                    {
                        TimeEnterPressed = 0;
                        LastName:
                        Console.WriteLine();
                        Console.Write("Enter the new last name of the person: ");
                        string newLastNameInput = Console.ReadLine();
                        if (newLastNameInput.Length == 0)
                        {
                            Console.WriteLine("The last name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto LastName;
                            }
                        }

                        TimeEnterPressed = 0;
                        Age:
                        Console.WriteLine();
                        Console.Write("Enter the new age of the person: ");
                        int newAgeInput = 0;
                        try
                        {
                            newAgeInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter only digits! (Press enter on more time to go back)");

                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto Age;
                            }
                        }

                        if (Peoples.Contains($"First name: {firstName}; Last name: {newLastNameInput}; Age: {newAgeInput};"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto LastName;
                        }

                        Peoples = Peoples.Replace($"First name: {firstName}; Last name: {lastName}; Age: {age};", $"First name: {firstName}; Last name: {newLastNameInput}; Age: {newAgeInput};");

                        StreamWriter streamWriter = new StreamWriter(peoplesPath);
                        streamWriter.Write(Peoples);
                        streamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 234 || sellection == 243 || sellection == 423 || sellection == 432 || sellection == 342 || sellection == 324)
                    {
                        TimeEnterPressed = 0;
                        LastName:
                        Console.WriteLine();
                        Console.Write("Enter the new last name of the person: ");
                        string newLastNameInput = Console.ReadLine();
                        if (newLastNameInput.Length == 0)
                        {
                            Console.WriteLine("The last name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto LastName;
                            }
                        }

                        TimeEnterPressed = 0;
                        Age:
                        Console.WriteLine();
                        Console.Write("Enter the new age of the person: ");
                        int newAgeInput = 0;
                        try
                        {
                            newAgeInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter only digits! (Press enter on more time to go back)");

                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto Age;
                            }
                        }

                        Console.WriteLine();

                        JobMenu.ReadJob(peoplesPath, jobsPath);
                        string Jobs = File.ReadAllText(jobsPath);

                        int startOfPersonPosition = Peoples.IndexOf($"FirstName: {firstName}; LastName: {lastName}; Age: {age};");
                        int startOfOldJobPosition = Peoples.IndexOf('(', startOfPersonPosition);
                        int endOfOldJobPosition = Peoples.IndexOf(')', startOfPersonPosition);
                        int length0 = endOfOldJobPosition - startOfOldJobPosition;
                        string oldJob = Peoples.Substring(startOfOldJobPosition + 1, length0 - 1);

                        TimeEnterPressed = 0;
                        Console.Write("Enter the name of the new profession of the person: ");
                        string newJobName = Console.ReadLine();
                        int startOfNewJobPosition = Jobs.IndexOf($"Name: {newJobName}");
                        int endOfJobNewPosition = Jobs.IndexOf("<^>", startOfNewJobPosition);
                        int length = endOfJobNewPosition - startOfNewJobPosition;
                        string newJob = Jobs.Substring(startOfNewJobPosition, length);

                        if (Peoples.Contains($"FirstName: {firstName}; LastName: {newLastNameInput}; Age: {newAgeInput}; Profession - ({newJob})<^>"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto LastName;
                        }

                        Peoples = Peoples.Replace($"FirstName: {firstName}; LastName: {lastName}; Age: {age}; Profession - ({oldJob})<^>", $"FirstName: {firstName}; LastName: {newLastNameInput}; Age: {newAgeInput}; Profession - ({newJob})<^>");

                        StreamWriter streamWriter = new StreamWriter(peoplesPath);
                        streamWriter.Write(Peoples);
                        streamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 31 || sellection == 13)
                    {
                        FirstName:
                        Console.Write("Enter the new first name of the person: ");
                        string newFirstNameInput = Console.ReadLine();

                        if (newFirstNameInput.Length == 0)
                        {
                            Console.WriteLine("The first name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto FirstName;
                            }
                        }

                        TimeEnterPressed = 0;
                        Age:
                        Console.WriteLine();
                        Console.Write("Enter the new age of the person: ");
                        int newAgeInput = 0;
                        try
                        {
                            newAgeInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter only digits! (Press enter on more time to go back)");

                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto Age;
                            }
                        }

                        if (Peoples.Contains($"First name: {newFirstNameInput}; Last name: {lastName}; Age: {newAgeInput};"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto FirstName;
                        }

                        Peoples = Peoples.Replace($"First name: {firstName}; Last name: {lastName}; Age: {age};", $"First name: {newFirstNameInput}; Last name: {lastName}; Age: {newAgeInput};");

                        StreamWriter streamWriter = new StreamWriter(peoplesPath);
                        streamWriter.Write(Peoples);
                        streamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 314 || sellection == 341 || sellection == 431 || sellection == 413 || sellection == 134 || sellection == 143)
                    {
                        TimeEnterPressed = 0;
                        FirstName:
                        Console.Write("Enter the new first name of the person: ");
                        string newFirstNameInput = Console.ReadLine();

                        if (newFirstNameInput.Length == 0)
                        {
                            Console.WriteLine("The first name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto FirstName;
                            }
                        }

                        TimeEnterPressed = 0;
                        Age:
                        Console.WriteLine();
                        Console.Write("Enter the new age of the person: ");
                        int newAgeInput = 0;
                        try
                        {
                            newAgeInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter only digits! (Press enter on more time to go back)");

                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto Age;
                            }
                        }

                        Console.WriteLine();

                        JobMenu.ReadJob(peoplesPath, jobsPath);
                        string Jobs = File.ReadAllText(jobsPath);

                        int startOfPersonPosition = Peoples.IndexOf($"FirstName: {firstName}; LastName: {lastName}; Age: {age};");
                        int startOfOldJobPosition = Peoples.IndexOf('(', startOfPersonPosition);
                        int endOfOldJobPosition = Peoples.IndexOf(')', startOfPersonPosition);
                        int length0 = endOfOldJobPosition - startOfOldJobPosition;
                        string oldJob = Peoples.Substring(startOfOldJobPosition + 1, length0 - 1);

                        TimeEnterPressed = 0;
                        Console.Write("Enter the name of the new profession of the person: ");
                        string newJobName = Console.ReadLine();
                        int startOfNewJobPosition = Jobs.IndexOf($"Name: {newJobName}");
                        int endOfJobNewPosition = Jobs.IndexOf("<^>", startOfNewJobPosition);
                        int length = endOfJobNewPosition - startOfNewJobPosition;
                        string newJob = Jobs.Substring(startOfNewJobPosition, length);

                        if (Peoples.Contains($"FirstName: {newFirstNameInput}; LastName: {lastName}; Age: {newAgeInput}; Profession - ({newJob})<^>"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto FirstName;
                        }

                        Peoples = Peoples.Replace($"FirstName: {firstName}; LastName: {lastName}; Age: {age}; Profession - ({oldJob})<^>", $"FirstName: {newFirstNameInput}; LastName: {lastName}; Age: {newAgeInput}; Profession - ({newJob})<^>");

                        StreamWriter streamWriter = new StreamWriter(peoplesPath);
                        streamWriter.Write(Peoples);
                        streamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 34 || sellection == 43)
                    {
                        TimeEnterPressed = 0;
                        Age:
                        Console.WriteLine();
                        Console.Write("Enter the new age of the person: ");
                        int newAgeInput = 0;
                        try
                        {
                            newAgeInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter only digits! (Press enter on more time to go back)");

                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto Age;
                            }
                        }

                        Console.WriteLine();

                        JobMenu.ReadJob(peoplesPath, jobsPath);
                        string Jobs = File.ReadAllText(jobsPath);

                        int startOfPersonPosition = Peoples.IndexOf($"FirstName: {firstName}; LastName: {lastName}; Age: {age};");
                        int startOfOldJobPosition = Peoples.IndexOf('(', startOfPersonPosition);
                        int endOfOldJobPosition = Peoples.IndexOf(')', startOfPersonPosition);
                        int length0 = endOfOldJobPosition - startOfOldJobPosition;
                        string oldJob = Peoples.Substring(startOfOldJobPosition + 1, length0 - 1);

                        TimeEnterPressed = 0;
                        Console.Write("Enter the name of the new profession of the person: ");
                        string newJobName = Console.ReadLine();
                        int startOfNewJobPosition = Jobs.IndexOf($"Name: {newJobName}");
                        int endOfJobNewPosition = Jobs.IndexOf("<^>", startOfNewJobPosition);
                        int length = endOfJobNewPosition - startOfNewJobPosition;
                        string newJob = Jobs.Substring(startOfNewJobPosition, length);

                        if (Peoples.Contains($"FirstName: {firstName}; LastName: {lastName}; Age: {newAgeInput}; Profession - ({newJob})<^>"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto Age;
                        }

                        Peoples = Peoples.Replace($"FirstName: {firstName}; LastName: {lastName}; Age: {age}; Profession - ({oldJob})<^>", $"FirstName: {firstName}; LastName: {lastName}; Age: {newAgeInput}; Profession - ({newJob})<^>");

                        StreamWriter streamWriter = new StreamWriter(peoplesPath);
                        streamWriter.Write(Peoples);
                        streamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 41 || sellection == 14)
                    {
                        TimeEnterPressed = 0;
                        FirstName:
                        Console.Write("Enter the new first name of the person: ");
                        string newFirstNameInput = Console.ReadLine();

                        if (newFirstNameInput.Length == 0)
                        {
                            Console.WriteLine("The first name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto FirstName;
                            }
                        }

                        Console.WriteLine();

                        JobMenu.ReadJob(peoplesPath, jobsPath);
                        string Jobs = File.ReadAllText(jobsPath);

                        int startOfPersonPosition = Peoples.IndexOf($"FirstName: {firstName}; LastName: {lastName}; Age: {age};");
                        int startOfOldJobPosition = Peoples.IndexOf('(', startOfPersonPosition);
                        int endOfOldJobPosition = Peoples.IndexOf(')', startOfPersonPosition);
                        int length0 = endOfOldJobPosition - startOfOldJobPosition;
                        string oldJob = Peoples.Substring(startOfOldJobPosition + 1, length0 - 1);

                        TimeEnterPressed = 0;
                        Console.Write("Enter the name of the new profession of the person: ");
                        string newJobName = Console.ReadLine();
                        int startOfNewJobPosition = Jobs.IndexOf($"Name: {newJobName}");
                        int endOfJobNewPosition = Jobs.IndexOf("<^>", startOfNewJobPosition);
                        int length = endOfJobNewPosition - startOfNewJobPosition;
                        string newJob = Jobs.Substring(startOfNewJobPosition, length);

                        if (Peoples.Contains($"FirstName: {newFirstNameInput}; LastName: {lastName}; Age: {age}; Profession - ({newJob})<^>"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto FirstName;
                        }

                        Peoples = Peoples.Replace($"FirstName: {firstName}; LastName: {lastName}; Age: {age}; Profession - ({oldJob})<^>", $"FirstName: {newFirstNameInput}; LastName: {lastName}; Age: {age}; Profession - ({newJob})<^>");

                        StreamWriter streamWriter = new StreamWriter(peoplesPath);
                        streamWriter.Write(Peoples);
                        streamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 42 || sellection == 24)
                    {
                        TimeEnterPressed = 0;
                        LastName:
                        Console.WriteLine();
                        Console.Write("Enter the new last name of the person: ");
                        string newLastNameInput = Console.ReadLine();
                        if (newLastNameInput.Length == 0)
                        {
                            Console.WriteLine("The last name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto LastName;
                            }
                        }

                        Console.WriteLine();

                        JobMenu.ReadJob(peoplesPath, jobsPath);
                        string Jobs = File.ReadAllText(jobsPath);

                        int startOfPersonPosition = Peoples.IndexOf($"FirstName: {firstName}; LastName: {lastName}; Age: {age};");
                        int startOfOldJobPosition = Peoples.IndexOf('(', startOfPersonPosition);
                        int endOfOldJobPosition = Peoples.IndexOf(')', startOfPersonPosition);
                        int length0 = endOfOldJobPosition - startOfOldJobPosition;
                        string oldJob = Peoples.Substring(startOfOldJobPosition + 1, length0 - 1);

                        TimeEnterPressed = 0;
                        Console.Write("Enter the name of the new profession of the person: ");
                        string newJobName = Console.ReadLine();
                        int startOfNewJobPosition = Jobs.IndexOf($"Name: {newJobName}");
                        int endOfJobNewPosition = Jobs.IndexOf("<^>", startOfNewJobPosition);
                        int length = endOfJobNewPosition - startOfNewJobPosition;
                        string newJob = Jobs.Substring(startOfNewJobPosition, length);

                        if (Peoples.Contains($"FirstName: {firstName}; LastName: {newLastNameInput}; Age: {age}; Profession - ({newJob})<^>"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto LastName;
                        }

                        Peoples = Peoples.Replace($"FirstName: {firstName}; LastName: {lastName}; Age: {age}; Profession - ({oldJob})<^>", $"FirstName: {firstName}; LastName: {newLastNameInput}; Age: {age}; Profession - ({newJob})<^>");

                        StreamWriter streamWriter = new StreamWriter(peoplesPath);
                        streamWriter.Write(Peoples);
                        streamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                    else if (sellection == 412 || sellection == 421 || sellection == 142 || sellection == 124 || sellection == 214 || sellection == 241)
                    {
                        TimeEnterPressed = 0;
                        FirstName:
                        Console.Write("Enter the new first name of the person: ");
                        string newFirstNameInput = Console.ReadLine();

                        if (newFirstNameInput.Length == 0)
                        {
                            Console.WriteLine("The first name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto FirstName;
                            }
                        }

                        TimeEnterPressed = 0;
                        LastName:
                        Console.WriteLine();
                        Console.Write("Enter the new last name of the person: ");
                        string newLastNameInput = Console.ReadLine();
                        if (newLastNameInput.Length == 0)
                        {
                            Console.WriteLine("The last name of the person can't be \"nothing\"! (Press enter on more time to go back)");
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListPersonMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto LastName;
                            }
                        }

                        Console.WriteLine();

                        JobMenu.ReadJob(peoplesPath, jobsPath);
                        string Jobs = File.ReadAllText(jobsPath);

                        int startOfPersonPosition = Peoples.IndexOf($"FirstName: {firstName}; LastName: {lastName}; Age: {age};");
                        int startOfOldJobPosition = Peoples.IndexOf('(', startOfPersonPosition);
                        int endOfOldJobPosition = Peoples.IndexOf(')', startOfPersonPosition);
                        int length0 = endOfOldJobPosition - startOfOldJobPosition;
                        string oldJob = Peoples.Substring(startOfOldJobPosition + 1, length0 - 1);

                        TimeEnterPressed = 0;
                        Console.Write("Enter the name of the new profession of the person: ");
                        string newJobName = Console.ReadLine();
                        int startOfNewJobPosition = Jobs.IndexOf($"Name: {newJobName}");
                        int endOfJobNewPosition = Jobs.IndexOf("<^>", startOfNewJobPosition);
                        int length = endOfJobNewPosition - startOfNewJobPosition;
                        string newJob = Jobs.Substring(startOfNewJobPosition, length);

                        if (Peoples.Contains($"FirstName: {newFirstNameInput}; LastName: {newLastNameInput}; Age: {age}; Profession - ({newJob})<^>"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("This person already exist!");
                            goto FirstName;
                        }

                        Peoples = Peoples.Replace($"FirstName: {firstName}; LastName: {lastName}; Age: {age}; Profession - ({oldJob})<^>", $"FirstName: {newFirstNameInput}; LastName: {newLastNameInput}; Age: {age}; Profession - ({newJob})<^>");

                        StreamWriter streamWriter = new StreamWriter(peoplesPath);
                        streamWriter.Write(Peoples);
                        streamWriter.Close();


                        Console.WriteLine();
                        Console.WriteLine("The person has been updated!");
                    }
                }                           
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("A person with this name and age does not exist! (Press enter on more time to go back)");

                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    ListPersonMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto UpdatePerson;
                }
            }
        }

        static void DeletePerson(string peoplesPath, string jobsPath)
        {
            ReadPerson(peoplesPath, jobsPath);

            string Peoples = File.ReadAllText(peoplesPath);
            int TimeEnterPressed = 0;

            DeletePerson:
            Console.WriteLine();
            Console.Write("Enter the full name of the person that you want to delete and there age separated with comma (Example: Georgi Chaparov, 19): ");

            string nameInput = Console.ReadLine();
            string[] personsInformation;
            string firstName = "";
            int lastNameIndex;
            string lastName = "";
            string age = "";
            try
            {
                personsInformation = nameInput.Split(' ');
                firstName = personsInformation[0];
                lastNameIndex = personsInformation[1].Length;
                lastName = personsInformation[1].Remove(lastNameIndex - 1);
                age = personsInformation[2];
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Input was not in the correct format!");
                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    ListPersonMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto DeletePerson;
                }
            }

            TimeEnterPressed = 0;
            if (Peoples.Contains($"First name: {firstName}; Last name: {lastName}; Age: {age};"))
            {             
                int startOfJobPosition = Peoples.IndexOf($"First name: {firstName}; Last name: {lastName}; Age: {age};");
                int endOfJobPosition = Peoples.IndexOf("<^>", startOfJobPosition);
                int length = (endOfJobPosition + 4) - startOfJobPosition;

                try
                {
                    Peoples = Peoples.Remove(startOfJobPosition, length + 2);
                }
                catch (Exception)
                {
                    Peoples = Peoples.Remove(startOfJobPosition, length + 4);
                }

                StreamWriter streamWriter = new StreamWriter(peoplesPath);
                streamWriter.Write(Peoples);
                streamWriter.Close();

                Console.WriteLine();
                Console.WriteLine("The person has been deleted!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("A person with this name and age does not exist! (Press enter on more time to go back)");
                TimeEnterPressed++;

                if (TimeEnterPressed == 2)
                {
                    ListPersonMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto DeletePerson;
                }
            }
        }
    }
}
