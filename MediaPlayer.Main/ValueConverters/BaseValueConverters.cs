using System;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Data;
using System.Linq;

namespace MediaPlayer
{
    public abstract class BaseValueConverters<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        /// <summary>
        /// Converter instance 
        /// </summary>
        private static T mConverter = null;

        /// <summary>
        /// Provides a static instance of the value converter
        /// Singleton implementation 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new T());
        }

        /// <summary>
        /// Convert enum type of page 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Converts back a page to enum 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
