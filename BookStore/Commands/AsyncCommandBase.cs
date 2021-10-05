using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookStore.Commands
{
    public abstract class AsyncCommandBase : ICommand
    { 

        private bool _isExecuting;
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged = delegate { };
        
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return !IsExecuting;
        }

        public async void Execute(object parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync();
            }
            catch (Exception ex)
            {
                //ToDo exception logging
            }

            IsExecuting = false;
        }

        protected abstract Task ExecuteAsync();
    }
}
