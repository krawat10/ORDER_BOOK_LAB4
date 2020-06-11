using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LAB4_150348.Seeders
{
    public interface IDbSeeder<in T> where T : DbContext
    {
        public Task<bool> Seed(T context);
    }
}