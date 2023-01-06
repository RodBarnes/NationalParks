namespace NationalParks.Models
{
    public class Filter
    {
        public static ObservableCollection<Models.State> StateSelections { get; } = new();
        public static ObservableCollection<Models.Topic> TopicSelections { get; } = new();
        public static ObservableCollection<Models.Activity> ActivitySelections { get; } = new();

        public List<Topic> Topics { get; set; } = new();
        public List<Activity> Activities { get; set; } = new();
        public List<State> States { get; set; } = new();
    }
}
