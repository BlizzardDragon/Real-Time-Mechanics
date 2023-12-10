using UnityEngine;

[CreateAssetMenu(fileName = "Reward", menuName = "ScriptableObjects/Configs/ResourceReward", order = 0)]
public class ResourceRewardConfig : RewardConfig
{
    [Header("Resource")]
    [SerializeField] private CurrencyType type;
    [SerializeField] private int value;

    [Header("Random")]
    [SerializeField] private RandomValueTypes randomType;
    [SerializeField] private float minMultiplier = 0.8f;
    [SerializeField] private float maxMultiplier = 1.2f;

    public CurrencyType Type => type;


    public void SetRandomValueType(RandomValueTypes randomType) => this.randomType = randomType;

    public int GetValue()
    {
        var newValue = 0;

        if (randomType == RandomValueTypes.RandomValue)
        {
            newValue = (int)(this.value * Random.Range(this.minMultiplier, this.maxMultiplier));
        }
        else if (randomType == RandomValueTypes.NotRandom)
        {
            newValue = this.value;
        }

        return newValue *= this.level;
    }
}