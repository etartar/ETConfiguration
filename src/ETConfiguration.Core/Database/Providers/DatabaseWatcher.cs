using Microsoft.Extensions.Primitives;

namespace ETConfiguration.Core.Database.Providers
{
    public class DatabaseWatcher : IDisposable
    {
        private readonly TimeSpan _refreshInterval;
        private CancellationTokenSource _cancellationTokenSource;

        public DatabaseWatcher(TimeSpan refreshInterval)
        {
            _refreshInterval = refreshInterval;
        }

        public IChangeToken Watch()
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource(_refreshInterval);
            return new CancellationChangeToken(_cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}
