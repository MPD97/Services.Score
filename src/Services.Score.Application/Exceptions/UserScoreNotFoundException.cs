using System;

namespace Services.Score.Application.Exceptions
{
    public class UserScoreNotFoundException : AppException
    {
        public override string Code { get; } = "user_score_not_found";
        public Guid UserId { get; }

        public UserScoreNotFoundException(Guid userId) 
            : base($"User score with id: {userId} was not found.")
        {
            UserId = userId;
        }
    }
}