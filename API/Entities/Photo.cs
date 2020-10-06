using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")] // This will force Ef to name the table 'Photos'.
    public class Photo
    {
        public int Id { get; set; } 
        public string Url { get; set; } 
        public bool IsMain { get; set; }    
        public string PublicId { get; set; }
        public AppUser AppUser { get; set; } // Fully defining the relationship for EF
        public int AppUserId { get; set; } // Fully defining the relationship for EF
    }
}