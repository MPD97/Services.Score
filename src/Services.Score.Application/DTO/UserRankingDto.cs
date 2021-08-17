using System;

namespace Services.Score.Application.DTO
{
    public class UserRankingDto
    {
        public Guid Id { get; set; }
        public int Place { get; set; }
    }
}