using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public enum Types
    {
        Panel,Generator
    }
    public class Object
    {
        public int Id {  get; set; }
        public Types Type { get; set; }
        public string Name { get; set; }

        public Object()
        {
        }

        public Object(int id,string name, Types type)
        {
            Id = id;
            Type = type;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} - {Type} {Name}";
        }
    }
}
