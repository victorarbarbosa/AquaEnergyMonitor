using Blazored.LocalStorage;

namespace AquaEnergyMonitor.Services
{
    public class SessionService
    {
        private readonly ILocalStorageService _localStorage;

        public SessionService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SetUserId(Guid userId)
        {
            await _localStorage.SetItemAsync("userId", userId.ToString());
        }

        public async Task<Guid?> GetUserId()
        {
            var userId = await _localStorage.GetItemAsync<string>("userId");
            return Guid.TryParse(userId, out var result) ? result : (Guid?)null;
        }

        public async Task ClearUserId()
        {
            await _localStorage.RemoveItemAsync("userId");
        }
    }
}
