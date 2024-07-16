using System;

public class DvDestekCagrilar
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

    public DvDestekCagrilar() { }

    public DvDestekCagrilar(DateTime tarih, string arayan, string aranan, TimeSpan toplamSure, TimeSpan beklemeSuresi, TimeSpan gorusmeSuresi, int sonuc, short tipi)
    {
        Tarih = tarih;
        Arayan = arayan;
        Aranan = aranan;
        ToplamSure = toplamSure;
        BeklemeSuresi = beklemeSuresi;
        GorusmeSuresi = gorusmeSuresi;
        Sonuc = sonuc;
        Tipi = tipi;
    }

    public override string ToString()
    {
        return $"DvDestekCagrilar{{id={Id}, tarih={Tarih}, Arayan='{Arayan}', Aranan='{Aranan}', toplamSure={ToplamSure}, beklemeSuresi={BeklemeSuresi}, gorusmeSuresi={GorusmeSuresi}, sonuc={Sonuc}, tipi={Tipi}}}";
    }

}