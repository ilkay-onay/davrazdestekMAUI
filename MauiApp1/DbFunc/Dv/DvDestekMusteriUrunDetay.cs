public class DvDestekMusteriUrunDetay
{

    /*
    MAUI.dbo.Dv_Destek_Musteri_Urun_Detay.Id
    MAUI.dbo.Dv_Destek_Musteri_Urun_Detay.BaglantiId
    MAUI.dbo.Dv_Destek_Musteri_Urun_Detay.Tarih
    MAUI.dbo.Dv_Destek_Musteri_Urun_Detay.Aciklama
    MAUI.dbo.Dv_Destek_Musteri_Urun_Detay.PersonelId
    */

    public int Id { get; set; }
    public int BaglantiId { get; set; }
    public DateTime Tarih { get; set; }
    public string Aciklama { get; set; }
    public int PersonelId { get; set; }

    public DvDestekMusteriUrunDetay() { }

    public DvDestekMusteriUrunDetay(int baglantiId, DateTime tarih, string aciklama, int personelId){}

    public override string ToString()
    {
        return $"DvDestekMusteriUrunDetay{{id={Id}, baglantiId={BaglantiId}, tarih={Tarih}, aciklama='{Aciklama}', personelId={PersonelId}}}";
    }    



}