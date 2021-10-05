using BookStore.Models;
using BookStore.Services;
using BookStore.ViewModels;
using System;
using System.Threading.Tasks;

namespace BookStore.Commands
{
    public class DeleteCommand : AsyncCommandBase
    {
        private readonly BookListViewModel _bookListViewModel;
        private readonly IItemService _deleteItemService;

        public DeleteCommand(BookListViewModel bookListViewModel, IItemService deleteItemService)
        {
            _bookListViewModel = bookListViewModel;
            _deleteItemService = deleteItemService;
        }

        protected override async Task ExecuteAsync()
        {
            await _deleteItemService.DeleteItem(_bookListViewModel);
        }
    }
}
