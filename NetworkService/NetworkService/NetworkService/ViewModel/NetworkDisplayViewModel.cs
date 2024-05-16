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
    public class NetworkDisplayViewModel : BindableBase
    {
        public IEnumerable<Types> Types
        {
            get
            {
                return (IEnumerable<Types>)Enum.GetValues(typeof(Model.Types));
            }
        }
        private DataIO serializer= new DataIO();

        public ObservableCollection<EntitesWithType> EntitesWithTypesCollection { get; set; }

        public NetworkDisplayViewModel()
        {
            LoadData();
        }

        

        private void LoadData()
        {
            ObservableCollection<Entity> entities = serializer.DeSerializeObject<ObservableCollection<Entity>>("Entites.xml");
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
