interface IDbFunctions
{

    // DvDestekCagrilar
    void InsertDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar);
    void UpdateDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar);
    void DeleteDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar);
    List<DvDestekCagrilar> GetAllDvDestekCagrilar();
    List<DvDestekCagrilar> SearchDvDestekCagrilar(string search);
    DvDestekCagrilar GetDvDestekCagrilarById(int id);
    
    
    // DvDestekKisiler
    void insertDvDestekKisiler(DvDestekKisiler dvDestekKisiler);
    void updateDvDestekKisiler(DvDestekKisiler dvDestekKisiler);
    void deleteDvDestekKisiler(DvDestekKisiler dvDestekKisiler);
    List<DvDestekKisiler> getAllDvDestekKisiler();
    List<DvDestekKisiler> searchDvDestekKisiler(string search);
    DvDestekKisiler getDvDestekKisilerById(int id);
    
    // DvDestekMusteriUrun
    void insertDvDestekMusteriUrun(DvDestekMusteriUrun dvDestekMusteriUrun);
    void updateDvDestekMusteriUrun(DvDestekMusteriUrun dvDestekMusteriUrun);
    void deleteDvDestekMusteriUrun(DvDestekMusteriUrun dvDestekMusteriUrun);
    List<DvDestekMusteriUrun> getAllDvDestekMusteriUrun();
    List<DvDestekMusteriUrun> searchDvDestekMusteriUrun(string search);
    DvDestekMusteriUrun getDvDestekMusteriUrunById(int id);
    

    
    
    // DvDestekUrunler
    void insertDvDestekUrunler(DvDestekUrunler dvDestekUrunler);
    void updateDvDestekUrunler(DvDestekUrunler dvDestekUrunler);
    void deleteDvDestekUrunler(DvDestekUrunler dvDestekUrunler);
    List<DvDestekUrunler> getAllDvDestekUrunler();
    List<DvDestekUrunler> searchDvDestekUrunler(string search);
    DvDestekUrunler getDvDestekUrunlerById(int id);
    
    
    //DvDestekMusteriUrunDetay
    void insertDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay);
    void updateDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay);
    void deleteDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay);
    List<DvDestekMusteriUrunDetay> getAllDvDestekMusteriUrunDetay();
    List<DvDestekMusteriUrunDetay> searchDvDestekMusteriUrunDetay(string search);
    DvDestekMusteriUrunDetay getDvDestekMusteriUrunDetayById(int id);
    
    
    
}