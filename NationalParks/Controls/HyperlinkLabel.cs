/*
  Credit to Shagaroo (https://stackoverflow.com/users/8761813/shagaroo) on Stackoverflow
  for this control (https://stackoverflow.com/questions/73695949/net-maui-how-to-include-a-link-in-a-label/74384181?r=Saves_AllUserSaves#74384181)
*/
namespace NationalParks.Controls
{
    public class HyperlinkLabel : Label
    {
        public static readonly BindableProperty UrlProperty =
            BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkLabel), null);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public HyperlinkLabel()
        {
            TextDecorations = TextDecorations.Underline;
            TextColor = Colors.Blue;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await Launcher.OpenAsync(Url))
            });
        }
    }
}
