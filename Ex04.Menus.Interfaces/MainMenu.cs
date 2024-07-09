using System;
using System.Linq;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : ISelectedItem
    {
        private const string k_NotAMenuErrorMessage = "The parent item you inserted is not a menu! you can't create a sub menu for it.";
        private const string k_NotANumberErrorMessage = "Please enter a valid integer!";
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
            else
            {
                throw new ArgumentException(k_NotAMenuErrorMessage, i_ParentMenuItem.Title);
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
                catch (Exception thrownException)
                {
                    Console.WriteLine(thrownException.Message);
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
                if(!userChoiceParsedSuccessfully)
                {
                    throw new FormatException(k_NotANumberErrorMessage);
                }
                else
                {
                    throw new ArgumentException(string.Format("Your choice must be between 0 and {0}!", numOfElementsInMenu));
                }
            }
        }
    }
}
