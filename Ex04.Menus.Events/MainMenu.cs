using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Events
{
    public class MainMenu
    {
        private List<MenuItem> r_MenuItems;

        public MainMenu()
        {
            r_MenuItems = new List<MenuItem>();
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            r_MenuItems.Add(menuItem);
        }


    }
}
