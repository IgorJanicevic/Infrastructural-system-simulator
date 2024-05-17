using MVVMLight.Messaging;
using NetworkService.Model;
using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkService.ViewModel
{
    public class MeasurementGraphViewModel : BindableBase
    {
        private ObservableCollection<Entity> _entites;
        public ObservableCollection<Entity> Entites
        {
            get { return _entites; }
            set
            {
                _entites = value;
                OnPropertyChanged(nameof(Entites));
            }
        }
        private Entity _selectedEntity;
        public Entity SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {                 
                    _selectedEntity = value;
                    OnPropertyChanged(nameof(SelectedEntity));
            }
        }

        public MeasurementGraphViewModel()
        {
        }
        public MeasurementGraphViewModel(ObservableCollection<Entity> entites)
        {
            Entites = entites;
            Messenger.Default.Register<ObservableCollection<Entity>>(this, UpdateValue);

        }

        private void UpdateValue(ObservableCollection<Entity> temp)
        {
            Entites = new ObservableCollection<Entity>(temp);
            foreach (Entity ent in Entites)
            {
                //if (ent.Id) { }
            }
            OnPropertyChanged(nameof(SelectedEntity));
        }




    }
}
