using System;

namespace EmployeesDB.Menu
{
    class MenuItem
    {
        private EventArgs eventArgs;
        
        public string Name { get; set; }

        public event EventHandler OnChoise;

        public MenuItem(string name, EventHandler action, EventArgs args)
        {
            Name = name;
            OnChoise += action;
            eventArgs = args;
        }

        public void DoAction(object sender)
        {
            if (OnChoise != null)
            {
                OnChoise(sender, eventArgs);
            }
        }
    }
}
