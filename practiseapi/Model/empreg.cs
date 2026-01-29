using System.ComponentModel.DataAnnotations;

namespace practiseapi.Model
{
    public class empreg
    {
        [Key]
        public int empid { get; set; }

        public string empname { get; set; }
        public int age { get; set; }
        public string city { get; set; }
    }
}

