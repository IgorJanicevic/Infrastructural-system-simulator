using Microsoft.Win32;
using MVVM3.Helpers;
using MVVMLight.Messaging;
using NetworkService.Model;
using Notification.Wpf;
using Projekat.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace NetworkService.ViewModel
{
    
    public class NetworkEntitesViewModel : BindableBase
    {

        public ObservableCollection<Entity> Entites {  get; set; }
        public ObservableCollection<Entity> EntitesForView {  get; set; }
        public IEnumerable<Types> TypesList
        {
            get
            {
                return (IEnumerable<Types>)Enum.GetValues(typeof(Model.Types));
            }
        }
        public List<string> TypesFilter { get; set; }
        

        private DataIO _serializer= new DataIO();
        private Entity _selectedEntity= new Entity();
        private string _nameAddText;
        private Types _typeAddText;
        private string _imagePath;

        private string _typeFilterText;
        private int _idFilterText;
        private string _selectedFilter;
        private bool _equalsChecked;
        public bool EqualsChecked
        {
            get { return _equalsChecked; }
            set
            {
                if (_equalsChecked != value)
                {
                    _equalsChecked = value;
                    OnPropertyChanged(nameof(EqualsChecked));
                }
            }
        }
        private bool _ltChecked;
        public bool LtChecked
        {
            get { return _ltChecked; }
            set
            {
                if (_ltChecked != value)
                {
                    _ltChecked = value;
                    OnPropertyChanged(nameof(LtChecked));
                }
            }
        }
        private bool _gtChecked;
        public bool GtChecked
        {
            get { return _gtChecked; }
            set
            {
                if (_gtChecked != value)
                {
                    _gtChecked = value;
                    OnPropertyChanged(nameof(GtChecked));
                }
            }
        }
        public int IdFilterText
        {
            get { return _idFilterText; }
            set
            {
                if (_idFilterText != value)
                {
                    _idFilterText = value;
                    OnPropertyChanged(nameof(IdFilterText));
                }
            }
        }
        public string SelectedFilter
        {
            get { return _selectedFilter; }
            set
            {
                if (_selectedFilter != value)
                {
                    _selectedFilter = value;
                    OnPropertyChanged(nameof(SelectedFilter));
                }
            }
        }
        public string TypesFilterText
        {
            get { return _typeFilterText; } set
            {
                if (_typeFilterText != value)
                {
                    _typeFilterText = value;
                    OnPropertyChanged(nameof(TypesFilterText));
                }
            }
        }
        public string NameAddText
        {
            get
            {
                return _nameAddText;
            }
            set
            {
                if (_nameAddText != value)
                {
                    _nameAddText = value;
                    OnPropertyChanged(nameof(NameAddText));
                }
            }
        }
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                }
            }
        }
        public Types TypeAddText
        {
            get { return _typeAddText; }
            set
            {
                if( _typeAddText != value)
                {
                    _typeAddText = value;
                    OnPropertyChanged(nameof(TypeAddText));
                }
            }
        }      
        public Entity SelectedEntity
        {
            get
            {
                return _selectedEntity;
            }
            set
            {
                if( _selectedEntity != value )
                {
                    _selectedEntity = value;
                    OnPropertyChanged(nameof(SelectedEntity));
                    //DeleteCommand.RaiseCanExecuteChanged();

                }
            }
        }


        public MyICommand AddNewEntity { get; set; }
        public MyICommand UploadImage { get; set; }
        public MyICommand FilterEntites { get; set; }
        public MyICommand<Entity> DeleteEntity { get; set; }

        public NetworkEntitesViewModel()
        {
            LoadData();
            AddNewEntity = new MyICommand(OnAdd);
            UploadImage = new MyICommand(AddImage);
            FilterEntites = new MyICommand(Filtering);
            DeleteEntity = new MyICommand<Entity>(DeleteFunc);
            TypesFilter = new List<string>() { "All","Panel","Generator" };
            
        }

        private void DeleteFunc(Entity entity)
        {
            if (entity != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want delete this?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Entites.Remove(entity);
                    MessageBox.Show("Successfully deleted entity", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                    SaveData(Entites);
                }
            }
        }
        private void Filtering()
        {
            List<Entity> filteredEntities = Entites.ToList();
            
            
            if (!TypesFilterText.Equals("All"))
            {
                filteredEntities = Entites.Where(el => el.Type.ToString().Equals(TypesFilterText.ToString())).ToList();
            }

            if (IdFilterText != 0)
            {
                if (EqualsChecked == LtChecked == GtChecked == false)
                {
                    MessageBox.Show("The data cannot be filtered by ID\nbecause you have not selected any radio buttons\r\n", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (EqualsChecked)
                    filteredEntities = filteredEntities.Where(x => x.Id == IdFilterText).ToList();
                else if (LtChecked)
                    filteredEntities = filteredEntities.Where(x => x.Id < IdFilterText).ToList();
                else if (GtChecked)
                    filteredEntities = filteredEntities.Where(x => x.Id > IdFilterText).ToList();
            }

            EntitesForView = new ObservableCollection<Entity>(filteredEntities);
            OnPropertyChanged(nameof(EntitesForView));

            TypesFilterText = "All";
            LtChecked = false;
            GtChecked = false;
            EqualsChecked = false;
            IdFilterText = 0;

        }
        private void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Profile Image";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.InitialDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\Images"));

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                ImagePath = openFileDialog.FileName;
            }
        }
        private void OnAdd()
        {
            try
            {
                bool error = false;
                if(NameAddText==null)
                {
                    MessageBox.Show("Name field cannot be empty!", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                    error=true;
                }
                
                if (ImagePath == null)
                {
                    MessageBox.Show("Choose a picture!", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                    error=true;
                }
                if (!error)
                {
                    Entity newEntity = new Entity(NameAddText.ToString(), TypeAddText, ImagePath);
                    Entites.Add(newEntity);
                    Messenger.Default.Send<Entity>(newEntity);
                    SaveData(Entites);
                    Messenger.Default.Send<NotificationContent>(CreateSuccessToastNotification());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR!");
            }
        }

        private NotificationContent CreateSuccessToastNotification()
        {
            var notificationContent = new NotificationContent
            {
                Title = "Success",
                Message = "Note successfully added.",
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
        private void LoadData()
        {              
            Entites = _serializer.DeSerializeObject<ObservableCollection<Entity>>("Entites.xml");
            EntitesForView=Entites;
        }
        private void SaveData(ObservableCollection<Entity> entities)
        {
            _serializer.SerializeObject<ObservableCollection<Entity>>(entities, "Entites.xml");
        }
    }
}
