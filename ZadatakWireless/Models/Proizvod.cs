namespace ZadatakWireless.Models
{
    public class Proizvod
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public Kategorija Kategorija { get; set; }
        public Dobavljac Dobavljac { get; set; }
        public Proizvodjac Proizvodjac { get; set; }
        public double Cena { get; set; }
    }
}