using System;

namespace Dispatcher
{
    public delegate void NameChangeEventHandler(object sender, NameChangeEventArgs args);

    public class Dispatcher
    {
        private string name;
        public event NameChangeEventHandler NameChange;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnNameChange(new NameChangeEventArgs(name));
            }
        }

        protected virtual void OnNameChange(NameChangeEventArgs args)
        {
            NameChange?.Invoke(this, args);
        }
    }

    public class NameChangeEventArgs : EventArgs
    {
        public string NewName { get; private set; }

        public NameChangeEventArgs(string newName)
        {
            NewName = newName;
        }
    }

    public class Handler
    {
        public void OnDispatcherNameChange(object sender, NameChangeEventArgs args)
        {
            Console.WriteLine($"Ім'я диспетчера змінено на {args.NewName}.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dispatcher dispatcher = new Dispatcher();
            Handler handler = new Handler();

            dispatcher.NameChange += handler.OnDispatcherNameChange;

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                dispatcher.Name = input;
            }
        }
    }
}
