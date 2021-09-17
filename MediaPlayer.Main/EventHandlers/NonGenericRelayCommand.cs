using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaPlayer
{
    public class NonGenericRelayCommand : ICommand
    {

        #region Private Members
        /// <summary>
        /// The action to run 
        /// </summary>
        private Action mAction;
        #endregion

        #region Public Events 
        /// <summary>
        /// The event that fires when csee cref='CanExecute(object)'/> value changes 
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// A relay command can always execute 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the commands action 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion

        #region Constructor 
        public NonGenericRelayCommand(Action action)
        {
            mAction = action;
        }
        #endregion
    }
}
