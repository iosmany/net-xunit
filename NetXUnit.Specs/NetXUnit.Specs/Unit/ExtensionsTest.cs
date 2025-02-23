using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ErrorOr;
using NetXUnit.Webapi;

namespace NetXUnit.Specs.Unit
{
    public class ExtensionsTest
    {
#if RELEASE
        [Fact]
        public void BenchmarkToMultipleVersions()
        {
            var summary = BenchmarkRunner.Run<BenchMarkToMultipleExtensionMethod>();
        }
#endif
    }

    [MemoryDiagnoser]
    public class BenchMarkToMultipleExtensionMethod
    {
        readonly Error error = Error.Failure();

        [Benchmark]
        public List<Error> ToMultipleV1()
        {
            return error.ToMultiple();
        }

        [Benchmark]
        public List<Error> ToMultipleV2()
        {
            return error.ToMultiple();
        }
    }
}
