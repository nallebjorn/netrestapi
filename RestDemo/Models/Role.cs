namespace RestDemo.Models
{
    public class Role
    {
        public Role(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int id { get; set; }
        public string name { get; set; }
    }
}