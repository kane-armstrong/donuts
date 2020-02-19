using DonutsApi.Application;
using System;

namespace DonutsApi.Services
{
    public class RandomUserProfile : ICurrentUserProfile
    {
        private static readonly User John = new User
        {
            Id = Guid.Parse("26a42892-9eba-426a-822b-84c537c830cc"),
            Name = "John Doe"
        };

        private static readonly User Jane = new User
        {
            Id = Guid.Parse("6115f347-6af9-4bcf-b431-0eb920f54882"),
            Name = "Jane Doe"
        };

        private static readonly Random Seed = new Random();
        private const int Min = 1;
        private const int Max = 100;
        
        public Guid UserId
        {
            get
            {
                var number = Seed.Next(Min, Max);
                return number % 2 == 0 ? Jane.Id : John.Id;
            }
        }
    }
}
