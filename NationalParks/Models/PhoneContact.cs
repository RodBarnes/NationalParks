namespace NationalParks.Models
{
    public class PhoneContact
    {
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public string Type { get; set; }

        public bool HasExtension { get => !String.IsNullOrEmpty(Extension); }
    }
}
