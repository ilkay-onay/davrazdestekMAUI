public class DvDestekUrunler{
    
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