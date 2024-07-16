interface IDbFunctions
{

    void InsertDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar);
    void UpdateDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar);
    void DeleteDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar);
    List<DvDestekCagrilar> GetAllDvDestekCagrilar();
    List<DvDestekCagrilar> SearchDvDestekCagrilar(string search);
    List<DvDestekCagrilar> GetByFirst100DvDestekCagrilar();
    DvDestekCagrilar GetDvDestekCagrilarById(int id);


}