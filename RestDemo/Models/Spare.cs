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
        public string price { get; set; }
        public string vin { get; set; }
        public Img[] images { get; set; }

        public override string ToString()
        {
            return "id " + id + " category " + category.id + " provider " + provider.id + " name " + name +
                   " description " + description + " price " + price + " vin " + vin + " images ";
        }
    }
}