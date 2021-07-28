namespace Services.Score.Core.Exceptions
{
    public class InvalidScoreMessageException : DomainException
    {
        public override string Code { get; } = "invalid_score_message";
        public string Message { get; }
        public InvalidScoreMessageException(string message) : 
            base($"Message: {message} is not a valid message. ")
        {
            Message = message;
        }
    }
}