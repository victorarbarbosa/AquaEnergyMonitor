namespace AquaEnergyMonitor.Services
{
    public class SessionService
    {
        public Guid? UserId { get; private set; }

        public void SetUserId(Guid id) => UserId = id;
        public void ClearUserId() => UserId = null;
    }
}
