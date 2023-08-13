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
        public bool ShowAdminPhoto {get;set;} = true;
        public bool AllowPhoto { get; set; } = false;

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}