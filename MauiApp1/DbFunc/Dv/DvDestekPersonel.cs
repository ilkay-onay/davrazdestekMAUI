public class DvDestekPersonel
{
    public int Id { get; set; }
    public string Ad_Soyad { get; set; }
    public string E_posta { get; set; }
    public string Sifre { get; set; }
    public string Telefon { get; set; }
    public string Dahili { get; set; }

    public DvDestekPersonel() { }

    public DvDestekPersonel(string adSoyad, string eposta, string sifre, string telefon, string dahili)
    {
        Ad_Soyad = adSoyad;
        E_posta = eposta;
        Sifre = sifre;
        Telefon = telefon;
        Dahili = dahili;
    }

    public override string ToString()
    {
        return $"DvDestekPersonel{{id={Id}, adSoyad='{Ad_Soyad}', eposta='{E_posta}', sifre='{Sifre}', telefon='{Telefon}', dahili='{Dahili}'}}";
    }
}
