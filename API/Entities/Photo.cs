using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public AppUser AppUser { get; set; }  // Fully defining the relation between Appuser and Photo
        public int AppUserId { get; set; } // Fully defining the relation between Appuser and Photo
    }
}