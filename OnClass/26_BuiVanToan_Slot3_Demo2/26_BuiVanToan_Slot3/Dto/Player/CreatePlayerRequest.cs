using _26_BuiVanToan_Slot3.Dto.PlayerInstrument;
using System.ComponentModel.DataAnnotations;

namespace _26_BuiVanToan_Slot3.Dto.Player
{


    public class CreatePlayerRequest
    {
        [Required]
        public string NickName { get; set; }
        [Required]
        public List<CreatePlayerInstrumentRequest> PlayerInstruments { get; set; }
    }
}
