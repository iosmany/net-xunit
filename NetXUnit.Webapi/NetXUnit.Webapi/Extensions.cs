using ErrorOr;

namespace NetXUnit.Webapi
{
    public static class Extensions
    {
        public static List<Error> ToMultiple(this Error error)
            => Enumerable.Empty<Error>()
            .Prepend(error)
            .ToList();

        public static List<Error> ToMultipleV2(this Error error)
            => new List<Error>() { error };

    }
}
