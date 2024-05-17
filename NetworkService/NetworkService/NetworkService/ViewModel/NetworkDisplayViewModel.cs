using NetworkService.Helpers;
using MVVMLight.Messaging;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NetworkService.ViewModel
{
    public class NetworkDisplayViewModel : BindableBase
    {
        private bool isDragging = false;
        private Entity draggedItem = null;
        private int draggedItemIndex = -1;
        public IEnumerable<Types> Types
        {
            get
            {
                return (IEnumerable<Types>)Enum.GetValues(typeof(Model.Types));
            }
        }
        private DataIO serializer= new DataIO(); 
        
        private Entity _selectedEntity;
        public Entity SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                if (value != _selectedEntity)
                {
                    _selectedEntity = value;
                    OnPropertyChanged(nameof(SelectedEntity));
                }
            }
        }

        private ObservableCollection<EntitesWithType> _entitesWithTypesCollection;
        public ObservableCollection<EntitesWithType> EntitesWithTypesCollection
        {
            get { return _entitesWithTypesCollection; }
            set
            {
                _entitesWithTypesCollection = value;
                OnPropertyChanged(nameof(EntitesWithTypesCollection));
            }
        }

        public NetworkDisplayViewModel()
        {
        }

        public NetworkDisplayViewModel(ObservableCollection<Entity> entites)
        {
            DragStartCommand = new MyICommand<Entity>(OnDragStart);
            DropCommand = new MyICommand<Tuple<DragEventArgs, Canvas>>(OnDrop);
            Messenger.Default.Register<ObservableCollection<Entity>>(this, UpdateValue);
            LoadData(entites);
        }

        private void UpdateValue(ObservableCollection<Entity> temp)
        {
            LoadData(temp);
        }

        private void LoadData(ObservableCollection<Entity> _entites)
        {
            ObservableCollection<Entity> entities = new ObservableCollection<Entity>(_entites);
            EntitesWithTypesCollection = new ObservableCollection<EntitesWithType>();
            foreach(var type in Types)
            {
                EntitesWithType entity = new EntitesWithType();
                entity.Types = type;
                var  tempEntity =entities.Where(x => x.Type.ToString().Equals(type.ToString())).ToList();
                entity.Entites = new ObservableCollection<Entity>(tempEntity);
                EntitesWithTypesCollection.Add(entity);
            }
           
            
        }

        #region DragAndDrop

        public MyICommand<Entity> DragStartCommand { get; set; }
        public MyICommand<Tuple<DragEventArgs, Canvas>> DropCommand { get; set; }




        public void OnDragStart(Entity entity)
        {
            if (entity != null)
            {
                SelectedEntity= entity;
                var data = new DataObject(typeof(Entity), entity);
                DragDrop.DoDragDrop(Application.Current.MainWindow, data, DragDropEffects.Move);
                MessageBox.Show(SelectedEntity.ToString());
            }
       }


        public void OnDrop(Tuple<DragEventArgs, Canvas> args)
        {
            if (args == null) return;

            var e = args.Item1;
            var canvas = args.Item2;

            if (e.Data.GetDataPresent(typeof(Entity)))
            {
                var droppedEntity = e.Data.GetData(typeof(Entity)) as Entity;
                if (droppedEntity != null && canvas != null)
                {
                    var image = new Image
                    {
                        Source = new BitmapImage(new Uri(droppedEntity.ImagePath, UriKind.RelativeOrAbsolute)),
                        Width = canvas.ActualWidth,
                        Height = canvas.ActualHeight
                    };
                    canvas.Children.Clear();
                    canvas.Children.Add(image);
                }
            }
        }



            #endregion


        }
}
