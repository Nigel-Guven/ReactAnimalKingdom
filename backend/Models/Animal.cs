namespace backend.Models
{
    public enum AnimalStatus
    {
        Safe,
        Vulnerable,
        Endangered,
        Extinct
    }

    public class Animal
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LatinName { get; set; }
        public string? Type { get; set; }
        public List<string>? Location { get; set; }
        public AnimalStatus Status { get; set; }
    }
}