using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private readonly string m_Title;
        private List<ISelectedItem> m_SelectedListeners;
        
        public MenuItem(string i_Title)
        {
            m_Title = i_Title;
        }
        
        public string Title
        { 
            get 
            { 
                return m_Title; 
            }
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
     
        private void notifyAll()
        {
            foreach(ISelectedItem selectedItem in m_SelectedListeners)
            {
                selectedItem.Selected(this);
            }
        }
    }
}
