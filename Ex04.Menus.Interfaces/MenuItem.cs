using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private List<ISelectedItem> m_SelectedListeners;
        private readonly MenuItem r_ParentNode;
        private readonly List<MenuItem> r_SubMenuItems;
        private readonly string r_Title;
        private readonly bool r_IsMenu;
        private const bool v_Selected = true;
        private bool m_Selected = !v_Selected;
        
        public MenuItem(MenuItem i_ParentNode, string i_Title, bool i_IsMenu)
        {
            r_ParentNode = i_ParentNode;
            r_Title = i_Title;
            r_IsMenu = i_IsMenu;

            if(i_IsMenu)
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

        public bool IsMenu
        {
            get
            {
                return r_IsMenu;
            }
        }

        internal bool Selected
        {
            get
            {
                return m_Selected;
            }
            set
            {
                m_Selected = value;
                
                if(m_Selected)
                {
                    notifyAllListeners();
                }
            }
        }

        public MenuItem ParentNode
        {
            get
            {
                return r_ParentNode;
            }
        }

        public void AddSubMenuItem(MenuItem i_MenuItem)
        {
            if(IsMenu)
            {
                r_SubMenuItems.Add(i_MenuItem);
            }

            //TODO potential place for ArgumentException if IsMenu == false
        }
        
        public void AddListener(ISelectedItem i_MenuItem)
        {
            if (m_SelectedListeners == null)
            {
                m_SelectedListeners = new List<ISelectedItem>();
            }

            m_SelectedListeners.Add(i_MenuItem);
        }

        public void RemoveListener(ISelectedItem i_MenuItem)
        {
            m_SelectedListeners.Remove(i_MenuItem);
        }
     
        private void notifyAllListeners()
        {
            foreach(ISelectedItem selectedItem in m_SelectedListeners)
            {
                selectedItem.Selected(this);
            }

            Selected = !v_Selected;
        }

        public void Show()
        {
            if (IsMenu)
            {
                Console.WriteLine(Title);
                Console.WriteLine("==============");
                foreach(MenuItem item in r_SubMenuItems)
                {
                    //TODO add number before each title
                    Console.WriteLine(item.Title);

                    //TODO add select choice
                }
            }
        }
    }
}
