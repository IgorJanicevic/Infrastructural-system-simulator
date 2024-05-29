using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using NetworkService.Views;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using Messenger = MVVMLight.Messaging.Messenger;

namespace NetworkService.ViewModel
{
    
    public class NetworkEntitesViewModel : BindableBase
    {

        private ObservableCollection<Entity> _entites;
        public ObservableCollection<Entity> Entites
        {
            get { return _entites; }
            set { 
                
                    _entites = value;
                    OnPropertyChanged(nameof(Entites));
                    OnPropertyChanged(nameof(Entity));
            }
        }
        private ObservableCollection<Entity> _entitesForView;
        public ObservableCollection<Entity> EntitesForView
        {
            get { return _entitesForView; }
            set
            {          
                    _entitesForView = value;
                    OnPropertyChanged(nameof(EntitesForView));        
            }
        }
        public IEnumerable<Types> TypesList
        {
            get
            {
                return (IEnumerable<Types>)Enum.GetValues(typeof(Model.Types));
            }
        }
        public List<string> TypesFilter { get; set; }
        

        private readonly DataIO _serializer= new DataIO();
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

        private int _heightKeyboard = 0;
        public int HeightKeyboard
        {
            get { return (int)_heightKeyboard; }
            set { 
                _heightKeyboard=value;
                OnPropertyChanged(nameof(HeightKeyboard));
            }
        }


        #region def Commands
        public MyICommand AddNewEntity { get; set; }
        public MyICommand UploadImage { get; set; }
        public MyICommand FilterEntites { get; set; }
        public MyICommand<Entity> DeleteEntity { get; set; }
        public MyICommand<object> ShowKeyboardCommand { get; set; }

        #region keyboard

        public bool First=true;

        private string _displayText = string.Empty;
        public string DisplayText
        {
            get { return _displayText; }
            set { _displayText = value; }
        }
        public MyICommand<string> ButtonPressCommand { get; set; }

        private TextBox _selectedTextBox;
        public TextBox SelectedTextBox
        {
            get { return _selectedTextBox; }
            set
            {
                _selectedTextBox=value;
                OnPropertyChanged(nameof(SelectedTextBox));
            }
        }
        private void OnButtonPress(string parameter)
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
                HeightKeyboard = 0;
                DisplayText = "";
                OnPropertyChanged(nameof(HeightKeyboard));
                return;
            }
            else
            {
                DisplayText += parameter;
            }
            if (First)
            {
                NameAddText = DisplayText;
            }
            else
            {
                int temp = 0;
                Int32.TryParse(DisplayText, out temp);
                IdFilterText = temp;
            }
            
        }
        #endregion

        #endregion

        #region main
        public NetworkEntitesViewModel()
        {
        }

        public NetworkEntitesViewModel(ObservableCollection<Entity> entites)
        {
            Messenger.Default.Register<ObservableCollection<Entity>>(this, UpdateValue);
            LoadData(entites);


            LoadCommands();

        }
        #endregion

        private void UpdateValue(ObservableCollection<Entity> temp)
        {
            if (Entites.Count == temp.Count)
            {
                Entites = new ObservableCollection<Entity>(temp);
                EntitesForView = new ObservableCollection<Entity>(FilterUpdate());
                OnPropertyChanged(nameof(EntitesForView));
                OnPropertyChanged(nameof(Entites));
            }
            else
            {
                Entites = new ObservableCollection<Entity>(temp);
                EntitesForView= new ObservableCollection<Entity>(temp);
                OnPropertyChanged(nameof(EntitesForView));
                OnPropertyChanged(nameof(Entites));
            }

        }
        private List<Entity> FilterUpdate()
        {            
            List<Entity> tempCollection= (EntitesForView.Where(x=> Entites.Contains(x)).ToList());
            EntitesForView = new ObservableCollection<Entity>(tempCollection);
            return tempCollection;

        }

        #region Commands Function
        private void Filtering()
        {
            List<Entity> filteredEntities = Entites.ToList();

            if (TypesFilterText == null)
            {
            }
            else
            {

                if (!TypesFilterText.Equals("All"))
                {
                    filteredEntities = Entites.Where(el => el.Type.ToString().Equals(TypesFilterText.ToString())).ToList();
                }
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
                    
                    Messenger.Default.Send<NotificationContent>(NotificationsCollection.CreateNameFaildToastNotification());
                    error=true;
                }
                
                if (ImagePath == null)
                {
                    //Messenger.Default.Send<NotificationContent>(NotificationsCollection.CreateImageFaildToastNotification());
                    if (TypeAddText == Types.Generator)
                    {
                        ImagePath = "C:\\Users\\HomePC\\Documents\\GitHub\\Infrastructural-system-simulator\\NetworkService\\NetworkService\\NetworkService\\Resources\\Images\\generator_default.jpg";
                    }
                    else
                    {
                        ImagePath = "C:\\Users\\HomePC\\Documents\\GitHub\\Infrastructural-system-simulator\\NetworkService\\NetworkService\\NetworkService\\Resources\\Images\\panel_default.jpg";
                    }
                    //error=false;
                }
                if (!error)
                {
                    Entity newEntity = new Entity(NameAddText.ToString(), TypeAddText, ImagePath);
                    Messenger.Default.Send<Tuple<Entity,string>>(new Tuple<Entity,string>(newEntity,"ADD"));
                    NameAddText = null;
                    ImagePath = null;
                    Messenger.Default.Send<NotificationContent>(NotificationsCollection.CreateSuccessToastNotification());
                    //LogData.Log($"Added {newEntity}");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR!");
            }
        }
        private void DeleteFunc(Entity entity)
        {
            if (entity != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want delete this?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Messenger.Default.Send<Tuple<Entity, string>>(new Tuple<Entity, string>(entity, "DEL"));
                    //LogData.Log($"Deleted {entity}");

                }
                Messenger.Default.Send<NotificationContent>(NotificationsCollection.DeleteSuccessToastNotification());
            }
        }
        private void OnShowKeyboard(object selected)
        {
            HeightKeyboard = 190;
            if (selected != null)
            {
                SelectedTextBox= selected as TextBox;
                OnPropertyChanged(nameof(NameAddText));
                if (SelectedTextBox.Name.Equals("NameAddTextBox"))
                {
                    First = true;
                }
                else
                {
                    First= false;
                }
            }
            OnPropertyChanged(nameof(HeightKeyboard));
        }      
        #endregion

        #region Loads
        private void LoadData(ObservableCollection<Entity> entites)
        {
            Entites = entites;
            EntitesForView = entites;

        }
        private void LoadCommands()
        {
            ButtonPressCommand = new MyICommand<string>(OnButtonPress);
            AddNewEntity = new MyICommand(OnAdd);
            UploadImage = new MyICommand(AddImage);
            FilterEntites = new MyICommand(Filtering);
            DeleteEntity = new MyICommand<Entity>(DeleteFunc);
            TypesFilter = new List<string>() { "All", "Panel", "Generator" };
            ShowKeyboardCommand = new MyICommand<object>(OnShowKeyboard);

        }

        #endregion
    }
}
