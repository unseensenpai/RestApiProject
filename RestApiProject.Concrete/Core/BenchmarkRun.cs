using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Contracts.Core;

namespace TestProject.BL.Core
{
    public class BenchmarkRun : IBenchmarkRun
    {
        public bool RunBenchmark()
        {
            var results = BenchmarkRunner.Run<BenchTests>();
            return results is not null;
        }
    }

    [MemoryDiagnoser]
    public class BenchTests
    {
        private readonly List<string> Names = new()
        {
            "selman",
            "ahmet",
            "mehmet",
            "said"
        };

        private readonly XPCollection<string> NamesCollection = new XPCollection<string>()
        {
            "selman",
            "ahmet",
            "mehmet",
            "said"
        };

        [Benchmark]
        public void TestIEnumerable()
        {
            foreach (var name in Names)
            {
                Console.WriteLine(name);
            }
        }

        [Benchmark]
        public void TestIQuerayable()
        {
            foreach (var name in Names.AsQueryable())
            {
                Console.WriteLine(name);
            }
        }

        [Benchmark]
        public void TestICollection()
        {
            foreach (var name in NamesCollection)
            {
                Console.WriteLine(name);

            }
        }
    }
}
