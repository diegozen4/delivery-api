using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}
