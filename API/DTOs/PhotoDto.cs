namespace API.DTOs
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public bool AllowPhoto { get; set; }
        public bool ShowAdminPhoto {get;set;}
    }
}