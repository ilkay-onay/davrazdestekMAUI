public class DvDestekUrunler{
    /*
    MAUI.dbo.Dv_Destek_Urunler.Id
MAUI.dbo.Dv_Destek_Urunler.Urun
    */

    public int Id { get; set; }

    public string Urun { get; set; }

    public DvDestekUrunler() { }

    public DvDestekUrunler(string urun)
    {
        Urun = urun;
    }

    public override string ToString()
    {
        return $"DvDestekUrunler{{id={Id}, urun='{Urun}'}}";
    }
    
}