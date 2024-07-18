namespace MauiApp1.Models
{
    public class CallRecord
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public string Arayan { get; set; }
        public string Aranan { get; set; }
        public TimeSpan ToplamSure { get; set; }
        public TimeSpan BeklemeSuresi { get; set; }
        public TimeSpan GorusmeSuresi { get; set; }
        public int Sonuc { get; set; }
        public short Tipi { get; set; }
    }
}

