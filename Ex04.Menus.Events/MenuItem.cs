using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Events
{
    public delegate void MenuItemSelectedHandler(object sender, SelectEventArgs e);

    public class SelectEventArgs : EventArgs
    {
        private string m_Title;
        private List<MenuItem> m_SubMenuItems;

        public string Title { get { return m_Title; } }

        public List<MenuItem> SubMenuItems { get { return m_SubMenuItems; } }

        public SelectEventArgs(string i_Title, List<MenuItem> i_SubMenuItems)
        {
            m_Title = i_Title;
            m_SubMenuItems = i_SubMenuItems;
        }
    }

    public class MenuItem
    {
        private string m_Title;
        public event MenuItemSelectedHandler MenuItemSelectOccurred;
        private List<MenuItem> m_SubMenuItems;


        public string Title
        {
            get 
            {
                return m_Title;
            }
            set
            {
                m_Title = value; 
            }
        }

        public void Selected()
        {
            SelectEventArgs e = new SelectEventArgs();
            OnMenuItemSelectedOccurred(e);
        }

        protected virtual void OnMenuItemSelectedOccurred(SelectEventArgs e)
        {
            if (MenuItemSelectOccurred != null)
            {
                MenuItemSelectOccurred.Invoke(this, e);
            }
        }


    }
}
