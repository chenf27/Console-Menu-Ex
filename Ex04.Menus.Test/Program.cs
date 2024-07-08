using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    internal class Program
    {
        public const bool v_IsMenu = true;
        public static void Main()
        {
            Interfaces.MenuItem mainMenuItem = MenuItem(null, "Main Menu", v_IsMenu);
            Interfaces.MainMenu mainMenu = new Interfaces.MainMenu(mainMenuItem);

            mainMenu.Show();
        }
    }
}
