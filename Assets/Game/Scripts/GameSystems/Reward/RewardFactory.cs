using System;
using Zenject;

public class RewardFactory
{
    private readonly DiContainer container;

    public RewardFactory(DiContainer container)
    {
        this.container = container;
    }

    internal IReward CreateReward(RewardConfig rewardConfig)
    {
        if (rewardConfig is ResourceRewardConfig)
        {
            ResourceRewardConfig resourceRewardConfig = (ResourceRewardConfig)rewardConfig;
            var value = resourceRewardConfig.GetValue();
            var currencyStorage = this.container.Resolve<CurrencyStorage>();
            return new ResourceReward(resourceRewardConfig.Type, value, currencyStorage);
        }
        
        throw new ArgumentException($"Not correct type: {rewardConfig.GetType()}!");
    }
}