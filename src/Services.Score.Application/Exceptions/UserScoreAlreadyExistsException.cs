using System;

namespace Services.Score.Application.Exceptions
{
    public class UserScoreAlreadyExistsException : AppException
    {
        public override string Code { get; } = "user_score_already_exists";
        public Guid UserId { get; }

        public UserScoreAlreadyExistsException(Guid userId) 
            : base($"User score with id: {userId} already exists.")
        {
            UserId = userId;
        }
    }
}