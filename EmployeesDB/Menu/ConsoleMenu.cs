using System;
using System.Collections.Generic;

namespace EmployeesDB.Menu
{
    public abstract class ConsoleMenu
    {

        private const ConsoleColor CursorColor = ConsoleColor.White;
        private const ConsoleColor SelectedItemColor = ConsoleColor.Black; 

        private IList<MenuItem> menuItems;

        private int currentItem = 0;

        protected bool CloseMenu { get; set; }

        public int CurrentItem 
        {
            get { return currentItem; }
        }

        public ConsoleMenu()
        {
            menuItems = new List<MenuItem>();
            CloseMenu = false;
        }

        public abstract void Initialize();

        protected void AddItem(string name, EventHandler action, EventArgs args = null)
        {
            if (action != null)
            {
                menuItems.Add(new MenuItem(name, action, args));
            }
        }

        protected void ClearMemu()
        {
            menuItems.Clear();
        }

        protected void DrawMenu()
        {
            Console.Clear();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == currentItem)
                {
                    Console.BackgroundColor = CursorColor;
                    Console.ForegroundColor = SelectedItemColor;
                    Console.WriteLine(menuItems[i].Name);

                    Console.BackgroundColor = SelectedItemColor;
                    Console.ForegroundColor = CursorColor;
                } else
                {
                    Console.WriteLine(menuItems[i].Name);
                }                
            }
        }

        protected void ListenKey()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        UpArrowPressed();
                        DrawMenu();
                        break;
                    case ConsoleKey.DownArrow:
                        DownArrowPressed();
                        DrawMenu();
                        break;
                    case ConsoleKey.Enter:
                        EnterPressed();
                        break;
                }
    
                if(CloseMenu) return;
            }
        }


        private void UpArrowPressed()
        {
            if (currentItem > 0)
            {
                currentItem--;
            }    
        }

        private void DownArrowPressed()
        {
            if (currentItem < menuItems.Count - 1)
            {
                currentItem++;
            }
        }

        private void EnterPressed()
        {
            menuItems[currentItem].DoAction(this);
        }

    }
}
