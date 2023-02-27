namespace NationalParks.Models;

public class Contacts
{
    public ICollection<PhoneContact> PhoneNumbers { get; set; }
    public ICollection<EmailContact> EmailAddresses { get; set; }
}
