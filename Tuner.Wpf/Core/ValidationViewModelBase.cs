using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Tuner.Wpf.Core
{
    public abstract class ValidationViewModelBase<TView> : ViewModelBase<TView>, IValidationViewModel, INotifyDataErrorInfo where TView : IView
    {
   
        public ValidationViewModelBase(IKernel kernel) : base(kernel)
        {
         
        }

        private PropertyDescriptorCollection _properties;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private object _lock = new object();
        public bool HasErrors { get { return _errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0); } }
        public bool IsValid { get { return !this.HasErrors; } }

        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (_errors.ContainsKey(propertyName) && (_errors[propertyName] != null) && _errors[propertyName].Count > 0)
                    return _errors[propertyName].ToList();
                else
                    return null;
            }
            else
                return _errors.SelectMany(err => err.Value.ToList());
        }

        public void OnErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            lock (_lock)
            {
                var validationContext = new ValidationContext(this, null, null);
                validationContext.MemberName = propertyName;
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateProperty(value, validationContext, validationResults);

                //clear previous _errors from tested property  
                if (_errors.ContainsKey(propertyName))
                    _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
                HandleValidationResults(validationResults);
            }
        }

        public void ValidateProperty([CallerMemberName] string propertyName = null)
        {
            if(_properties==null)
                _properties = TypeDescriptor.GetProperties(this);
            var property = _properties.Find(propertyName, true);
            ValidateProperty(property.GetValue(this), propertyName);
        }

        public bool Validate()
        {
            lock (_lock)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);
                //clear all previous _errors  
                var propNames = _errors.Keys.ToList();
                _errors.Clear();
                propNames.ForEach(pn => OnErrorsChanged(pn));
                HandleValidationResults(validationResults);
            }
            return IsValid;
        }

        private void HandleValidationResults(List<ValidationResult> validationResults)
        {
            //Group validation results by property names  
            var resultsByPropNames = from res in validationResults
                                     from mname in res.MemberNames
                                     group res by mname into g
                                     select g;
            //add _errors to dictionary and inform binding engine about _errors  
            foreach (var prop in resultsByPropNames)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();
                _errors.Add(prop.Key, messages);
                OnErrorsChanged(prop.Key);
            }

            SetHasErrors();
        }

        public override void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            ValidateProperty(propertyName);
            base.OnPropertyChanged(propertyName);
        }

        private void SetHasErrors()
        {
            //HasErrors =_errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0);
        }
    }
}
