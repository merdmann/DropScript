using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace UIDropScript.Helper
{
   
    public class Identifier : INotifyPropertyChanged
    {
        String value;
        String name;

        /*
         * The default constructor
         */
        public Identifier(String name)
        {
            this.name = name;
        }

        public String Value
        {
            get {
                Console.WriteLine("{0}={1}", this.name, this.value);
                return this.value; 
            }
            set
            {
                this.value = value;
                NotifyPropertyChanged(this.name);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
