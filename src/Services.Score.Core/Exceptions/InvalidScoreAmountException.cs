namespace Services.Score.Core.Exceptions
{
    public class InvalidScoreAmountException : DomainException
    {
        public override string Code { get; } = "invalid_score_amount";
        
        public int Score { get; }
        public int MinimumScore { get; }
        public int MaximumScore { get; }

        public InvalidScoreAmountException(int score, int minimumScore, int maximumScore) : 
            base($"Score: {score} is not a valid score amount. " +
                 $"Score must be between: {minimumScore} and {maximumScore}.")
        {
            Score = score;
            MinimumScore = minimumScore;
            MaximumScore = maximumScore;
        }
    }
}