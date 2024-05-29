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

        private ObservableCollection<EnitityInCanvas> _entitesInCanvas = new ObservableCollection<EnitityInCanvas>() { null, null, null, null, null, null, null, null, null, null, null, null };
        public ObservableCollection<EnitityInCanvas> EntitesInCanvas
        {
            get { return _entitesInCanvas; }
            set
            {
                _entitesInCanvas = value;
                OnPropertyChanged(nameof(EntitesInCanvas));
                OnPropertyChanged(nameof(Entity));
            }
        }

        public NetworkDisplayViewModel()
        {
        }

        public NetworkDisplayViewModel(ObservableCollection<Entity> entites)
        {
            DragStartCommand = new MyICommand<Entity>(OnDragStart);
            DropCommand = new MyICommand<object>(OnDrop);
            DeleteCommand = new MyICommand<object>(DeleteFromCanvas);
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

            foreach(EnitityInCanvas item in EntitesInCanvas)
            {
                if (item != null) {
                    foreach (Entity enNew in entities)
                    {
                        if (item.Entity.Id == enNew.Id)
                        {
                            MessageBox.Show(enNew.LastMeasure.ToString());
                            OnPropertyChanged(nameof(EntitesInCanvas));
                            OnPropertyChanged(nameof(Entity));
                            item.Entity.Update(enNew.LastMeasure);
                            break;
                        }
                    }
                }
            }
            
           
            
        }

        #region DragAndDrop

        public MyICommand<Entity> DragStartCommand { get; set; }
        public MyICommand<object> DropCommand { get; set; }

        public MyICommand<object> DeleteCommand { get; set; }




        public void DeleteFromCanvas(object canvas)
        {
            Entity EntityForBack=null;
            Canvas canvas1= canvas as Canvas;
            int id = GetCanvasId(canvas1);
            if (EntitesInCanvas[id - 1] != null)
            {
                EntityForBack= EntitesInCanvas[id - 1].Entity;
                EntitesInCanvas.RemoveAt(id - 1);
                AddInTreeView(EntityForBack);
                OnPropertyChanged(nameof(EntitesInCanvas));
            }        
           
        }

        private void AddInTreeView(Entity entity)
        {
            foreach(EntitesWithType el in EntitesWithTypesCollection)
            {
                if (el.Types == entity.Type)
                {
                    el.Entites.Add(entity);
                    MessageBox.Show("Rad");
                }
            }
        }
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

        
        
        public void OnDrop(object args)
        {
            Canvas canvas = args as Canvas;

            if (CanvasTaken(canvas))
            {
                MessageBox.Show("Uspesno dodavanje");
                OnPropertyChanged(nameof(EntitesInCanvas));
                Entity forDelete = null;
                EntitesWithType type = null;
                foreach(EntitesWithType TYP in EntitesWithTypesCollection)
                {
                    foreach(Entity en in TYP.Entites)
                    {
                        if (en.Id == SelectedEntity.Id)
                        {
                            forDelete = en;
                            type = TYP;
                            break;
                        }
                    }

                }
                type.Entites.Remove(forDelete);
                
            }

                              
        }

        #region Temp Funcction For Converting
        private int GetCanvasId(Canvas canvas)
        {
            string name = canvas.Name;
            MessageBox.Show(name);
            string br = name.Remove(0, 6);
            return int.Parse(br);
        }
        private void AddOnCanvas(Canvas canvas)
        {
            string name = canvas.Name;
            switch (name)
            {
                case "Canvas1": EntitesInCanvas.Insert(0, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas2": EntitesInCanvas.Insert(1, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas3": EntitesInCanvas.Insert(2, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas4": EntitesInCanvas.Insert(3, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas5": EntitesInCanvas.Insert(4, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas6": EntitesInCanvas.Insert(5, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas7": EntitesInCanvas.Insert(6, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas8": EntitesInCanvas.Insert(7, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas9": EntitesInCanvas.Insert(8, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas10": EntitesInCanvas.Insert(9, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas11": EntitesInCanvas.Insert(10, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                case "Canvas12": EntitesInCanvas.Insert(11, new EnitityInCanvas(SelectedEntity, canvas, true));break;
                default: break;
                
            }
        }
        private bool CanvasTaken(Canvas canvas)
        {
            int idCanvas= GetCanvasId(canvas);
            if (EntitesInCanvas[idCanvas-1]== null)
            {
                
                AddOnCanvas(canvas);
                return true;
            }
            return false;
        }
        #endregion


        #endregion


    }
}
