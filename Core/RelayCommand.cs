using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaPlayer
{
    /// <summary>
    /// Handles all UI Commands
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Properties

        private Action<object> _execute;

        private Func<object, bool> _canExecute { get; set; }

        #endregion

        #region Public properties

        /// <summary>
        /// Indicates changes in execution
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize commands private properties
        /// </summary>
        /// <param name="execute">action to be execute</param>
        /// <param name="canExecute">run if action is to be executed</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        
        /// <summary>
        /// Indicates if an action can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Executes an action 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
