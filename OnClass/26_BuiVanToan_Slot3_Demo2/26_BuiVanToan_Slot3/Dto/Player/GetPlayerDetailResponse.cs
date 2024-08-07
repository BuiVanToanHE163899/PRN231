namespace _26_BuiVanToan_Slot3.Dto.Player
{


    public class GetPlayerDetailResponse
    {

        public string NickName { get; set; }

        public DateTime JoinedDate { get; set; }

        public List<GetPlayerInstrumentResponse> PlayerInstruments { get; set; }
    }

}