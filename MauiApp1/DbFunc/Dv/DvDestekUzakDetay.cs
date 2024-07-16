public class DvDestekUzakDetay{

    /*
    MAUI.dbo.Dv_Destek_Uzak_Detay.Id
MAUI.dbo.Dv_Destek_Uzak_Detay.DestekId
MAUI.dbo.Dv_Destek_Uzak_Detay.PersonelId
MAUI.dbo.Dv_Destek_Uzak_Detay.YapilanIslem
MAUI.dbo.Dv_Destek_Uzak_Detay.IslemTarihi
MAUI.dbo.Dv_Destek_Uzak_Detay.DestekVerilenKisi
    */

    public int Id { get; set; }
    public int DestekId { get; set; }
    public int PersonelId { get; set; }
    public string YapilanIslem { get; set; }
    public DateTime IslemTarihi { get; set; }
    public int DestekVerilenKisi { get; set; }

    public DvDestekUzakDetay() { }

    public DvDestekUzakDetay(int destekId, int personelId, string yapilanIslem, DateTime islemTarihi, int destekVerilenKisi)
    {
        DestekId = destekId;
        PersonelId = personelId;
        YapilanIslem = yapilanIslem;
        IslemTarihi = islemTarihi;
        DestekVerilenKisi = destekVerilenKisi;
    }

    public override string ToString()
    {
        return $"DvDestekUzakDetay{{id={Id}, destekId={DestekId}, personelId={PersonelId}, yapilanIslem='{YapilanIslem}', islemTarihi={IslemTarihi}, destekVerilenKisi={DestekVerilenKisi}}}";
    }

}