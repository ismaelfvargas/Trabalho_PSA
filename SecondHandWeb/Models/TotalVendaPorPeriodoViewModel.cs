using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHandWeb.Models
{
    public class TotalVendaPorPeriodoViewModel
    {
        public DateTime dtIni { get; set; }

        public DateTime dtFim { get; set; }

        public int numVendasPeriodo { get; set; }

        public decimal valorVendasPeriodo { get; set; }
    }
}
