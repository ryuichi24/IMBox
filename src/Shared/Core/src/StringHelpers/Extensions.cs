using System;

namespace IMBox.Core.StringHelpers
{
    public static class Extensions
    {
        public static Guid ToGuid(this string stringValue)
        {
            var isSuccess = Guid.TryParse(stringValue, out Guid parsed);

            if (!isSuccess) throw new ArgumentException($"The format is invalid: {stringValue}");

            return parsed;
        }
    }
}