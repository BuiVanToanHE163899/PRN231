using _26_BuiVanToan_Slot3.Dto.Player;
using Microsoft.EntityFrameworkCore;
using _26_BuiVanToan_Slot3.Models;

namespace _26_BuiVanToan_Slot3
{


    public class PlayerService : IPlayerService
    {
        private readonly CodeFirstDemoContext _dbContext;

    public PlayerService(CodeFirstDemoContext dbContext)
        {
            _dbContext = dbContext;
        }
 
        public async Task CreatePlayerAsync(CreatePlayerRequest playerRequest)
        {
          try
                {
                var player = new Player
                {
                    NickName = playerRequest.NickName,
                    Instruments = playerRequest.PlayerInstruments.Select(x => new PlayerInstrument
                    {
                        InstrumentTypeId = x.InstrumentTypeId,
                        Level = x.Level
                        
                    }).ToList()

                };
                _dbContext.Players.Add(player);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating player", ex);
            }
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            try
                {
                var player = _dbContext.Players.Find(id);
                if (player == null)
                {
                    return  await Task.FromResult(false);
                }
                _dbContext.Players.Remove(player);
                _dbContext.PlayerInstruments.RemoveRange(player.Instruments);
                 _dbContext.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting player", ex);
            }
        }

        public async Task<bool> UpdatePlayerAsync(int id, UpdatePlayerRequest playerRequest)
        {
           try
                {
                var player = _dbContext.Players.Find(id);
                if (player == null)
                {
                    return  await Task.FromResult(false);
                }
                player.NickName = playerRequest.NickName;
            

                _dbContext.Players.Update(player);
                _dbContext.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating player", ex);
            }
        }

       async Task<GetPlayerDetailResponse> IPlayerService.GetPlayerDetailAsync(int id)
        {
            try
            {
                var player = _dbContext.Players.Find(id);
                if (player == null)
                {
                    return await Task.FromResult<GetPlayerDetailResponse>(null);
                }
                return await Task.FromResult(new GetPlayerDetailResponse
                {
                    NickName = player.NickName,
                    JoinedDate = player.JoinedDate,
                    PlayerInstruments = player.Instruments.Select(x => new PlayerInstrument
                    {
                        InstrumentTypeId = x.InstrumentTypeId,
                        Level = x.Level
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting player detail", ex);
            }
        }
    }


}
