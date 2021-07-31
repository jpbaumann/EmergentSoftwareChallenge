using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EmergentSoftwareChallenge.Infrastructure
{
    public class VersionComparer : Comparer<string>, IVersionComparer
    {
        private static readonly string _separator = ".";
        private static readonly string _defaultValue = "0";

        public override int Compare([AllowNull] string x, [AllowNull] string y)
        {
            var xIsEmpty = string.IsNullOrWhiteSpace(x);
            var yIsEmpty = string.IsNullOrWhiteSpace(y);
            if (xIsEmpty && yIsEmpty)
                return 0;
            if (xIsEmpty)
                return -1;
            if (yIsEmpty)
                return 1;
            var xValues = x.Split(_separator, System.StringSplitOptions.TrimEntries);
            var yValues = y.Split(_separator, System.StringSplitOptions.TrimEntries);
            for (int i = 0; i <= 2; i++)
            {
                var xIn = xValues.Length > i ? SanitizeString(xValues[i]) : _defaultValue;
                var yIn = yValues.Length > i ? SanitizeString(yValues[i]) : _defaultValue;
                var xSuccess = int.TryParse(xIn, out var xValue);
                var ySuccess = int.TryParse(yIn, out var yValue);
                if (!xSuccess && !ySuccess)
                    return 0;
                if (!xSuccess)
                    return 1;
                if (!ySuccess)
                    return -1;
                if (xValue != yValue)
                    return xValue - yValue;
            }
            return 0;
        }

        private static string SanitizeString(string value) => string.IsNullOrWhiteSpace(value) ? _defaultValue : value;
    }
}
