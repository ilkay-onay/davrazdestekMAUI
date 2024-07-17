interface IDbFunctions
{

    void InsertDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar);
    void UpdateDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar);
    void DeleteDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar);
    List<DvDestekCagrilar> GetAllDvDestekCagrilar();
    List<DvDestekCagrilar> SearchDvDestekCagrilar(string search);
    List<DvDestekCagrilar> GetByFirst100DvDestekCagrilar();
    DvDestekCagrilar GetDvDestekCagrilarById(int id);
    
    
    void insertDvDestekKisiler(DvDestekKisiler dvDestekKisiler);
    void updateDvDestekKisiler(DvDestekKisiler dvDestekKisiler);
    void deleteDvDestekKisiler(DvDestekKisiler dvDestekKisiler);
    List<DvDestekKisiler> getAllDvDestekKisiler();
    List<DvDestekKisiler> searchDvDestekKisiler(string search);
    List<DvDestekKisiler> getByFirst100DvDestekKisiler();
    DvDestekKisiler getDvDestekKisilerById(int id);
    
    void insertDvDestekUrunler(DvDestekUrunler dvDestekUrunler);
    void updateDvDestekUrunler(DvDestekUrunler dvDestekUrunler);
    void deleteDvDestekUrunler(DvDestekUrunler dvDestekUrunler);
    List<DvDestekUrunler> getAllDvDestekUrunler();
    List<DvDestekUrunler> searchDvDestekUrunler(string search);
    List<DvDestekUrunler> getByFirst100DvDestekUrunler();
    DvDestekUrunler getDvDestekUrunlerById(int id);
    


}