namespace Book_subscription.Server.Infrastructure.unitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
    }
}
