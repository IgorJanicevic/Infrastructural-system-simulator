using NetworkService.Helpers;
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
            int id = 0;
            do
            {
                Random random = new Random();
                id = random.Next(1, 500);

            } while (GetAllId().Contains(id));
            Id = id;
            LastMeasure = 0;
            Type = type;
            Name = name;
            ImagePath = imagePath;
            Measures = new List<int>();
        }

        public Entity(Entity entity)
        {
            this.Type = entity.Type;
            this.Name = entity.Name;
            this.ImagePath = entity.ImagePath;
            this.Id = entity.Id;
            this.LastMeasure = entity.LastMeasure;
            this.Measures = entity.Measures;
        }

        public void Update(int measure)
        {

            Measures.Insert(0,measure);
            LastMeasure = measure;
        }

        public override string ToString()
        {
            return $"{Id} - {Type} {Name} LM: {LastMeasure}";
        }

        private List<int> GetAllId()
        {
            DataIO serializer = new DataIO();
            List<Entity> entities = serializer.DeSerializeObject<List<Entity>>("Entites.xml");
            List<int> ids= new List<int>();
            foreach(Entity entity in entities)
            {
                ids.Add(entity.Id);
            }
            return ids ;
        }
    }
}
