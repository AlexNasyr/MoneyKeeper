using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Model {
    public interface ICurrenciesRepository {
        public Dictionary<string, decimal> Accounts { get; set; }
        public Dictionary<string, decimal> Courses { get; set; }
        decimal Convert(decimal summ, string pair);
    }
}
