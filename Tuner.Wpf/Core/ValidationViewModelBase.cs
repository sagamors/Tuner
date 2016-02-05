using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Controls;
using System.Xml.Schema;

namespace Tuner.Wpf.Core
{
    public abstract class ValidationViewModelBase<TView> : ViewModelBase<TView>, INotifyDataErrorInfo where TView : IView
    {
        Dictionary<string, List<string>> propErrors = new Dictionary<string, List<string>>();

        public ValidationViewModelBase(IKernel kernel) : base(kernel)
        {

        }

        public bool HasErrors
        {
            get
            {
                try
                {
                    var propErrorsCount = propErrors.Values.FirstOrDefault(r => r.Count > 0);
                    if (propErrorsCount != null)
                        return true;
                    else
                        return false;
                }
                catch { }
                return true;
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errors = new List<string>();
            if (propertyName != null)
            {
                propErrors.TryGetValue(propertyName, out errors);
                return errors;
            }
            else
                return null;
        }
    }


}
