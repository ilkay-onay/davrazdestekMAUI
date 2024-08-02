using MauiApp1.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace MauiApp1
{
    public partial class SettingsPage : ContentPage
    {
        public ObservableCollection<string> Languages { get; set; }
        private readonly DatabaseService _databaseService; // Assuming you might need this for further functionality

        public SettingsPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Server=192.168.100.220;Database=Dv;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;");
            Languages = new ObservableCollection<string>
            {
                "Türkçe",
                "İngilizce",
                "Fransýzca",
                "İspanyolca",
                "Almanca",
                "İtalyanca",


            };

            BindingContext = this;
        }

        private void OnLanguagePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (LanguagePicker.SelectedIndex != -1)
            {
                string selectedLanguage = Languages[LanguagePicker.SelectedIndex];
                // Handle language change logic here
                DisplayAlert("Dil ", $"Seçtiğiniz Dil : {selectedLanguage}", "Tamam");
            }
        }
        private async void OnLicenseButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Lisans", " Copyright © 2024 <copyright holders>\r\n\r\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:\r\n\r\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\r\n\r\nTHE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.", "Kapat");
        }

        private async void OnCreditsButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Emeði Geçenler", "İlkay Onay\nBatuhan Çetin\nEnis Yaman\nAli Gür", "Kapat");
        }
    }
}
