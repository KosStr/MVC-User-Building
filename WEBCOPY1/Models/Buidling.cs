using System.ComponentModel.DataAnnotations;

namespace WEBCOPY1.Models
{
    //Class for building
    public class Building
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int Square { get; set; }
        public int Price { get; set; } 
        public int YearOfCreation { get; set; }
    }
}
