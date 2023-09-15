using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Contracts.Core;

namespace TestProject.HttpApi.Core
{
    public class BenchmarkRunController : CoreController, IBenchmarkRun
    {
        private readonly IBenchmarkRun _benchmarkRun;
        public BenchmarkRunController(IBenchmarkRun benchmarkRun)
        {
            _benchmarkRun = benchmarkRun;
        }
        /// <summary>
        /// Trigger BL specified benchmarks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool RunBenchmark()
            => _benchmarkRun.RunBenchmark();

    }
}
