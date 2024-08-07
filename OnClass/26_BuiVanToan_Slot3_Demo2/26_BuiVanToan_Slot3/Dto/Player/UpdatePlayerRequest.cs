using System.ComponentModel.DataAnnotations;

namespace _26_BuiVanToan_Slot3.Dto.Player
{


    public class UpdatePlayerRequest
    {
        [Required]
        public string NickName { get; set; }
    }

}
