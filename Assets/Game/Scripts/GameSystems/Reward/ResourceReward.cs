public class ResourceReward : IReward
{
    private readonly CurrencyType resourceType;
    private readonly int value;
    private readonly CurrencyStorage currencyStorage;

    public ResourceReward(CurrencyType resourceType, int value, CurrencyStorage currencyStorage)
    {
        this.resourceType = resourceType;
        this.value = value;
        this.currencyStorage = currencyStorage;
    }

    public void Apply() => this.currencyStorage.AddResource(this.resourceType, this.value);
}