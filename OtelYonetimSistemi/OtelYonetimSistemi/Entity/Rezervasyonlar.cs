namespace OtelYonetimSistemi.Entity
{
    public class Rezervasyonlar
    {
        public int RezId { get; set; }
        public int MusteriId { get; set; }
        public int OtelId { get; set; }
        public string GirisTarihi { get; set; }
        public string CikisTarihi { get; set; }
        public string OdaSayisi { get; set; }
    }
}
