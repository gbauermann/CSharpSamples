using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Models
{
    public class LogEntry
    {
        public int CodigoErro { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataErro { get; set; } = DateTime.UtcNow;
    }
}
