using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Model {
    public class CurrenciesRepository : ICurrenciesRepository {
        public Dictionary<string, decimal> Accounts { get; set; }
        public Dictionary<string, decimal> Courses { get; set; }

        public CurrenciesRepository(Dictionary<string, decimal> accounts, Dictionary<string, decimal> courses) {
            Accounts = accounts;
            Courses = courses;
        }

        public decimal Convert(decimal summ, string pair) {
            return Courses.ContainsKey(pair) ?  summ * Courses[pair] : summ;
        }
    }
}
