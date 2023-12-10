using System;
using FrameworkUnity.OOP.Interfaces.Listeners;
using Zenject;

public class CurrencyStoragePresenter : IStartGameListener, IDisposable
{
    private CurrencyStorage currencyStorage;
    private ViewCurrency[] viewCurrencies;

    private const string DEFAULT_VALUE = "0";


    [Inject]
    public void Construct(CurrencyStorage currencyStorage, ViewCurrency[] viewCurrencies)
    {
        this.currencyStorage = currencyStorage;
        this.viewCurrencies = viewCurrencies;
    }

    public void OnStartGame()
    {
        SetDefaultValues();
        this.currencyStorage.OnCurrencyChanged += UpdateViewCurrency;
    }

    public void Dispose() => this.currencyStorage.OnCurrencyChanged -= UpdateViewCurrency;

    private void SetDefaultValues()
    {
        foreach (var viewCurrency in viewCurrencies)
        {
            viewCurrency.SetValue(DEFAULT_VALUE);
        }
    }

    private void UpdateViewCurrency(CurrencyStorageData data)
    {
        CurrencyType type = data.Type;
        int totalValue = data.TotalValue;
        int addedValue = data.AddedValue;

        foreach (var viewCurrency in this.viewCurrencies)
        {
            if (viewCurrency.Type == type)
            {
                viewCurrency.SetValue(totalValue.ToString());

                if (addedValue != 0)
                {
                    viewCurrency.PlayAddAnimation(addedValue.ToString());
                    viewCurrency.PlaySFX();
                }
            }
        }
    }
}