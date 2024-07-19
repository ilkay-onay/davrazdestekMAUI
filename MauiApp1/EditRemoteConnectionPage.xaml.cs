using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Extensions.Logging; // Bu ad alanını eklemeyi unutmayın

namespace MauiApp1
{
    public partial class EditRemoteConnectionPage : ContentPage
    {
        private readonly RemoteConnection _remoteConnection;
        private readonly DatabaseService _databaseService;
        private readonly ILogger<EditRemoteConnectionPage> _logger;

        public EditRemoteConnectionPage(RemoteConnection remoteConnection, DatabaseService databaseService, ILogger<EditRemoteConnectionPage> logger)
        {
            InitializeComponent();
            _remoteConnection = remoteConnection;
            _databaseService = databaseService;
            _logger = logger;
            BindingContext = _remoteConnection;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                var query = "UPDATE Dv_Destek_Uzak SET Baglanan = @Baglanan, BaglananUniq = @BaglananUniq, BaglananIp = @BaglananIp, Yon = @Yon, Musteri = @Musteri, MusteriUniq = @MusteriUniq, MusteriIp = @MusteriIp, BaglantiSaat = @BaglantiSaat, BaglantiTarih = @BaglantiTarih, BaglantiSure = @BaglantiSure, BaglantiAciklama = @BaglantiAciklama, BaglantiDestekNo = @BaglantiDestekNo, BaglantiUniq = @BaglantiUniq, MusteriErpId = @MusteriErpId, DestekUrunId = @DestekUrunId, TalepDetay = @TalepDetay, ToplamSure = @ToplamSure WHERE Id = @Id";
                var parameters = new Dictionary<string, object>
                {
                    { "@Baglanan", _remoteConnection.Baglanan },
                    { "@BaglananUniq", _remoteConnection.BaglananUniq },
                    { "@BaglananIp", _remoteConnection.BaglananIp },
                    { "@Yon", _remoteConnection.Yon },
                    { "@Musteri", _remoteConnection.Musteri },
                    { "@MusteriUniq", _remoteConnection.MusteriUniq },
                    { "@MusteriIp", _remoteConnection.MusteriIp },
                    { "@BaglantiSaat", _remoteConnection.BaglantiSaat },
                    { "@BaglantiTarih", _remoteConnection.BaglantiTarih },
                    { "@BaglantiSure", _remoteConnection.BaglantiSure },
                    { "@BaglantiAciklama", _remoteConnection.BaglantiAciklama },
                    { "@BaglantiDestekNo", _remoteConnection.BaglantiDestekNo },
                    { "@BaglantiUniq", _remoteConnection.BaglantiUniq },
                    { "@MusteriErpId", _remoteConnection.MusteriErpId },
                    { "@DestekUrunId", _remoteConnection.DestekUrunId },
                    { "@TalepDetay", _remoteConnection.TalepDetay },
                    { "@ToplamSure", _remoteConnection.ToplamSure },
                    { "@Id", _remoteConnection.ID }
                };
                await _databaseService.ExecuteQueryAsync(query, parameters);

                await DisplayAlert("Success", "Record updated successfully.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to update record: {ex.Message}", "OK");
            }
        }
    }
}
