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
            DropCommand = new MyICommand<Tuple<DragEventArgs, object>>(OnDrop);
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

        public MyICommand<Entity> DragStartCommand { get; }
        public MyICommand<Tuple<DragEventArgs, object>> DropCommand { get; }

        
           

        private void OnDragStart(Entity entity)
        {
            if (entity != null)
            {
                var data = new DataObject(typeof(Entity), entity);
                DragDrop.DoDragDrop(Application.Current.MainWindow, data, DragDropEffects.Move);
            }
        }

        private void OnDrop(Tuple<DragEventArgs, object> parameters)
        {
            var e = parameters.Item1;
            var canvas = parameters.Item2 as Canvas;

            if (e.Data.GetDataPresent(typeof(Entity)))
            {
                var droppedEntity = e.Data.GetData(typeof(Entity)) as Entity;
                // Handle the dropped entity and canvas reference
            }
        }



        #endregion


    }
}
