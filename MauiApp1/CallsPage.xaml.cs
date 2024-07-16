using MauiApp1.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class CallsPage : ContentPage
    {
        public CallsPage()
        {
            InitializeComponent();
        }
        private readonly DatabaseService _databaseService;
        private readonly ILogger<CallsPage> _logger;
        private int _currentPage = 1;
        private const int PageSize = 10;

        public ObservableCollection<Call> Calls { get; set; }

        public CallsPage(DatabaseService databaseService, ILogger<CallsPage> logger)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _logger = logger;
            Calls = new ObservableCollection<Call>();
            CallsCollectionView.ItemsSource = Calls;
            LoadCallsAsync();
        }

        private async void OnPreviousClicked(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadCallsAsync();
            }
        }

        private async void OnNextClicked(object sender, EventArgs e)
        {
            _currentPage++;
            await LoadCallsAsync();
        }

        private async Task LoadCallsAsync()
        {
            try
            {
                var calls = await _databaseService.GetCallsAsync(_currentPage, PageSize);
                Calls.Clear();
                foreach (var call in calls)
                {
                    Calls.Add(call);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading calls for page {PageNumber}", _currentPage);
                await DisplayAlert("Error", "Failed to load calls.", "OK");
            }
        }
    }
}
