using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : ISelectedItem
    {
        private Menu m_MainMenu;
        
        public void AddMenuItem()
        {
            string title = "";
            MenuItem newItem = new MenuItem(title);
            
            newItem.AddListener(this);
        }

        void ISelectedItem.Selected(MenuItem i_Item)
        {
            /// TODO implement
        }

        public void Show()
        {
            Console.Clear();
            m_MainMenu.Show();
        }

    }
}
