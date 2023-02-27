namespace NationalParks.ViewModels;

public partial class ContactsVM : CollapsibleViewVM
{
    [ObservableProperty] ICollection<PhoneContact> phoneContacts;
    [ObservableProperty] ICollection<EmailContact> emailContacts;

    public ContactsVM(string title, bool isOpen, ICollection<PhoneContact> phoneList = null, ICollection<EmailContact> emailList = null) : base(title, isOpen)
    {
        PhoneContacts = phoneList;
        EmailContacts = emailList;
        HasContent = PhoneContacts?.Count > 0 || EmailContacts?.Count > 0;
    }    
}
