using System;

namespace DonutsApi.Services
{
    public interface ICurrentUserProfile
    {
        Guid UserId { get; }
    }
}
