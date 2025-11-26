using System.Diagnostics;
using System.Runtime.Versioning;
using PDV.Application.DTOs;
using PDV.Application.Services.Interfaces;

namespace PDV.Application.Services.Implementations
{
    public class Life : ILife
    {
        private readonly PerformanceCounter _cpuCounter;

        public Life()
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            // 3. A primeira leitura é descartada (geralmente vem 0)
            _cpuCounter.NextValue();
        }

        [SupportedOSPlatform("windows")]
        public Task<double> CpuLoad()
        {
            // 4. Pega o valor atual calculado desde a última chamada
            float valorAtual = _cpuCounter.NextValue();

            // 5. Arredonda
            double valorArredondado = Math.Round(valorAtual, 2);

            // 6. Retorna como Task (já que seu método pede Task<double>)
            return Task.FromResult(valorArredondado);
        }
    }
}
