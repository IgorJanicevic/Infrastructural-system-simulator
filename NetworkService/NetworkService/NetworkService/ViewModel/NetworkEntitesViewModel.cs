using MVVM3.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class NetworkEntitesViewModel : BindableBase
    {

        private ObservableCollection<Entity> Entites = new ObservableCollection<Entity>();
        private Entity SelectedEntity= new Entity();
        public NetworkEntitesViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            Entity ob1= new Entity("Igor",Types.Panel, "C:\\Users\\HomePC\\Pictures\\Screenshots\\Screenshot 2024-04-25 122954.png");
            Entity nn= new Entity("Igor",Types.Generator, "C:\\Users\\HomePC\\Pictures\\Screenshots\\Screenshot 2024-04-25 122954.png");
            Entites.Add(ob1);
            Entites.Add(ob1);
        }
    }
}
