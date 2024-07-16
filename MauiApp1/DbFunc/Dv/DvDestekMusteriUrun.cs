using System;

public class DvDestekMusteriUrun
{
    public int Id { get; set; }
    public int MusteriId { get; set; }
    public int UrunId { get; set; }
    public DateTime BaslamaTarihi { get; set; }
    public DateTime SonKullanimTarihi { get; set; }
    public string Aciklama { get; set; }
    public int Miktar { get; set; }
    public short Durum { get; set; }
    public int BirimId { get; set; }

    public DvDestekMusteriUrun() { }

    public DvDestekMusteriUrun(int musteriId, int urunId, DateTime baslamaTarihi, DateTime sonKullanimTarihi, string aciklama, int miktar, short durum, int birimId)
    {
        MusteriId = musteriId;
        UrunId = urunId;
        BaslamaTarihi = baslamaTarihi;
        SonKullanimTarihi = sonKullanimTarihi;
        Aciklama = aciklama;
        Miktar = miktar;
        Durum = durum;
        BirimId = birimId;
    }
}