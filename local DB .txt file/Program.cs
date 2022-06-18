using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using local_DB.txt_file.Menu;

namespace local_DB.txt_file
{
    public class Program
    {
        static void Main(string[] args)
        {
            string peoplesPath = @"C:\Users\User\source\repos\local DB .txt file\local DB .txt file\Data\Peoples.txt";
            string jobsPath = @"C:\Users\User\source\repos\local DB .txt file\local DB .txt file\Data\Jobs.txt";

            if (!File.Exists(peoplesPath))
            {
                File.Create(peoplesPath);
            }

            if (!File.Exists(jobsPath))
            {
                File.Create(jobsPath);
            }

            while (true)
            {              
                Console.Clear();
                MainMenu(peoplesPath, jobsPath);
                Console.ReadLine();
            }          
        }

        public static void MainMenu(string peoplesPath, string jobsPath)
        {
            Console.WriteLine("         Main Menu");
            Console.WriteLine();

            Console.WriteLine("1 - Person menu");
            Console.WriteLine("2 - Job menu");
            Console.WriteLine("0 - Exit");
            
            Console.WriteLine();

            Console.Write("Enter command: ");
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
                    PersonMenu.ListPersonMenu(peoplesPath, jobsPath);
                }
                else if (selection == 2)
                {
                    JobMenu.ListJobMenu(peoplesPath, jobsPath);
                }
                else
                {
                    Console.WriteLine("Select a correct number!");
                }
            }
            else
            {
                Console.WriteLine("Please select a digit");
            }
        }
    }
}
