public struct CurrencyStorageData
{
    public CurrencyType Type { get; set; }
    public int TotalValue { get; set; }
    public int AddedValue { get; set; }

    public CurrencyStorageData(CurrencyType resourceType, int totalValue, int addedValue)
    {
        this.Type = resourceType;
        this.TotalValue = totalValue;
        this.AddedValue = addedValue;
    }
}