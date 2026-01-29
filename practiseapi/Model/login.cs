using System.ComponentModel.DataAnnotations;

namespace practiseapi.Model
{
    public class login
    {
        [Key]
       public int id { get; set; }
        public string username { get; set; }
        public int password { get; set; }
        public string Role { get; set; }
    }
}
