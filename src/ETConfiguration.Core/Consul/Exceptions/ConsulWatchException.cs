namespace ETConfiguration.Core.Consul.Exceptions
{
    public sealed class ConsulWatchException
    {
        public ConsulWatchException(Exception exception, int failureCount)
        {
            Exception = exception;
            FailureCount = failureCount;
        }

        public Exception Exception { get; }

        public int FailureCount { get; }
    }
}
