using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace MediaPlayer
{
    public static class ViewsFactory
    {
        public static IShellViewModel GetShellViewModelInstance(string path)
        {
            return new ShellViewModel(path);
        }
    }
}
