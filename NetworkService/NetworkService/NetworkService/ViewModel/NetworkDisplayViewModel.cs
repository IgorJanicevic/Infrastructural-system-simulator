using MVVM3.Helpers;
using MVVMLight.Messaging;
using NetworkService.Model;
using Projekat.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

       
    }
}
