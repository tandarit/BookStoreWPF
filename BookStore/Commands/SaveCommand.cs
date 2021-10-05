using BookStore.Services;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Commands
{
    class SaveCommand : AsyncCommandBase
    {
        private readonly BookListViewModel _bookListViewModel;
        private readonly IItemService _saveItemService;

        public SaveCommand(BookListViewModel bookListViewModel, IItemService saveItemService)
        {
            _bookListViewModel = bookListViewModel;
            _saveItemService = saveItemService;
        }

        protected override async Task ExecuteAsync()
        {
            await _saveItemService.SaveItem(_bookListViewModel);
        }
    }
}
