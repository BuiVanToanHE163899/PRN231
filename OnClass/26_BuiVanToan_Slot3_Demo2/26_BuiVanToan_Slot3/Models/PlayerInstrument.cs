using System.ComponentModel.DataAnnotations;

namespace _26_BuiVanToan_Slot3.Models
{


    public class PlayerInstrument
    {
        [Key]
        public int PlayerInstrumentId { get; set; }
        public int PlayerId { get; set; }
        public int InstrumentTypeId { get; set; }
        public string ModelName { get; set; }
        public string Level { get; set; }
    }


}
