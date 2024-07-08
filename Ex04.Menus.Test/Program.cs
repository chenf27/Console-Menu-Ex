using Ex04.Menus.Interfaces;
using System;

namespace Ex04.Menus.Test
{
    internal class Program
    {
        public static void Main()
        {
            MainMenu mainMenu = new MainMenu("Main Menu");
            MenuItem mainMenuItem = mainMenu.MenuItem;
            MenuItem subMenu1 = new MenuItem(mainMenuItem, "sub menu 1", true);
            MenuItem subMenu2 = new MenuItem(mainMenuItem, "sub menu 2", true);
            MenuItem action1_1 = new MenuItem(mainMenuItem, "action 1", new Action1());
            MenuItem action1_2 = new MenuItem(mainMenuItem, "action 1", new Action1());
            MenuItem action2_1 = new MenuItem(mainMenuItem, "action 2", new Action2());
            MenuItem action2_2 = new MenuItem(mainMenuItem, "action 2", new Action2());

            mainMenu.AddMenuItem(mainMenuItem, subMenu1);
            mainMenu.AddMenuItem(mainMenuItem, subMenu2);
            mainMenu.AddMenuItem(mainMenuItem, action1_1);
            mainMenu.AddMenuItem(subMenu1, action1_2);
            mainMenu.AddMenuItem(subMenu1, action2_1);
            mainMenu.AddMenuItem(subMenu2, action2_2);

            mainMenu.Show();
        }
    }

    public class Action1 : IMenuAction
    {
        void IMenuAction.Execute() 
        {
            Console.WriteLine("Action 1 executed!");
        }
    }
    
    public class Action2 : IMenuAction
    {
        void IMenuAction.Execute() 
        {
            Console.WriteLine("Action 2 executed!");
        }
    }
}
