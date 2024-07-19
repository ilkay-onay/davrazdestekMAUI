using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.ViewModels
{
    public class RemoteConnection
    {
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
    }

    public class RemoteConnectionsPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RemoteConnection> RemoteConnections { get; set; }

        public RemoteConnectionsPageViewModel()
        {
            // Örnek veri ekleyin veya servisten veri alın
            RemoteConnections = new ObservableCollection<RemoteConnection>
            {
                new RemoteConnection
                {
                    ID = 1,
                    Baglanan = "John Doe",
                    BaglananUniq = "123",
                    BaglananIp = "192.168.1.1",
                    Yon = "G",
                    Musteri = "Jane Smith",
                    MusteriUniq = "456",
                    MusteriIp = "192.168.1.2",
                    BaglantiSaat = new TimeSpan(15, 0, 0),
                    BaglantiTarih = DateTime.Now,
                    BaglantiSure = new TimeSpan(1, 30, 0),
                    BaglantiAciklama = "Support Call",
                    BaglantiDestekNo = "789",
                    BaglantiUniq = "101"
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
