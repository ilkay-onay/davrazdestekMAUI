public class DvDestekPersonel
{
    /*
    MAUI.dbo.Dv_Destek_Personel.Id
MAUI.dbo.Dv_Destek_Personel.Ad_Soyad
MAUI.dbo.Dv_Destek_Personel.E-posta
MAUI.dbo.Dv_Destek_Personel.Sifre
MAUI.dbo.Dv_Destek_Personel.Telefon
MAUI.dbo.Dv_Destek_Personel.Dahili

    */

    public int Id { get; set; }
    public string AdSoyad { get; set; }
    public string Eposta { get; set; }
    public string Sifre { get; set; }
    public string Telefon { get; set; }
    public string Dahili { get; set; }


    public DvDestekPersonel() { }

    public DvDestekPersonel(string adSoyad, string eposta, string sifre, string telefon, string dahili)
    {
        AdSoyad = adSoyad;
        Eposta = eposta;
        Sifre = sifre;
        Telefon = telefon;
        Dahili = dahili;
    }

    public override string ToString()
    {
        return $"DvDestekPersonel{{id={Id}, adSoyad='{AdSoyad}', eposta='{Eposta}', sifre='{Sifre}', telefon='{Telefon}', dahili='{Dahili}'}}";
    }

}