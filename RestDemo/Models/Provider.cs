namespace RestDemo.Models
{
    public class Provider : User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string address { get; set; }
    }
}