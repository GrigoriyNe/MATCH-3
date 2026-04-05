namespace Leaderboard
{
    public class RecordString
    {
        public RecordString(string date, int record)
        {
            Date = date;
            Record = record;
        }

        public string Date { get; set; }
        public int Record { get; set; }
    }
}