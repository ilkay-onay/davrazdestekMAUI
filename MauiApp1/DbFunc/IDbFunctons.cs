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
    
    
    //DvDestekMusteriUrunDetay
    void insertDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay);
    void updateDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay);
    void deleteDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay);
    List<DvDestekMusteriUrunDetay> getAllDvDestekMusteriUrunDetay();
    List<DvDestekMusteriUrunDetay> searchDvDestekMusteriUrunDetay(string search);
    DvDestekMusteriUrunDetay getDvDestekMusteriUrunDetayById(int id);
    
    //DvDestekPersonel
    void insertDvDestekPersonel(DvDestekPersonel dvDestekPersonel);
    void updateDvDestekPersonel(DvDestekPersonel dvDestekPersonel);
    void deleteDvDestekPersonel(DvDestekPersonel dvDestekPersonel);
    List<DvDestekPersonel> getAllDvDestekPersonel();
    List<DvDestekPersonel> searchDvDestekPersonel(string search);
    DvDestekPersonel getDvDestekPersonelById(int id);
    

    
    
    // DvDestekUrunler
    void insertDvDestekUrunler(DvDestekUrunler dvDestekUrunler);
    void updateDvDestekUrunler(DvDestekUrunler dvDestekUrunler);
    void deleteDvDestekUrunler(DvDestekUrunler dvDestekUrunler);
    List<DvDestekUrunler> getAllDvDestekUrunler();
    List<DvDestekUrunler> searchDvDestekUrunler(string search);
    DvDestekUrunler getDvDestekUrunlerById(int id);
    
    
    
    
    //DvDestekUzak
    void insertDvDestekUzak(DvDestekUzak dvDestekUzak);
    void updateDvDestekUzak(DvDestekUzak dvDestekUzak);
    void deleteDvDestekUzak(DvDestekUzak dvDestekUzak);
    List<DvDestekUzak> getAllDvDestekUzak();
    List<DvDestekUzak> searchDvDestekUzak(string search);
    DvDestekUzak getDvDestekUzakById(int id);
    
    
    //DvDestekUzakDetay
    void insertDvDestekUzakDetay(DvDestekUzakDetay dvDestekUzakDetay);
    void updateDvDestekUzakDetay(DvDestekUzakDetay dvDestekUzakDetay);
    void deleteDvDestekUzakDetay(DvDestekUzakDetay dvDestekUzakDetay);
    List<DvDestekUzakDetay> getAllDvDestekUzakDetay();
    List<DvDestekUzakDetay> searchDvDestekUzakDetay(string search);
    DvDestekUzakDetay getDvDestekUzakDetayById(int id);
    
    
    


}