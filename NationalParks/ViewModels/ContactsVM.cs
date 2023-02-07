namespace NationalParks.ViewModels;

public partial class ContactsVM : CollapsibleViewVM
{
    [ObservableProperty] List<PhoneContact> phoneContacts;
    [ObservableProperty] List<EmailContact> emailContacts;

    public ContactsVM(string title, bool isOpen) : base(title, isOpen)
    {

    }    
}
