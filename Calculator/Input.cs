using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Input : INotifyPropertyChanged
    {
        private string input;
        public string Value
        {
            get { return input; }
            set 
            {
                input = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
