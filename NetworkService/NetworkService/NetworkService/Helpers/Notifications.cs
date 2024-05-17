using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetworkService.Helpers
{
    public class NotificationsCollection : BindableBase
    {
        public static NotificationContent CreateImageFaildToastNotification()
        {
            var notificationContent = new NotificationContent
            {
                Title = "Faild",
                Message = "Choose a picture!",
                Type = NotificationType.Error,
                TrimType = NotificationTextTrimType.AttachIfMoreRows, // Will show attach button on message
                RowsCount = 2, // Will show 3 rows and trim after              
                CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true)
                Background = new SolidColorBrush(Colors.Red),
                Foreground = new SolidColorBrush(Colors.White),


            };

            return notificationContent;
        }
        public static NotificationContent CreateNameFaildToastNotification()
        {
            var notificationContent = new NotificationContent
            {
                Title = "Faild",
                Message = "Name cannot be empty!",
                Type = NotificationType.Error,
                TrimType = NotificationTextTrimType.AttachIfMoreRows, // Will show attach button on message
                RowsCount = 2, // Will show 3 rows and trim after              
                CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true)
                Background = new SolidColorBrush(Colors.Red),
                Foreground = new SolidColorBrush(Colors.White),


            };

            return notificationContent;
        }
        public static NotificationContent CreateSuccessToastNotification()
        {
            var notificationContent = new NotificationContent
            {
                Title = "Success",
                Message = "Entity successfully added.",
                Type = NotificationType.Success,
                TrimType = NotificationTextTrimType.AttachIfMoreRows, // Will show attach button on message
                RowsCount = 2, // Will show 3 rows and trim after
                //LeftButtonAction = () => SomeAction(), // Action on left button click, button will not show if it null 
                //RightButtonAction = () => SomeAction(), // Action on right button click,  button will not show if it null
                //LeftButtonContent, // Left button content (string or what u want)
                //RightButtonContent, // Right button content (string or what u want)
                CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true)

                Background = new SolidColorBrush(Colors.LimeGreen),
                Foreground = new SolidColorBrush(Colors.White),

                // FontAwesome5 by Codinion NuGet paket ti treba da bi radilo ovo sa ikonicama
                // Icon = new SvgAwesome()
                // {
                //      Icon = EFontAwesomeIcon.Regular_Star,
                //      Height = 25,
                //      Foreground = new SolidColorBrush(Colors.Yellow)
                // },

                // Image = new NotificationImage()
                // {
                //      Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Test image.png")));,
                //      Position = ImagePosition.Top
                // }
            };

            return notificationContent;
        }
        public static NotificationContent DeleteSuccessToastNotification()
        {
            var notificationContent = new NotificationContent
            {
                Title = "Success",
                Message = "Entity successfully deleted.",
                Type = NotificationType.Success,
                TrimType = NotificationTextTrimType.AttachIfMoreRows, // Will show attach button on message
                RowsCount = 2, // Will show 3 rows and trim after
                //LeftButtonAction = () => SomeAction(), // Action on left button click, button will not show if it null 
                //RightButtonAction = () => SomeAction(), // Action on right button click,  button will not show if it null
                //LeftButtonContent, // Left button content (string or what u want)
                //RightButtonContent, // Right button content (string or what u want)
                CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true)

                Background = new SolidColorBrush(Colors.LimeGreen),
                Foreground = new SolidColorBrush(Colors.White),

                // FontAwesome5 by Codinion NuGet paket ti treba da bi radilo ovo sa ikonicama
                // Icon = new SvgAwesome()
                // {
                //      Icon = EFontAwesomeIcon.Regular_Star,
                //      Height = 25,
                //      Foreground = new SolidColorBrush(Colors.Yellow)
                // },

                // Image = new NotificationImage()
                // {
                //      Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Test image.png")));,
                //      Position = ImagePosition.Top
                // }
            };

            return notificationContent;
        }


    }
}
