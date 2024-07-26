using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
        public class MusteriUrun
        {
            public int Id { get; set; }
            public int MusteriId { get; set; }
            public int UrunId { get; set; }
            public DateTime BaslamaTarihi { get; set; }
            public DateTime SonKullanimTarihi { get; set; }
            public string Aciklama { get; set; }
            public int Miktar { get; set; }
            public string Durum { get; set; }
            public int BirimId { get; set; }
        }
    }

