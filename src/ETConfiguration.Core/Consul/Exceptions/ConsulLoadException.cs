namespace ETConfiguration.Core.Consul.Exceptions
{
    public sealed class ConsulLoadException
    {
        public ConsulLoadException(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; }
    }
}
