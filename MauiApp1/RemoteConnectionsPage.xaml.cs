using MauiApp1.Services;
using MauiApp1.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

namespace MauiApp1
{
    public partial class RemoteConnectionsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private int _currentPage = 1;
        private const int PageSize = 10;
        private int _totalPageCount;

        public ObservableCollection<RemoteConnection> RemoteConnections { get; set; }

        public RemoteConnectionsPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Server=192.168.100.220;Database=MAUI;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;");
            RemoteConnections = new ObservableCollection<RemoteConnection>();
            LoadRemoteConnectionsAsync();
        }

        private async void OnPreviousClicked(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadRemoteConnectionsAsync();
            }
        }

        private async void OnNextClicked(object sender, EventArgs e)
        {
            if (_currentPage < _totalPageCount)
            {
                _currentPage++;
                await LoadRemoteConnectionsAsync();
            }
        }

        private async Task LoadRemoteConnectionsAsync()
        {
            try
            {
                var remoteConnections = await _databaseService.GetRemoteConnectionsAsync(_currentPage, PageSize);
                var totalRemoteConnections = await _databaseService.GetTotalRemoteConnectionCountAsync();
                _totalPageCount = (int)Math.Ceiling(totalRemoteConnections / (double)PageSize);

                RemoteConnectionsGrid.Children.Clear();
                RemoteConnectionsGrid.RowDefinitions.Clear();

                // Add headers
                AddGridHeader("ID", 0);
                AddGridHeader("Baglanan", 1);
                AddGridHeader("BaglananUniq", 2);
                AddGridHeader("BaglananIp", 3);
                AddGridHeader("Yon", 4);
                AddGridHeader("Musteri", 5);
                AddGridHeader("MusteriUniq", 6);
                AddGridHeader("MusteriIp", 7);
                AddGridHeader("BaglantiSaat", 8);
                AddGridHeader("BaglantiTarih", 9);
                AddGridHeader("BaglantiSure", 10);
                AddGridHeader("BaglantiAciklama", 11);
                AddGridHeader("BaglantiDestekNo", 12);
                AddGridHeader("BaglantiUniq", 13);
                int row = 1;
                foreach (var remoteConnection in remoteConnections)
                {
                    AddGridRow();
                    AddGridCell(remoteConnection.ID.ToString(), row, 0);
                    AddGridCell(remoteConnection.Baglanan, row, 1);
                    AddGridCell(remoteConnection.BaglananUniq, row, 2);
                    AddGridCell(remoteConnection.BaglananIp, row, 3);
                    AddGridCell(remoteConnection.Yon, row, 4);
                    AddGridCell(remoteConnection.Musteri, row, 5);
                    AddGridCell(remoteConnection.MusteriUniq, row, 6);
                    AddGridCell(remoteConnection.MusteriIp, row, 7);
                    AddGridCell(remoteConnection.BaglantiSaat.ToString(), row, 8);
                    AddGridCell(remoteConnection.BaglantiTarih.ToString(), row, 9);
                    AddGridCell(remoteConnection.BaglantiSure.ToString(), row, 10);
                    AddGridCell(remoteConnection.BaglantiAciklama, row, 11);
                    AddGridCell(remoteConnection.BaglantiDestekNo, row, 12);
                    AddGridCell(remoteConnection.BaglantiUniq, row, 13);
                    row++;
                }

                PageInfoLabel.Text = $"Page {_currentPage} of {_totalPageCount}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadRemoteConnectionsAsync: {ex.Message}");
                await DisplayAlert("Error", "Failed to load remote connections. Please try again later.", "OK");
            }
        }

        private void AddGridHeader(string headerText, int column)
        {
            var frame = new Frame
            {
                Content = new Label
                {
                    Text = headerText,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(5)
                },
                BorderColor = Colors.Black,
                Margin = new Thickness(1)
            };
            RemoteConnectionsGrid.Children.Add(frame);
            Grid.SetRow(frame, 0);
            Grid.SetColumn(frame, column);
        }

        private void AddGridCell(string cellText, int row, int column)
        {
            var frame = new Frame
            {
                Content = new Label
                {
                    Text = cellText,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(5)
                },
                BorderColor = Colors.Black,
                Margin = new Thickness(1)
            };
            RemoteConnectionsGrid.Children.Add(frame);
            Grid.SetRow(frame, row);
            Grid.SetColumn(frame, column);
        }

        private void AddGridRow()
        {
            RemoteConnectionsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }
    }
}
