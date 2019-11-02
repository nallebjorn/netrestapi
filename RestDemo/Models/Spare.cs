namespace RestDemo.Models
{
    public class Spare
    {
        public int id { get; set; }
        public Category category { get; set; }
        public CarMark carMark { get; set; }
        public Provider provider { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string vin { get; set; }
    }
}