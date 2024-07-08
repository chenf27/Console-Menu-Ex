using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : ISelectedItem
    {
        private MenuItem m_MainMenu;
        private MenuItem m_CurrentMenuLevel;

        public MainMenu(MenuItem i_MenuItem)
        {
            m_MainMenu = i_MenuItem;
            m_CurrentMenuLevel = m_MainMenu;
        }

        public void AddMenuItem()
        {
            string title = "";
            bool isMenu = true;
            MenuItem newItem = new MenuItem(null, title, isMenu); //TODO change null as parent!!!!!!
            
            newItem.AddListener(this);
        }

        void ISelectedItem.Selected(MenuItem i_Item)
        {
            if(i_Item.IsMenu)
            {
                i_Item.Show();
            }
            /// TODO implement
        }

        public void Show()
        {
            Console.Clear();
            m_CurrentMenuLevel.Show();
        }
    }
}
