namespace MauiApp1.Models
{
    public class RemoteConnection
    {
        public int ID { get; set; }
        public string Baglanan { get; set; }
        public string BaglananUniq { get; set; }
        public string BaglananIp { get; set; }
        public string Yon { get; set; }
        public string Musteri { get; set; }
        public string MusteriUniq { get; set; }
        public string MusteriIp { get; set; }
        public TimeSpan BaglantiSaat { get; set; }
        public DateTime BaglantiTarih { get; set; }
        public TimeSpan BaglantiSure { get; set; }
        public string BaglantiAciklama { get; set; }
        public string BaglantiDestekNo { get; set; }
        public string BaglantiUniq { get; set; }
        public int? MusteriErpId { get; set; }
        public int? DestekUrunId { get; set; }
        public string TalepDetay { get; set; }
        public TimeSpan ToplamSure { get; set; }
    }
}