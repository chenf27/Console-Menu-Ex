using System;
using System.Linq;

namespace Ex04.Menus.Events
{
    public class MainMenu
    {
        private readonly MenuItem r_MainMenu;
        private MenuItem m_CurrentItem;
        private const string k_NotAMenuErrorMessage = "The parent item you inserted is not a menu! you can't create a sub menu for it.";
        private const string k_NotANumberErrorMessage = "Please enter a valid integer!";
        public const bool v_IsMenu = true;

        public MainMenu(string i_Title)
        {
            r_MainMenu = new MenuItem(null, i_Title, v_IsMenu);
            m_CurrentItem = r_MainMenu;
            r_MainMenu.MenuItemSelectOccurred += new MenuItemSelectedEventHandler(MenuItemSelectOccurredHandler);
        }

        public MenuItem MainMenuItem
        {
            get
            {
                return r_MainMenu;
            }
        }

        public void AddMenuItem(MenuItem i_ParentMenu, MenuItem i_SubMenuItem)
        {
            if(i_ParentMenu.IsMenu)
            {
                i_ParentMenu.AddSubMenuItem(i_SubMenuItem);
                i_SubMenuItem.MenuItemSelectOccurred += new MenuItemSelectedEventHandler(MenuItemSelectOccurredHandler);
            }
            else
            {
                throw new ArgumentException(k_NotAMenuErrorMessage, i_ParentMenu.Title);
            }
        }

        private void MenuItemSelectOccurredHandler(object i_Sender, MenuItemSelectedEventArgs i_EventArguments)
        {
            MenuItem menuItem = i_EventArguments.SelectedMenuItem;
            if(!menuItem.IsMenu)
            {
                ExecuteAction(menuItem);
            }
        }

        private void ExecuteAction(MenuItem i_MenuItem)
        {
            if(i_MenuItem.Method != null)
            {
                i_MenuItem.Method.Invoke();
            }
        }

        public void Show() //TOTO fix screen clear
        {
            while(true)
            {
                try
                {
                    Console.Clear();
                    m_CurrentItem.Show();
                    MenuItem menuItemFromUserChoice = handleUserInput(m_CurrentItem);
                    if(menuItemFromUserChoice != null)
                    {
                        if(menuItemFromUserChoice.IsMenu)
                        {
                            m_CurrentItem = menuItemFromUserChoice;
                        }
                        else
                        {
                            m_CurrentItem = menuItemFromUserChoice.ParentNode;
                        }
                        menuItemFromUserChoice.MenuItemSelected();  
                    }
                    else
                    {
                        break;
                    }

                }
                catch (Exception thrownException)
                {
                    Console.WriteLine(thrownException.Message);
                    delayScreenClear();
                }
            }
        }

        private MenuItem handleUserInput(MenuItem i_currentMenuItemLevel)
        {
            MenuItem menuItemFromUserChoice;
            bool userChoiceParsedSuccessfully = int.TryParse(Console.ReadLine(), out int userChoice);
            int numOfElementsInMenu = i_currentMenuItemLevel.GetSubMenuItems().Count;

            if(userChoiceParsedSuccessfully && userChoice >= 0 && userChoice <= numOfElementsInMenu)
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

        private void delayScreenClear()
        {
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
