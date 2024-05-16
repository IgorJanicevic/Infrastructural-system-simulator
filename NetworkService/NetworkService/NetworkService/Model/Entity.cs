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
    public class Entity
    {
        public int Id {  get; set; }
        public Types Type { get; set; }
        public string Name { get; set; }

        public string ImagePath {  get; set; }

        public List<int> Measures { get; set; }

        public int LastMeasure { get; set; }

        public Entity()
        {
            Measures = new List<int>();
        }

        public Entity(string name, Types type, string imagePath)
        {
            Id = 7;
            LastMeasure = 0;
            Type = type;
            Name = name;
            ImagePath = imagePath;
            Measures = new List<int>();
        }

        public void Update(int measure)
        {

            this.Measures.Add(measure);
            this.LastMeasure = measure;
        }

        public override string ToString()
        {
            return $"{Id} - {Type} {Name}";
        }
    }
}
