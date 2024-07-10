using Ex04.Menus.Interfaces;
using Ex04.Menus.Events;
using System;

namespace Ex04.Menus.Test
{
    internal class Program
    {
        public static void Main()
        {
            Run();
        }
        
        public static void Run()
        {
            try
            {
                Interfaces.MainMenu mainMenuInterfaces = CreateMainMenuInterfaces();
                mainMenuInterfaces.Show();

                Events.MainMenu mainMenuEvents = CreateMainMenuEvents();
                mainMenuEvents.Show();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("{0}.", ex.Message, ex.ParamName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static Interfaces.MainMenu CreateMainMenuInterfaces() 
        {
            Interfaces.MainMenu mainMenu = new Interfaces.MainMenu("Interfaces Main Menu");
            Interfaces.MenuItem mainMenuItem = mainMenu.MenuItem;
            Interfaces.MenuItem versionsAndCapitalsMenu = new Interfaces.MenuItem(mainMenuItem, "Versions and Capitals", true);
            Interfaces.MenuItem showDateTimeMenu = new Interfaces.MenuItem(mainMenuItem, "Show Date/Time", true);
            Interfaces.MenuItem showVersion = new Interfaces.MenuItem(versionsAndCapitalsMenu, "Show Version", new ShowVersion());
            Interfaces.MenuItem CountCapitals = new Interfaces.MenuItem(versionsAndCapitalsMenu, "Count Capitals", new CountCapitals());
            Interfaces.MenuItem showTime = new Interfaces.MenuItem(showDateTimeMenu, "Show Time", new ShowTime());
            Interfaces.MenuItem showDate = new Interfaces.MenuItem(showDateTimeMenu, "Show Date", new ShowDate());

            mainMenu.AddMenuItem(mainMenuItem, versionsAndCapitalsMenu);
            mainMenu.AddMenuItem(mainMenuItem, showDateTimeMenu);
            mainMenu.AddMenuItem(versionsAndCapitalsMenu, showVersion);
            mainMenu.AddMenuItem(versionsAndCapitalsMenu, CountCapitals);
            mainMenu.AddMenuItem(showDateTimeMenu, showTime);
            mainMenu.AddMenuItem(showDateTimeMenu, showDate);
                
            return mainMenu;
        }
        
        public static Events.MainMenu CreateMainMenuEvents() //TODO add exceptions, also make AddMenuItem the same for both versions (change mine or yours)
        {
            Action showTimeAction = () => Console.WriteLine("Current time: {0}", DateTime.Now.ToString("HH:mm:ss"));
            Action showDateAction = () => Console.WriteLine("Current Date: {0}", DateTime.Now.ToShortDateString());
            Action showVersionAction = () => Console.WriteLine("App Version: 24.2.4.9504");
            Action showCapitalsAction = () => CountCapitalLetters();
            Events.MainMenu mainMenu = new Events.MainMenu("Delegates Main Menu");
            Events.MenuItem mainMenuItem = mainMenu.MainMenuItem;
            Events.MenuItem versionsAndCapitalsMenu = new Events.MenuItem(mainMenuItem, "Versions and Capitals", true);
            Events.MenuItem showDateTimeMenu = new Events.MenuItem(mainMenuItem, "Show Date/Time", true);
            Events.MenuItem showTime = new Events.MenuItem(showDateTimeMenu, "Show Time", showTimeAction);
            Events.MenuItem showDate = new Events.MenuItem(showDateTimeMenu, "Show Date", showDateAction);
            Events.MenuItem showVersion = new Events.MenuItem(versionsAndCapitalsMenu, "Show Version", showVersionAction);
            Events.MenuItem showCapitals = new Events.MenuItem(versionsAndCapitalsMenu, "Count Capitals", CountCapitalLetters);

            mainMenu.AddMenuItem(mainMenuItem, versionsAndCapitalsMenu);
            mainMenu.AddMenuItem(mainMenuItem, showDateTimeMenu);
            mainMenu.AddMenuItem(versionsAndCapitalsMenu, showVersion);
            mainMenu.AddMenuItem(versionsAndCapitalsMenu, showCapitals);
            mainMenu.AddMenuItem(showDateTimeMenu, showTime);
            mainMenu.AddMenuItem(showDateTimeMenu, showDate);

            return mainMenu;
        }

        public static void CountCapitalLetters()
        {
            string userInput;
            int capitalLettersCounter = 0;

            Console.WriteLine("Please write a sentence:");
            userInput = Console.ReadLine();
            foreach (char character in userInput)
            {
                if (Char.IsUpper(character))
                {
                    capitalLettersCounter++;
                }
            }

            Console.WriteLine("Number of capital letter(s) in your sentence: {0}", capitalLettersCounter);
        }
    }


    public class ShowVersion : IMenuAction
    {
        void IMenuAction.Execute() 
        {
            Console.WriteLine("App Version: 24.2.4.9504");
        }
    }

    public class CountCapitals : IMenuAction
    {
        void IMenuAction.Execute() 
        {
            string userInput;
            int capitalLettersCounter = 0;

            Console.WriteLine("Please write a sentence:");
            userInput = Console.ReadLine();
            foreach(char character in userInput)
            {
                if(Char.IsUpper(character))
                {
                    capitalLettersCounter++;
                }
            }

            Console.WriteLine("Number of capital letter(s) in your sentence: {0}", capitalLettersCounter);
        }
    }

    public class ShowTime : IMenuAction
    {
        void IMenuAction.Execute()
        {
            Console.WriteLine("Current time: {0}", DateTime.Now.ToString("HH:mm:ss"));
        }
    }

    public class ShowDate : IMenuAction
    {
        void IMenuAction.Execute()
        {
            Console.WriteLine("Current Date: {0}", DateTime.Now.ToShortDateString());
        }
    }
}
