using BookStore.Models;
using BookStore.ViewModels;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IItemService
    {
        Task<bool> DeleteItem(ViewModelBase viewModel);
        Task<bool> SaveItem(ViewModelBase viewModel);
    }
}