using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Utilities.BackroundJob
{
    public class RoomStatusBackround:BackgroundService
    {
        private readonly ILogger<RoomStatusBackround> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly AppDbContext _context;

        public RoomStatusBackround(ILogger<RoomStatusBackround> logger, IServiceScopeFactory serviceScopeFactory,AppDbContext context)
        {
           _logger = logger;
          _serviceScopeFactory = serviceScopeFactory;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Room status background service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Room status background service is running.");

                try
                {
                    // Dependency injection ile scope oluştur
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        // Odaların durumunu güncelle
                        //var rooms = await _context.Rooms.Include(x=>x.Rese).ToListAsync();
                        //foreach (var room in rooms)
                        //{
                        //    // Room durumunu güncelle
                        //    bool roomStatus = await CheckRoomStatusAsync(room);
                        //    room.Status = roomStatus;
                        //}

                        await dbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating room statuses.");
                }

                // Belirli bir süre bekleyin (örneğin 1 dakika)
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("Room status background service is stopping.");
        }
        private async Task<bool> CheckRoomStatusAsync(Room room)
        {
            //if (vm.DeparturDate > DateTime.Now && vm.ArrivalDate < DateTime.Now)
            //{
            //    room.Status = false;
            //}
            //else
            //{
            //    room.Status = true;
            //}

            return true; // Geçici olarak true döndürüldü, gerekli mantığı uygulamanız gerekiyor
        }
    }
}
