using System;


public class DvDestekKisiler
{
    public int Id { get; set; }
    public string AdSoyad { get; set; }
    public string Gorev { get; set; }
    public string Mail { get; set; }
    public string Telefon { get; set; }
    public short Durum { get; set; }
    public int BagliFirmaId { get; set; }
    public string Aciklama { get; set; }

    public DvDestekKisiler() { }

    public DvDestekKisiler(string adSoyad, string gorev, string mail, string telefon, short durum, int bagliFirmaId, string aciklama)
    {
        AdSoyad = adSoyad;
        Gorev = gorev;
        Mail = mail;
        Telefon = telefon;
        Durum = durum;
        BagliFirmaId = bagliFirmaId;
        Aciklama = aciklama;
    }

    public override string ToString()
    {
        return $"{AdSoyad} - {Gorev} - {Mail} - {Telefon} - {Durum} - {BagliFirmaId} - {Aciklama}";
    }
}