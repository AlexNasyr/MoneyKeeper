using MoneyKeeper;
using MoneyKeeper.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace MKeeper.Tests {
    public class Tests {
        CurrenciesService currencyServise;
        ICurrenciesRepository currencyRepository;
        [SetUp]
        public void Setup() {
            Dictionary<string, decimal> accounts = new Dictionary<string, decimal>() { { "usd", 500m }, { "rur", 10000m }, };
            Dictionary<string, decimal> cources = new Dictionary<string, decimal>() { { "usdrur", 70m }, { "rurusd", 1 / 70m }, };
            currencyRepository = new CurrenciesRepository(accounts, cources);
            currencyServise = new CurrenciesService(currencyRepository);
        }

        [Test]
        public void AccAddCurrency() {
            currencyServise.AccAdd("usd", 100);
            Assert.AreEqual(600, currencyServise.GetAccAmount("usd"));
        }
        [Test]
        public void AccSubstCurrency() {
            currencyServise.AccSubstract("usd", 100);
            Assert.AreEqual(400, currencyServise.GetAccAmount("usd"));
        }
        [Test]
        public void AccConvertCurrency() {
            currencyServise.AccChangeCurrency("usd", "rur", 100);
            Assert.AreEqual(500m-100m/70, currencyServise.GetAccAmount("usd"));
        }
        [Test]
        public void ConvertCurrency() {
            Assert.AreEqual(7000, currencyServise.ChangeCurrency("usd", "rur", 100));
        }
    }
}