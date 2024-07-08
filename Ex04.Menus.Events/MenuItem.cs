using System;
using System.Collections.Generic;


namespace Ex04.Menus.Events
{
    
    public class MenuItemSelectedEventArgs : EventArgs
    {
        public MenuItem SelectedMenuItem { get; }

        public MenuItemSelectedEventArgs(MenuItem i_SelectedMenuItem)
        {
            SelectedMenuItem = i_SelectedMenuItem;
        }
    }

    public delegate void MenuItemSelectedEventHandler(object sender, MenuItemSelectedEventArgs e);

    public class MenuItem
    {
        private readonly string r_Title;
        private readonly MenuItem r_ParentNode;
        private readonly bool r_IsMenu;
        private readonly List<MenuItem> r_SubMenuItems;
        private Action m_Method;

        public event MenuItemSelectedEventHandler MenuItemSelectOccurred;

        public MenuItem(MenuItem i_ParentNode, string i_Title, bool i_IsMenu)
        {
            r_ParentNode = i_ParentNode;
            r_Title = i_Title;
            r_IsMenu = i_IsMenu;

            if (i_IsMenu)
            {
                r_SubMenuItems = new List<MenuItem>();
            }
        }

        public string Title
        {
            get
            {
                return r_Title;
            }
        }

        public List<MenuItem> SubMenuItems
        {
            get
            {
                //ERROR!
                if (r_SubMenuItems == null)
                {
                    return new List<MenuItem>();
                }
                else
                {
                    return r_SubMenuItems;
                }
            }
        }

        public Action Method
        {
            get
            {
                return m_Method;
            }
        }

        public bool IsMenu
        {
            get
            {
                return r_IsMenu;
            }
        }

        public MenuItem ParentNode
        {
            get
            {
                return r_ParentNode;
            }
        }
    
        //TODO IN SET
        public void SetAction(Action i_Method)
        {
            m_Method = i_Method;
        }

        public void AddSubMenuItem(MenuItem i_SubMenuItem)
        {
            //TODO EXEPTION HANDLE
            if (r_IsMenu)
            {
                r_SubMenuItems.Add(i_SubMenuItem);
            }
            else
            {
                throw new InvalidOperationException("Cannot add sub-menu items to a non-menu item.");
            }
        }

        public void MenuItemSelected()
        {
            OnMenuItemSelectOccurred(new MenuItemSelectedEventArgs(this));
        }

        protected virtual void OnMenuItemSelectOccurred(MenuItemSelectedEventArgs i_EventArguments)
        {
            if (MenuItemSelectOccurred != null)
            {
                MenuItemSelectOccurred.Invoke(this, i_EventArguments);
            }
        }

    }

}
