using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace local_DB.txt_file.Menu
{
    public static class JobMenu
    {
        public static void ListJobMenu(string peoplesPath, string jobsPath)
        {
            ListCommands:
            Console.Clear();

            Console.WriteLine("         Job Menu");
            Console.WriteLine();
            Console.WriteLine("1 - Create job");
            Console.WriteLine("2 - Read job");
            Console.WriteLine("3 - Update job");
            Console.WriteLine("4 - Delete job");
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
                    CreateJob(peoplesPath, jobsPath);
                }
                else if (selection == 2)
                {
                    ReadJob(peoplesPath, jobsPath);
                }
                else if (selection == 3)
                {
                    UpdateJob(peoplesPath, jobsPath);
                }
                else if (selection == 4)
                {
                    DeleteJob(peoplesPath, jobsPath);
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

        static void CreateJob(string peoplesPath, string jobsPath)
        {
            int TimeEnterPressed = 0;

            string Jobs = File.ReadAllText(jobsPath);

            name:
            Console.WriteLine();
            Console.Write("Enter the name of the job: ");
            string name = Console.ReadLine();

            if (name.Length == 0)
            {
                Console.WriteLine();
                Console.WriteLine("The name can't be \"nothing\"! (Press enter on more time to go back)");
                Console.WriteLine();
                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    ListJobMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto name;
                }
            }
            else if (Jobs.Contains($"Name: {name};"))
            {
                Console.WriteLine();
                Console.WriteLine("This Job already exist!");
                Console.WriteLine();
                goto name;
            }

            Console.WriteLine();

            Console.Write("Enter the description of the job: ");
            string description = Console.ReadLine();

            StreamWriter streamWriter = new StreamWriter(jobsPath, true);
            streamWriter.WriteLine($"\tName: {name}; Description: {description}<^>");
            streamWriter.Close();

            Console.WriteLine();
            Console.WriteLine("Job has been added!");
        }

        public static void ReadJob(string peoplesPath, string jobsPath)
        {
            Console.WriteLine();
            Console.WriteLine("Jobs:");
            Console.WriteLine(File.ReadAllText(jobsPath));
        }

        static void UpdateJob(string peoplesPath, string jobsPath)
        {
            int TimeEnterPressed = 0;
            ReadJob(peoplesPath, jobsPath);

            nameInput:
            Console.WriteLine();
            Console.Write("Enter the name of the job that you want to update: ");

            string nameInput = Console.ReadLine();

            Console.WriteLine();

            string Jobs = File.ReadAllText(jobsPath);
            string Peoples = File.ReadAllText(peoplesPath);

            if (Jobs.Contains($"Name: {nameInput};"))
            {                
                Console.Clear();

                Console.WriteLine($"         Updating {nameInput}");
                Console.WriteLine();
                Console.WriteLine($"1 - Update the name of {nameInput}");
                Console.WriteLine($"2 - Update the description of {nameInput}");
                Console.WriteLine();

                Updating:
                Console.Write("Please enter the number of the command that you want to execute (If you want to update more then one thing, write the commands number together. Example: 21): ");
                string input = Console.ReadLine();

                Console.WriteLine(input);
                Console.WriteLine();

                int sellection = -1;
                if (int.TryParse(input, out sellection))
                {
                    if (sellection == 1)
                    {
                        
                        onlyname:
                        Console.Write("Enter the new name of the job: ");

                        string newOnlyNameInput = Console.ReadLine();
                        
                        if (newOnlyNameInput.Length == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("The name can't be \"nothing\"! (Press enter on more time to go back)");
                            Console.WriteLine();
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListJobMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto onlyname;
                            }
                        }

                        Jobs = Jobs.Replace($"Name: {nameInput};", $"Name: {newOnlyNameInput};");

                        StreamWriter nameStreamWriter = new StreamWriter(jobsPath);
                        nameStreamWriter.Write(Jobs);
                        nameStreamWriter.Close();

                        Console.WriteLine();
                        Console.WriteLine("The job has been updated!");
                    }
                    else if (sellection == 2)
                    {
                        Console.Write("Enter the new description of the job: ");
                        string onlyNewDescription = Console.ReadLine();

                        #region Find the position of the old description and saves it 
                        int startOfJobPositionForDescription = Jobs.IndexOf($"Name: {nameInput}");
                        int endOfJobPositionForDescription = Jobs.IndexOf("<^>", startOfJobPositionForDescription);
                        int indexOfOnlyDescription = Jobs.IndexOf("Description: ", startOfJobPositionForDescription);
                        int length1 = endOfJobPositionForDescription - indexOfOnlyDescription;
                        string onlyOldDescription = Jobs.Substring(indexOfOnlyDescription, length1);
                        #endregion

                        Jobs = Jobs.Replace($"Name: {nameInput}; {onlyOldDescription}", $"Name: {nameInput}; Description: {onlyNewDescription}");
                        Peoples = Peoples.Replace($"Name: {nameInput}; {onlyOldDescription}", $"Name: {nameInput}; Description: {onlyNewDescription}");
                        
                        StreamWriter descriptionStreamWriter = new StreamWriter(jobsPath);
                        descriptionStreamWriter.Write(Jobs);
                        descriptionStreamWriter.Close();
                        StreamWriter DescritionStreamWriter = new StreamWriter(peoplesPath);
                        DescritionStreamWriter.Write(Peoples);
                        DescritionStreamWriter.Close();

                        Console.WriteLine();
                        Console.WriteLine("The job has been updated!");
                    }
                    else if (sellection == 12 || sellection == 21)
                    {
                        TimeEnterPressed = 0;

                        name:
                        Console.Write("Enter the new name of the job: ");
                        string newNameInput = Console.ReadLine();                   

                        if (newNameInput.Length == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("The name can't be \"nothing\"! (Press enter on more time to go back)");
                            Console.WriteLine();
                            TimeEnterPressed++;
                            if (TimeEnterPressed == 2)
                            {
                                ListJobMenu(peoplesPath, jobsPath);
                                return;
                            }
                            else
                            {
                                goto name;
                            }
                        }

                        Jobs = Jobs.Replace($"Name: {nameInput};", $"Name: {newNameInput};");
                        Console.WriteLine();

                        Console.Write("Enter the new description of the job: ");
                        string newDescription = Console.ReadLine();

                        #region Update
                        #region Find the position of the old description and saves it 
                        int startOfJobPosition = Jobs.IndexOf($"Name: {newNameInput}");
                        int endOfJobPosition = Jobs.IndexOf("<^>", startOfJobPosition);
                        int indexOfDescription = Jobs.IndexOf("Description: ", startOfJobPosition);
                        int length = endOfJobPosition - indexOfDescription;
                        string oldDescription = Jobs.Substring(indexOfDescription, length);
                        #endregion

                        #region Update the name and description
                        Jobs = Jobs.Replace($"Name: {nameInput}; {oldDescription}", $"Name: {newNameInput}; Description: {newDescription}");
                        Peoples = Peoples.Replace($"Name: {nameInput}; {oldDescription}", $"Name: {newNameInput}; Description: {newDescription}");
                        #endregion
                        StreamWriter streamWriter = new StreamWriter(jobsPath);
                        streamWriter.Write(Jobs);
                        streamWriter.Close();
                        StreamWriter StreamWriter = new StreamWriter(peoplesPath);
                        StreamWriter.Write(Peoples);
                        StreamWriter.Close();
                        #endregion

                        Console.WriteLine();
                        Console.WriteLine("The job has been updated!");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Select a correct number!");
                        Console.ReadLine();
                        goto Updating;
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
                        goto Updating;
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("A job with this name does not exist!");
                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    ListJobMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto nameInput;
                }
            }
        }

        static void DeleteJob(string peoplesPath, string jobsPath)
        {
            int TimeEnterPressed = 0;
            ReadJob(peoplesPath, jobsPath);
            jobName:
            Console.WriteLine();
            Console.Write("Enter the name of the job that you want to delete: ");
            string jobName = Console.ReadLine();
            string Jobs = File.ReadAllText(jobsPath);
            string Peoples = File.ReadAllText(peoplesPath);


            if (Jobs.Contains($"Name: {jobName};"))
            {
                #region Find the position of the Job and saves it
                int startOfJobPosition = Jobs.IndexOf($"Name: {jobName}");
                int endOfJobPosition = Jobs.IndexOf("<^>", startOfJobPosition);
                int length = (endOfJobPosition + 2) - startOfJobPosition;
                string job = Jobs.Substring(startOfJobPosition, length - 2);
                #endregion

                if (Peoples.Contains(job))
                {
                    Console.WriteLine();
                    Console.WriteLine("There is a person that is using this profession! Delete the person or change his profession before deleting this one.");
                    return;
                }

                #region Remove the Job from the file
                try
                {
                    Jobs = Jobs.Remove(startOfJobPosition - 2, length + 4); //If its the middle element or the last
                }
                catch (Exception)
                {
                    Jobs = Jobs.Remove(startOfJobPosition, length + 4); //If its the first element
                }

                StreamWriter streamWriter = new StreamWriter(jobsPath);
                streamWriter.Write(Jobs);
                streamWriter.Close();
                #endregion

                Console.WriteLine();
                Console.WriteLine("The job has been deleted!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("A job with this name does not exist!");
                TimeEnterPressed++;
                if (TimeEnterPressed == 2)
                {
                    ListJobMenu(peoplesPath, jobsPath);
                    return;
                }
                else
                {
                    goto jobName;
                }
            }
        }
    }
}
