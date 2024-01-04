using System;
using System.Collections.Generic;

namespace ValidationBuilder
{
    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Validator<T>
    {
        private readonly List<Func<T, bool>> _predicates = new List<Func<T, bool>>();
        private readonly List<Func<T, bool>> _customValidationMethods = new List<Func<T, bool>>();
        private readonly List<ValidationError> _validationErrors = new List<ValidationError>();

        public Validator<T> AddRule(Func<T, bool> predicate, string errorMessage)
        {
            _predicates.Add(predicate);
            return this;
        }

        public Validator<T> AddCustomValidation(Func<T, bool> customValidationMethod, string errorMessage)
        {
            _customValidationMethods.Add(customValidationMethod);
            return this;
        }

        public Validator<T> MustBeTrue(Func<T, bool> condition, string errorMessage)
        {
            return AddRule(condition, errorMessage);
        }

        public Validator<T> NotNull(string errorMessage)
        {
            return AddRule(value => value != null, errorMessage);
        }

        public Validator<T> LessThanOrEqual(Func<T, IComparable> propertySelector, IComparable value, string errorMessage)
        {
            return AddRule(item => propertySelector(item).CompareTo(value) <= 0, errorMessage);
        }

        public List<ValidationError> Validate(T value)
        {
            _validationErrors.Clear();

            foreach (var predicate in _predicates)
            {
                if (!predicate(value))
                {
                    _validationErrors.Add(new ValidationError { PropertyName = "N/A", ErrorMessage = "N/A" });
                }
            }

            foreach (var customValidationMethod in _customValidationMethods)
            {
                if (!customValidationMethod(value))
                {
                    _validationErrors.Add(new ValidationError { PropertyName = "N/A", ErrorMessage = "N/A" });
                }
            }

            return _validationErrors;
        }
    }
}
