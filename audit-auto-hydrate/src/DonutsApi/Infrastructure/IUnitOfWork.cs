using System;
using System.Threading.Tasks;
using DonutsApi.Application;
using Microsoft.EntityFrameworkCore;

namespace DonutsApi.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        DbSet<Donut> Donuts { get; }
        Task Complete();
    }
}
