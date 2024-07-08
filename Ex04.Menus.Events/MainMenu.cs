using System;


namespace Ex04.Menus.Events
{
    public class MainMenu
    {
        private MenuItem m_MainMenu;
        private MenuItem m_CurrentItem;

        public MenuItem MainMenuItem
        {
            get
            {
                return m_MainMenu; 
            }
        } 

        public MainMenu(string i_Title)
        {
            m_MainMenu = new MenuItem(null, i_Title, true);
            m_CurrentItem = m_MainMenu;
        }

        public void AddMenuItem(MenuItem i_ParentMenu, MenuItem i_SubMenuItem)
        {
            i_ParentMenu.AddSubMenuItem(i_SubMenuItem);
            i_SubMenuItem.MenuItemSelectOccurred += MenuItemSelectOccurredHandler;
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

            m_CurrentItem = m_MainMenu;
        }


        public void Show()
        {
            
            while (true)
            {
                DisplayMenuOptions(m_CurrentItem);

                Console.Write("Select an option: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= m_CurrentItem.SubMenuItems.Count)
                {
                    if (choice == 0)
                    {
                        break;
                    }

                    MenuItem selectedMenuItem = m_CurrentItem.SubMenuItems[choice - 1];
                    selectedMenuItem.MenuItemSelected();
                }
                else
                {
                    Console.WriteLine("Invalid choice, please try again.");
                }

            }
        }

        private void DisplayMenuOptions(MenuItem i_Menu)
        {
            Console.Clear();
            Console.WriteLine(i_Menu.Title);
            Console.WriteLine(new string('-', 20));

            for (int i = 0; i < i_Menu.SubMenuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {i_Menu.SubMenuItems[i].Title}");
            }

            Console.WriteLine("0. Back");
        }

    }
}
