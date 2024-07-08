using System;
using System.Linq;
using System.Threading;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : ISelectedItem
    {
        private MenuItem m_MainMenu;
        private MenuItem m_CurrentMenuLevel;
        public const bool v_IsMenu = true;

        public MainMenu(string i_Title)
        {
            m_MainMenu = new MenuItem(null, i_Title, v_IsMenu);
            m_CurrentMenuLevel = m_MainMenu;
        }

        public MenuItem MenuItem
        {
            get
            {
                return m_MainMenu;
            }
        }

        public void AddMenuItem(MenuItem i_ParentMenuItem, MenuItem i_NewMenuItem)
        {
            if (i_ParentMenuItem.IsMenu)
            {
                i_ParentMenuItem.AddSubMenuItem(i_NewMenuItem);
                i_NewMenuItem.AddListener(this);
            }
        }

        void ISelectedItem.Selected(MenuItem i_Item)
        {
            if(i_Item.IsMenu)
            {
                m_CurrentMenuLevel = i_Item;
            }
            else
            {
                i_Item.Execute();
            }
        }

        public void Show() //TODO add Console.Clear() somehow
        {
            while(true)
            {
                try
                {
                    m_CurrentMenuLevel.Show();
                    MenuItem menuItemFromUserChoice = handleUserInput(m_CurrentMenuLevel);

                    if (menuItemFromUserChoice != null)
                    {
                        if(menuItemFromUserChoice.IsMenu)
                        {
                            m_CurrentMenuLevel = menuItemFromUserChoice;
                        }
                        else
                        {
                            menuItemFromUserChoice.Execute();
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private MenuItem handleUserInput(MenuItem i_currentMenuItemLevel)
        {
            MenuItem menuItemFromUserChoice;
            bool userChoiceParsedSuccessfully = int.TryParse(Console.ReadLine(), out int userChoice);
            int numOfElementsInMenu = i_currentMenuItemLevel.GetSubMenuItems().Count;

            if (userChoiceParsedSuccessfully && userChoice >= 0 && userChoice <= numOfElementsInMenu)
            {
                if(userChoice == 0)
                {
                    menuItemFromUserChoice = i_currentMenuItemLevel.ParentNode;
                }
                else
                {
                    menuItemFromUserChoice = i_currentMenuItemLevel.GetSubMenuItems().ElementAt(userChoice - 1);
                }
                
                return menuItemFromUserChoice;
            }
            else
            {
                throw new ArgumentException("Please enter a valid choice!");
            }
            
        }
    }
}
