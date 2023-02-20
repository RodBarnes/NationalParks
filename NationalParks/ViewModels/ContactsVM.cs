namespace NationalParks.ViewModels;

public partial class ContactsVM : CollapsibleViewVM
{
    [ObservableProperty] List<PhoneContact> phoneContacts;
    [ObservableProperty] List<EmailContact> emailContacts;

    public ContactsVM(string title, bool isOpen, List<PhoneContact> phoneList = null, List<EmailContact> emailList = null) : base(title, isOpen)
    {
        PhoneContacts = phoneList;
        EmailContacts = emailList;
        HasContent = PhoneContacts?.Count > 0 || EmailContacts?.Count > 0;
    }    
}
