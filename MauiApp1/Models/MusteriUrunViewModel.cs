namespace MauiApp1.Models
{
    public class MusteriUrunViewModel
    {
        // Properties from MusteriUrun
        public int Id { get; set; }
        public int MusteriId { get; set; }
        public int UrunId { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime SonKullanimTarihi { get; set; }
        public string Aciklama { get; set; }
        public int Miktar { get; set; }
        public short Durum { get; set; }
        public int BirimId { get; set; }

        // Properties from Dv_Destek_Urunler
        public string UrunAdi { get; set; }
    }
}

