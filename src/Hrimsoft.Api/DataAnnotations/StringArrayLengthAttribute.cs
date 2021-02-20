using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hrimsoft.Api
{
    /// <summary>
    /// Validates each array item that has length less or equal to some limit.
    /// </summary>
    public class StringArrayLengthAttribute : StringLengthAttribute
    {
        /// <summary>
        /// Validate that each array item has length less or equal to some limit.
        /// </summary>
        /// <param name="maximumLength">Maximum length of each array item</param>
        public StringArrayLengthAttribute(int maximumLength) : base(maximumLength)
        {
            SilentMode = true;
        }

        /// <summary>
        /// If set to true, will not put the field name into the validation error message 
        /// </summary>
        public bool SilentMode { get; set; }

        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            var array = value as IEnumerable<string>;
            if (array == null)
                return true;

            if (array.Any(item => !base.IsValid(item)))
                return false;

            return true;
        }

        private const string MAX_VIOLATION_MESSAGE = "The field{0} must contain strings with a maximum length of {1}.";
        private const string MIN_VIOLATION_MESSAGE = "The field{0} must contain strings with a minimum length of {1}.";
        private const string MIN_MAX_VIOLATION_MESSAGE = "The field{0} must contain strings with a minimum length of {1} and maximum length of {2}.";

        /// <inheritdoc />
        public override string FormatErrorMessage(string name)
        {
            var fieldName = SilentMode ? "" : " " + name;
            
            if (MinimumLength != 0 && MaximumLength == 0)
                return string.Format(MIN_VIOLATION_MESSAGE, fieldName, MinimumLength);
            if (MaximumLength != 0 && MinimumLength == 0)
                return string.Format(MAX_VIOLATION_MESSAGE, fieldName, MaximumLength);

            return string.Format(MIN_MAX_VIOLATION_MESSAGE, fieldName, MinimumLength, MaximumLength);
        }
    }
}