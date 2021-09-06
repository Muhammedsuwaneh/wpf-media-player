using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MediaPlayer
{
    /// <summary>
    /// Base view page 
    /// Returns an instance of each page 
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public class BaseView<VM> : Page
        where VM : ObservableObject, new()
    {
        private VM mViewModel;

        /// <summary>
        /// Page view instance 
        /// </summary>
        private VM ViewModel
        {
            get { return mViewModel; }
            set
            {
                if (mViewModel == value) return;

                mViewModel = value;

                this.DataContext = mViewModel;
            }
        }

        /// <summary>
        /// Constructor 
        /// Sets data context of current view 
        /// </summary>
        public BaseView()
        {
            this.DataContext = new VM();
        }
    }
}
