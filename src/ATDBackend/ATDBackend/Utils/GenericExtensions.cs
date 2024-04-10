namespace ATDBackend.Utils
{
    public static class GenericExtensions
    {
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
        public static bool IsNullOrEmptyWithTrim(this string value) => string.IsNullOrEmpty(value.Trim());
    }
}
