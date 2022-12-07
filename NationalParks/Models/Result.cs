namespace NationalParks.Models
{
    public class Result
    {
        public string Total { get; set; }
        public string Limit { get; set; }
        public string Start { get; set; }
        public List<Park> Data { get; set; }

        public int iTotal
        {
            get
            {
                if (int.TryParse(Total, out int i))
                {
                    return i;
                }
                else
                {
                    return -1;
                }
            }
        }

        public int iLimit
        {
            get
            {
                if (int.TryParse(Limit, out int i))
                {
                    return i;
                }
                else
                {
                    return -1;
                }
            }
        }

        public int iStart
        {
            get
            {
                if (int.TryParse(Start, out int i))
                {
                    return i;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
