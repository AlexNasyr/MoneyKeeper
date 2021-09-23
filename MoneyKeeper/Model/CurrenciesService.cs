using System;
using System.Collections.Generic;
using MoneyKeeper.Model;

namespace MoneyKeeper {
    public class CurrenciesService {
        private ICurrenciesRepository repository;
        public CurrenciesService(ICurrenciesRepository currenciesRepository) {
            repository = currenciesRepository;
        }
                
        public void AccAdd(string currency, decimal amount) {
            if (!repository.Accounts.ContainsKey(currency.ToLower())) { 
                repository.Accounts.Add(currency.ToLower(), 0);
            }
            repository.Accounts[currency.ToLower()] += amount;
        }
        public void AccSubstract(string currency, decimal amount) {
            if (!repository.Accounts.ContainsKey(currency.ToLower()) || repository.Accounts[currency.ToLower()] < amount) {
                throw new ArgumentException($"currency account {currency} not exist or insufficient funds");
            }
            repository.Accounts[currency.ToLower()] -= amount;
        }
        public void AccChangeCurrency(string currencyFrom, string currencyTo, decimal amountTo) {
            string currencyPair = $"{currencyTo.ToLower()}{currencyFrom.ToLower()}";
            if (!repository.Courses.ContainsKey(currencyPair)) {
                throw new ArgumentException("exchange cource not found");
            }
            if (!repository.Accounts.ContainsKey(currencyFrom) || repository.Accounts[currencyFrom] < repository.Convert(amountTo, currencyPair)) {
                throw new ArgumentException($"currency account {currencyFrom} not exist or insufficient funds");
            }

            decimal amountFrom = repository.Convert(amountTo, currencyPair);
            repository.Accounts[currencyFrom] -= amountFrom;
            repository.Accounts[currencyTo] += amountTo;
        }
        public decimal ChangeCurrency(string currencyFrom, string currencyTo, decimal amountTo) {
            string currencyPair = $"{currencyFrom.ToLower()}{currencyTo.ToLower()}";
            if (!repository.Courses.ContainsKey(currencyPair)) {
                throw new ArgumentException("currensy exchange cource not found");
            }
            return repository.Convert(amountTo, currencyPair);
        }
        public decimal GetAccAmount(string currency) {
            if (!repository.Accounts.ContainsKey(currency)) {
                throw new ArgumentException("account not exist");
            }
            return repository.Accounts[currency];
        }
    }


}
