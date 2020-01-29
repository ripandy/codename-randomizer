namespace Randomizer.UseCases
{
    public class RequestMessage : IRequestMessage
    {
        public RequestType RequestType { get; }

        public RequestMessage(RequestType requestType)
        {
            RequestType = requestType;
        }
    }

    public class RequestMessage<T> : RequestMessage
    {
        public T Value { get; }
        public RequestMessage(RequestType requestType, T value) : base(requestType)
        {
            Value = value;
        }
    }
}