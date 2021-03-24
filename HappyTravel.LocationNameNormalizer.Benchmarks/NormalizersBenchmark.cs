using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using HappyTravel.LocationNameNormalizer;

namespace HappyTravel.LocationNameNormalizer.Benchmarks
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [RPlotExporter, AllStatisticsColumn, MemoryDiagnoser]
    public class NormalizersBenchmark
    {
        [GlobalSetup]
        public void Setup()
        {
            var retriever = new FileLocationNameRetriever();
            _nameNormalizer = new DefaultLocationNameNormalizer(retriever);
            _nameNormalizer.Init();
        }


        /*[Benchmark(Baseline = true)]
        [Arguments("")]
        [Arguments("Aafg fsaw grdgdhjgkj")]
        [Arguments("Great Britain")]
        [Arguments("THE UNITED KINGDOM")]
        public string DictionaryBased(string name) => _nameNormalizer.GetNormalizedCountryName(name);


        [Benchmark]
        [Arguments("")]
        [Arguments("Aafg fsaw grdgdhjgkj")]
        [Arguments("Great Britain")]
        [Arguments("THE UNITED KINGDOM")]
        public string ListBased(string name) => _nameNormalizer.GetNormalizedCountryNameFromList(name);*/


        private DefaultLocationNameNormalizer _nameNormalizer;
    }
}
