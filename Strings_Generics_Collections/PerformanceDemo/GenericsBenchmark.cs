using System.Collections;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace PerformanceDemo
{
    [InProcess]
    [MemoryDiagnoser]
    [KeepBenchmarkFiles]
    public class GenericsBenchmark
    {
        private List<int> _listForTesting;
        private List<int> _listForInserts;
        private ArrayList _arrayListForTesting;
        private ArrayList _arrayListForInserts;

        [IterationSetup]
        public void SetupMethod()
        {
            _listForTesting = new List<int>(Count);
            _listForInserts = new List<int>(Count);
            _arrayListForTesting = new ArrayList(Count);
            _arrayListForInserts = new ArrayList(Count);
        }

        [Params(1000, 2000000)]
        // ReSharper disable once UnassignedField.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public int Count;

        [Benchmark]
        public List<int> GenericIntCollection()
        {
            for (var n = 0; n < Count; n++)
            {
                _listForTesting.Add(n); // no boxing
                var x = _listForTesting[n]; // no unboxing either
                _listForInserts.Add(x); // no boxing
            }

            return _listForInserts;
        }

        [Benchmark]
        public ArrayList NonGenericIntCollection()
        {
            for (var n = 0; n < Count; n++)
            {
                _arrayListForTesting.Add(n); // boxing
                var x = _arrayListForTesting[n]; // unboxing
                _arrayListForInserts.Add(x); // boxing
            }

            return _arrayListForInserts;
        }
    }
}
