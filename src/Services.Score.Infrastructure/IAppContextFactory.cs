using Services.Score.Application;

namespace Services.Score.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}