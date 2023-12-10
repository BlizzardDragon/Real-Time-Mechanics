using System;
using System.Collections.Generic;

public sealed class CurrencyStorage
{
    private Dictionary<CurrencyType, int> currencies = new();
    public event Action<CurrencyStorageData> OnCurrencyChanged;


    public Dictionary<CurrencyType, int> GetCurrenciesValue() => this.currencies;
    public int GetCurrencyValue(CurrencyType type) => this.currencies[type];

    internal void AddResource(CurrencyType resourceType, int value)
    {
        if (this.currencies.ContainsKey(resourceType))
        {
            this.currencies[resourceType] += value;
        }
        else
        {
            this.currencies[resourceType] = value;
        }

        var currencyData = new CurrencyStorageData(resourceType, this.currencies[resourceType], value);
        this.OnCurrencyChanged?.Invoke(currencyData);
    }
}
