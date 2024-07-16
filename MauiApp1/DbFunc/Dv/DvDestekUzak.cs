public class DvDestekUzak{
/*
MAUI.dbo.Dv_Destek_Uzak.ID
MAUI.dbo.Dv_Destek_Uzak.BaglananUniq
MAUI.dbo.Dv_Destek_Uzak.Baglanan
MAUI.dbo.Dv_Destek_Uzak.ToplamSure
MAUI.dbo.Dv_Destek_Uzak.BaglananIp
MAUI.dbo.Dv_Destek_Uzak.Yon
MAUI.dbo.Dv_Destek_Uzak.Musteri
MAUI.dbo.Dv_Destek_Uzak.MusteriUniq
MAUI.dbo.Dv_Destek_Uzak.MusteriIp
MAUI.dbo.Dv_Destek_Uzak.BaglantiSaat
MAUI.dbo.Dv_Destek_Uzak.BaglantiTarih
MAUI.dbo.Dv_Destek_Uzak.BaglantiSure
MAUI.dbo.Dv_Destek_Uzak.BaglantiAciklama
MAUI.dbo.Dv_Destek_Uzak.BaglantiDestekNo
MAUI.dbo.Dv_Destek_Uzak.MusteriErpId
MAUI.dbo.Dv_Destek_Uzak.BaglantiUniq
MAUI.dbo.Dv_Destek_Uzak.DestekUrunId
MAUI.dbo.Dv_Destek_Uzak.TalepDetay
*/

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
public int MusteriErpId { get; set; }
public int DestekUrunId { get; set; }
public string TalepDetay { get; set; }
public TimeSpan ToplamSure { get; set; }


public DvDestekUzak() {
    Baglanan = string.Empty;
    BaglananUniq = string.Empty;
    BaglananIp = string.Empty;
    Yon = string.Empty;
    Musteri = string.Empty;
    MusteriUniq = string.Empty;
    MusteriIp = string.Empty;
    BaglantiAciklama = string.Empty;
    BaglantiDestekNo = string.Empty;
    BaglantiUniq = string.Empty;
    TalepDetay = string.Empty;
}

 

public DvDestekUzak(string baglanan, string baglananUniq, string baglananIp, string yon, string musteri, string musteriUniq, string musteriIp, TimeSpan baglantiSaat, DateTime baglantiTarih, TimeSpan baglantiSure, string baglantiAciklama, string baglantiDestekNo, string baglantiUniq, int musteriErpId, int destekUrunId, string talepDetay, TimeSpan toplamSure)
{
    Baglanan = baglanan;
    BaglananUniq = baglananUniq;
    BaglananIp = baglananIp;
    Yon = yon;
    Musteri = musteri;
    MusteriUniq = musteriUniq;
    MusteriIp = musteriIp;
    BaglantiSaat = baglantiSaat;
    BaglantiTarih = baglantiTarih;
    BaglantiSure = baglantiSure;
    BaglantiAciklama = baglantiAciklama;
    BaglantiDestekNo = baglantiDestekNo;
    BaglantiUniq = baglantiUniq;
    MusteriErpId = musteriErpId;
    DestekUrunId = destekUrunId;
    TalepDetay = talepDetay;
    ToplamSure = toplamSure;
}

public override string ToString(){
    return $"DvDestekUzak{{id={ID}, baglanan='{Baglanan}', baglananUniq='{BaglananUniq}', baglananIp='{BaglananIp}', yon='{Yon}', musteri='{Musteri}', musteriUniq='{MusteriUniq}', musteriIp='{MusteriIp}', baglantiSaat={BaglantiSaat}, baglantiTarih={BaglantiTarih}, baglantiSure={BaglantiSure}, baglantiAciklama='{BaglantiAciklama}', baglantiDestekNo='{BaglantiDestekNo}', baglantiUniq='{BaglantiUniq}', musteriErpId={MusteriErpId}, destekUrunId={DestekUrunId}, talepDetay='{TalepDetay}', toplamSure={ToplamSure}}}";
}








}