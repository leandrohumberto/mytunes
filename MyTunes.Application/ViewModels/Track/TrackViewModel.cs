namespace MyTunes.Application.ViewModels.Track
{
    public class TrackViewModel
    {
        public TrackViewModel(uint number, string name, TimeSpan length)
        {
            Number = number;
            Name = name;
            Length = length;
        }

        public uint Number { get; private set; }

        public string Name { get; private set; }

        public TimeSpan Length { get; private set; }
    }
}