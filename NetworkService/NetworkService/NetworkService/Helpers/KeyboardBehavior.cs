using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace NetworkService.Helpers
{
    public class KeyboardBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
            base.OnDetaching();
        }

        private void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                AssociatedObject.RaiseEvent(new System.Windows.RoutedEventArgs(ButtonBase.ClickEvent));
                Debug.WriteLine("Enter key pressed.");
            }
            else
            {
                char character = (char)KeyInterop.VirtualKeyFromKey(e.Key);
                AssociatedObject.Text += character;
                AssociatedObject.CaretIndex = AssociatedObject.Text.Length;
                Debug.WriteLine($"Character '{character}' typed.");
            }
        }
    }
}
