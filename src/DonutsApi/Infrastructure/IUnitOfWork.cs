using System;
using System.Threading.Tasks;
using DonutsApi.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DonutsApi.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        DbSet<Donut> Donuts { get; }
        Task Complete();
        EntityEntry<Donut> GetEntry(Donut donut);
    }
}
