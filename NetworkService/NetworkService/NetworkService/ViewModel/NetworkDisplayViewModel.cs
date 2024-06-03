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
using System.Windows.Shapes;
using System.Windows.Media;
using Notification.Wpf;

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

        private  ObservableCollection<EnitityInCanvas> _entitesInCanvas = new ObservableCollection<EnitityInCanvas>() { null, null, null, null, null, null, null, null, null, null, null, null };
        public  ObservableCollection<EnitityInCanvas> EntitesInCanvas
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
            LoadCommands();
            Messenger.Default.Register<ObservableCollection<Entity>>(this, UpdateValue);
            Messenger.Default.Register<Tuple<int, int>>(this,UpdateOneEntity);

            LoadData(entites);
            UpdateFromMain();


        }

        private void UpdateFromMain()
        {
            LinesForCanvasFirst = MainWindowViewModel.LinesForCanvasFirst;
            EntitesInCanvas = MainWindowViewModel.EntitesInCanvas;
            OnPropertyChanged(nameof(LinesCanvas));
            
        }


        private void UpdateValue(ObservableCollection<Entity> temp)
        {
            LoadData(temp);
        }

        private void UpdateOneEntity(Tuple<int,int> retVal)
        {
            foreach(EnitityInCanvas enInCanvas in EntitesInCanvas)
            {
                if (enInCanvas != null)
                {
                    if (enInCanvas.Entity.Id == retVal.Item1)
                    {
                        enInCanvas.Entity.Update(retVal.Item2);
                    }
                }
            }
            MainWindowViewModel.EntitesInCanvas = EntitesInCanvas;
        }


        #region Drawing
        private List<Tuple<int,int, Line>> LinesForCanvasFirst = new List<Tuple<int,int, Line>>();

        private Canvas _linesCanvas;
        public Canvas LinesCanvas
        {
            get { return _linesCanvas; }
            set
            {
                _linesCanvas = value;
                OnPropertyChanged(nameof(LinesCanvas));
            }
        }

        private EnitityInCanvas _firstEntity;
        private EnitityInCanvas _secondEntity;
        private void OnEntityRightClick(object obj)
        {
            var entityForLine = obj as EnitityInCanvas;

            if (entityForLine != null)
            {
                if (_firstEntity == null)
                {
                    _firstEntity = entityForLine;

                }
                else
                {
                    _secondEntity = entityForLine;

                    if (_firstEntity != null && _secondEntity != null)
                    {
                        CreateLine(_firstEntity, _secondEntity);
                        _firstEntity = null;
                        _secondEntity = null;
                    }
                }
            }
        }

        private void CreateLine(EnitityInCanvas firstEntity, EnitityInCanvas secondEntity)
        {
            if (firstEntity != null && secondEntity != null && LinesCanvas != null)
            {
                Line line = new Line
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 2
                };

                if (firstEntity.Entity.Id == secondEntity.Entity.Id) return;
                foreach (var obj in LinesForCanvasFirst)
                {
                    if((obj.Item1== firstEntity.Entity.Id && obj.Item2== secondEntity.Entity.Id) || 
                       (obj.Item1==secondEntity.Entity.Id && obj.Item2 == firstEntity.Entity.Id))
                    {
                        return;
                    }
                }

                // Get the coordinates of the first and second Canvas elements
                Point firstEntityPosition = GetEntityPosition(firstEntity.Canvas.Name);
                Point secondEntityPosition = GetEntityPosition(secondEntity.Canvas.Name);

                line.X1 = firstEntityPosition.X;
                line.Y1 = firstEntityPosition.Y;
                line.X2 = secondEntityPosition.X;
                line.Y2 = secondEntityPosition.Y;

                LinesCanvas.Children.Add(line);
                LinesForCanvasFirst.Add(new Tuple<int, int, Line>(firstEntity.Entity.Id,secondEntity.Entity.Id,line));
                MainWindowViewModel.LinesForCanvasFirst= LinesForCanvasFirst;


            }
        }

        private Point GetEntityPosition(string name)
        {
            double x = 0;
            double y = 0;

            switch (name)
            {
                case "Canvas1": x = 85; y = 70; break;
                case "Canvas2": x = 130; y = 70; break;
                case "Canvas3": x = 230; y = 80; break;
                case "Canvas4": x = 85; y = 140; break;
                case "Canvas5": x = 130; y = 140; break;
                case "Canvas6": x = 230; y = 140; break;
                case "Canvas7": x = 90; y = 262; break;
                case "Canvas8": x = 135; y = 262; break;
                case "Canvas9": x = 235; y = 262; break;
                case "Canvas10": x = 85; y = 400; break;
                case "Canvas11": x = 130; y = 400; break;
                case "Canvas12": x = 235; y = 408; break;
            }

            return new Point(x, y);
        }

        #endregion


        private void LoadCommands()
        {
            DragStartCommand = new MyICommand<Entity>(OnDragStart);
            DropCommand = new MyICommand<object>(OnDrop);
            DeleteCommand = new MyICommand<object>(DeleteFromCanvas);
            CreateLineCommand = new MyICommand<object>(OnEntityRightClick);
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

            EntitesInCanvas = MainWindowViewModel.EntitesInCanvas;
          
            foreach(EnitityInCanvas enCanvas in EntitesInCanvas)
            {
                if(enCanvas != null) {
                foreach (EntitesWithType eTemp in EntitesWithTypesCollection)
                {
                    if (eTemp.Entites.Contains(enCanvas.Entity))
                    {
                        eTemp.Entites.Remove(enCanvas.Entity);
                    }
                }
                }
            }
        }

        #region DragAndDrop

        public MyICommand<Entity> DragStartCommand { get; set; }
        public MyICommand<object> DropCommand { get; set; }

        public MyICommand<object> DeleteCommand { get; set; }
        public MyICommand<object> CreateLineCommand {  get; set; }



        public void DeleteFromCanvas(object canvas)
        {
            Entity EntityForBack=null;
            Canvas canvas1= canvas as Canvas;
            int id = GetCanvasId(canvas1);
            if (EntitesInCanvas[id - 1] != null)
            {
                EntityForBack = EntitesInCanvas[id - 1].Entity;
                int idEntity = EntityForBack.Id;
                EntitesInCanvas[id - 1] = null;
                AddInTreeView(EntityForBack);
                OnPropertyChanged(nameof(EntitesInCanvas));
                List<Tuple<int, int, Line>> forDel = new List<Tuple<int, int, Line>>();
                foreach (var obj in LinesForCanvasFirst)
                {

                    if (obj.Item1 == idEntity || obj.Item2==idEntity)
                    {
                        LinesCanvas.Children.Remove(obj.Item3);
                        forDel.Add(obj);
                    }
                }
                //LinesForCanvasFirst.Where(el => el.Item1 != idEntity && el.Item2 != idEntity);
                foreach (var obj in forDel)
                    LinesForCanvasFirst.Remove(obj);
            }        
           MainWindowViewModel.EntitesInCanvas= EntitesInCanvas;
           MainWindowViewModel.LinesForCanvasFirst= LinesForCanvasFirst;
        }
        private void AddInTreeView(Entity entity)
        {
            foreach(EntitesWithType el in EntitesWithTypesCollection)
            {
                if (el.Types == entity.Type)
                {
                    el.Entites.Add(entity);
                }
            }
        }

        private EnitityInCanvas canvasForUnsuccessDelete = null;
        private int indexForDelete = -1;
        public void OnDragStart(Entity entity)
        {
            if (entity != null)
            {
                SelectedEntity= entity;
                var data = new DataObject(typeof(Entity), entity);
                indexForDelete = -1;
                canvasForUnsuccessDelete = null;
                foreach (EnitityInCanvas enInCanvas in EntitesInCanvas)
                {
                    indexForDelete++;
                    if (enInCanvas == null) continue;
                    if (enInCanvas.Entity.Id == entity.Id)
                    {
                        canvasForUnsuccessDelete= enInCanvas;
                        break;
                    }
                }
                if (indexForDelete != -1)
                {
                    EntitesInCanvas[indexForDelete] = null;
                }

                DragDrop.DoDragDrop(Application.Current.MainWindow, data, DragDropEffects.Move);


            }
       }        
        public void OnDrop(object args)
        {
            Canvas canvas = args as Canvas;

            if (CanvasTaken(canvas))
            {
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
                if (type != null)
                {
                    if (type.Entites.Contains(forDelete))
                        type.Entites.Remove(forDelete);
                    
                }
                MoveLines(SelectedEntity.Id, canvas.Name);


                OnPropertyChanged(nameof(EntitesInCanvas));
                OnPropertyChanged(nameof(Entity));
            }
            else
            {
                EntitesInCanvas[indexForDelete] = canvasForUnsuccessDelete;
            }
                                
        }

        #region Temp Funcction For Lines and Canvas
        private void MoveLines(int idEntity, string canvasName)
        {
            List<Tuple<int, int, Line>> tempListAdd = new List<Tuple<int, int, Line>>();
            List<Tuple<int,int,Line>> tempListDelete= new List<Tuple<int,int,Line>>();
            foreach (var obj in LinesForCanvasFirst)
            {
                if (obj.Item1 == idEntity || obj.Item2== idEntity)
                {
                    LinesCanvas.Children.Remove(obj.Item3);

                    Point newEntityPosition = GetEntityPosition(canvasName);
                    Line line = new Line
                    {
                        Stroke = Brushes.Blue,
                        StrokeThickness = 2
                    };

                    if (obj.Item1 == idEntity)
                    {
                        line.X1 = newEntityPosition.X;
                        line.Y1 = newEntityPosition.Y;
                        line.X2 = obj.Item3.X2;
                        line.Y2 = obj.Item3.Y2;
                    }
                    else
                    {
                        line.X1 = obj.Item3.X1;
                        line.Y1 = obj.Item3.Y1;
                        line.X2 = newEntityPosition.X;
                        line.Y2 = newEntityPosition.Y;
                    }                  

                    LinesCanvas.Children.Add(line);
                    OnPropertyChanged(nameof(LinesCanvas));
                    tempListAdd.Add(new Tuple<int,int,Line>(obj.Item1,obj.Item2,line));
                    tempListDelete.Add(obj);
                }
            }
           foreach(var obj in tempListDelete)
                LinesForCanvasFirst.Remove(obj);
           foreach(var obj in tempListAdd)
                LinesForCanvasFirst.Add(obj);

            MainWindowViewModel.LinesForCanvasFirst = LinesForCanvasFirst;
            tempListAdd.Clear();
            tempListDelete.Clear();
           

        }
        private int GetCanvasId(Canvas canvas)
        {
            string name = canvas.Name;
            string br = name.Remove(0, 6);
            return int.Parse(br);
        }
        private void AddOnCanvas(Canvas canvas)
        {
            string name = canvas.Name;
            switch (name)
            {
                case "Canvas1": EntitesInCanvas[0]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas2": EntitesInCanvas[1]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas3": EntitesInCanvas[2]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas4": EntitesInCanvas[3]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas5": EntitesInCanvas[4]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas6": EntitesInCanvas[5]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas7": EntitesInCanvas[6]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas8": EntitesInCanvas[7]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas9": EntitesInCanvas[8]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas10": EntitesInCanvas[9]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas11": EntitesInCanvas[10]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
                case "Canvas12": EntitesInCanvas[11]= new EnitityInCanvas(SelectedEntity,canvas,true) ;break; 
               
                default: break;
                
            }
            MainWindowViewModel.EntitesInCanvas= EntitesInCanvas;
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
