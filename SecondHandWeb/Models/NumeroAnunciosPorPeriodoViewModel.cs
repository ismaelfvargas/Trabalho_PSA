using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHandWeb.Models
{
    public class NumeroAnunciosPorPeriodoViewModel
    {
        public DateTime dtIni { get; set; }

        public DateTime dtFim { get; set; }

        public long numAnunciosPeriodo { get; set; }
    }
}
