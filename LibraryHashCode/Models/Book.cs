namespace LibraryHashCode.Models
{
    public class Book
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public int NumApear { get; set; } = 0;
        public int HasUsed { get; internal set; }
    }
}