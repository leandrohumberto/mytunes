namespace MyTunes.Application.ViewModels.Track
{
    public class TrackViewModel
    {
        public TrackViewModel(uint number, string title, TimeSpan length)
        {
            Number = number;
            Title = title;
            Length = length;
        }

        public uint Number { get; private set; }

        public string Title { get; private set; }

        public TimeSpan Length { get; private set; }
    }
}