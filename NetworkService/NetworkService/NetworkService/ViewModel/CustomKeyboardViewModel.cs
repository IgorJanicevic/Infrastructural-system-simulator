using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NetworkService.ViewModel
{
    public class CustomKeyboardViewModel : BindableBase
    {
        private string _displayText = string.Empty;

        public string DisplayText
        {
            get { return _displayText; }
            set { SetProperty(ref _displayText, value); }
        }

        public TextBox SelectedTextBox;

        public MyICommand<string> ButtonPressCommand { get; set; }

        public CustomKeyboardViewModel()
        {
            ButtonPressCommand = new MyICommand<string>(OnButtonPress);
            
        }   

        public void GetTextBox(TextBox selected)
        {
            SelectedTextBox = selected;
        }
        
        private void OnButtonPress(object parameter)
        {
            if (parameter == null)
                return;

            if (parameter.Equals("DEL"))
            {
                if (!string.IsNullOrEmpty(DisplayText))
                    DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
            }
            else if (parameter.Equals("ENTER"))
            {
                // Perform action on Enter, if needed
            }
            else
            {
                DisplayText += parameter;
            }

            Console.WriteLine(DisplayText);
            MessageBox.Show(DisplayText);
        }
    }
}
