using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class EntitesWithType
    {
        public Types Types {  get; set; }
        public ObservableCollection<Entity> Entites { get; set; }  

        public EntitesWithType() { 
            Entites = new ObservableCollection<Entity>();
        }
    }
}
