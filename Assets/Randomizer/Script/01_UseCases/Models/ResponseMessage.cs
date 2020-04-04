namespace Randomizer.UseCases
{
    public class ResponseMessage : IResponseMessage
    {
        public ResponseType ResponseType { get; }

        public ResponseMessage(ResponseType responseType)
        {
            ResponseType = responseType;
        }
    }
    
    public class ResponseMessage<T> : ResponseMessage
    {
        public T Value { get; }
        public ResponseMessage(ResponseType responseType, T value) : base(responseType)
        {
            Value = value;
        }
    }
}