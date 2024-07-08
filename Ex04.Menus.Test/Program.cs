using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Events;


namespace Ex04.Menus.Test
{
    internal class Program
    {
        public const bool v_IsMenu = true;
        public static void Main()
        {
            Action showTimeAction = () => Console.WriteLine($"Current time: {DateTime.Now}");
            Action showDateAction = () => Console.WriteLine($"Current date: {DateTime.Now.ToShortDateString()}");

            // Create menu items
            MenuItem mainMenu = new MenuItem(null, "Main Menu", true);
            MenuItem subMenu1 = new MenuItem(mainMenu, "Sub Menu 1", true);
            MenuItem subMenu2 = new MenuItem(mainMenu, "Sub Menu 2", true);
            MenuItem actionItem1 = new MenuItem(subMenu1, "Show Time", false);
            actionItem1.SetAction(showTimeAction);
            MenuItem actionItem2 = new MenuItem(subMenu2, "Show Date", false);
            actionItem2.SetAction(showDateAction);

            // Add sub menu items
            subMenu1.AddSubMenuItem(actionItem1);
            subMenu2.AddSubMenuItem(actionItem2);
            mainMenu.AddSubMenuItem(subMenu1);
            mainMenu.AddSubMenuItem(subMenu2);

            // Create main menu instance
            MainMenu menu = new MainMenu("Main Menu");
            menu.AddMenuItem(menu.Main, subMenu1);
            menu.AddMenuItem(menu.Main, subMenu2);
            menu.AddMenuItem(subMenu1, actionItem1);
            menu.AddMenuItem(subMenu2, actionItem2);

            // Display the menu
            menu.Show();
        }
    }
}
