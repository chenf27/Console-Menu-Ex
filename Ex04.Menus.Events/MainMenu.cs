using System;


namespace Ex04.Menus.Events
{
    public class MainMenu
    {
        private MenuItem m_MainMenu;
        private MenuItem m_CurrentItem;
        private const string k_NotAMenuErrorMessage = "The parent item you inserted is not a menu! you can't create a sub menu for it.";
        private const string k_NotANumberErrorMessage = "Please enter a valid integer!";
        public const bool v_IsMenu = true;

        public MainMenu(string i_Title)
        {
            m_MainMenu = new MenuItem(null, i_Title, v_IsMenu);
            m_CurrentItem = m_MainMenu;
            m_MainMenu.MenuItemSelectOccurred += new MenuItemSelectedEventHandler(MenuItemSelectOccurredHandler);
        }

        public MenuItem MainMenuItem
        {
            get
            {
                return m_MainMenu;
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

        private void MenuItemSelectOccurredHandler(object sender, MenuItemSelectedEventArgs e)
        {
            m_CurrentItem = e.SelectedMenuItem;
            if (!m_CurrentItem.IsMenu)
            {
                ExecuteAction(m_CurrentItem);
            }
        }

        private void ExecuteAction(MenuItem i_MenuItem)
        {
            if (i_MenuItem.Method != null)
            {
                i_MenuItem.Method.Invoke();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Executing action for {i_MenuItem.Title}...");
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
            }
        }

        public void Show()
        {
            //TODO WHILE TRUE ASK
            while (true)
            {
                m_CurrentItem.Show();
                Console.Write("Select an option: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= m_CurrentItem.SubMenuItems.Count)
                {
                    if (choice == 0)
                    {
                        if (m_CurrentItem.ParentNode != null)
                        {
                            m_CurrentItem = m_CurrentItem.ParentNode;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        MenuItem selectedMenuItem = m_CurrentItem.SubMenuItems[choice - 1];
                        selectedMenuItem.MenuItemSelected();
                        if(selectedMenuItem.IsMenu)
                        {
                            m_CurrentItem = selectedMenuItem;
                        }
                        else
                        {
                            m_CurrentItem = m_CurrentItem.Parent;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice, please try again.");
                }
            }
        }
    }
}
