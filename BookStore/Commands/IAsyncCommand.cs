using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookStore.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
}
