using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeneApp.Common
{
    [Table("Zenek")]
    public class Zene
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A cím megadása kötelező!")]
        public string Cim { get; set; } = string.Empty;

        [Required(ErrorMessage = "Az előadó megadása kötelező!")]
        public string Eloado { get; set; } = string.Empty;

        [Range(1, 3000, ErrorMessage = "A kiadás évének pozitívnak kell lennie.")]
        public int KiadasiEv { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A hossznak pozitívnak kell lennie (másodperc).")]
        public int Hossz { get; set; }

        [Range(1, 10, ErrorMessage = "A prioritás 1 és 10 között legyen.")]
        public int Prioritas { get; set; }

        // Formázott megjelenítés
        public string HosszFormazva => $"{Hossz / 60}:{(Hossz % 60):D2}";
    }
}