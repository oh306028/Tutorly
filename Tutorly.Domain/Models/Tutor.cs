
namespace Tutorly.Domain.Models
{
    public class Tutor : User
    {
        public Tutor()
        {
            
        }
        public List<Post> Posts { get; set; } = new();
        public byte Experience { get; set; }
        public string Description { get; set; } 


    }
}
