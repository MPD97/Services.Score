using System;

namespace Services.Score.Core.Exceptions
{
    public class UserScoreCannotBeIncreasedException : DomainException
    {
        public override string Code { get; } = "user_score_cannot_be_increased";
        public Guid UserId { get; }

        public UserScoreCannotBeIncreasedException(Guid userId) 
            : base($"User score with id: {userId} cannot be increased.")
        {
            UserId = userId;
        }
    }
}