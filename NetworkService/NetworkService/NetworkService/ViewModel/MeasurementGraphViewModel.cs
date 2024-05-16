using MVVM3.Helpers;
using NetworkService.Model;
using Projekat.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkService.ViewModel
{
    public class MeasurementGraphViewModel : BindableBase
    {
        public ObservableCollection<Entity> Entites { get; set; }
        DataIO serializer= new DataIO();
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


        public MeasurementGraphViewModel()
        {
           Entites = serializer.DeSerializeObject<ObservableCollection<Entity>>("Entites.xml");
        }
        public MeasurementGraphViewModel(ObservableCollection<Entity> entites)
        {

            Entites = entites;
        }

        
    }
}
