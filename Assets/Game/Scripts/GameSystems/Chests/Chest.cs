using System;
using Elementary;
using FrameworkUnity.OOP.Interfaces.Listeners;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public sealed class Chest : MonoBehaviour, IRealtimeTimer, IStartGameListener, IDisposable
{
    [ShowInInspector, ReadOnly]
    private Countdown timer;

    private ChestConfig config;
    private RewardFactory factory;

    public string ID => config.Name;
    public ChestType Type => config.Type;

    public event Action<IRealtimeTimer> OnStarted;
    public event Action<Chest> OnTimeEnded;


    [Inject]
    public void Construct(RewardFactory factory)
    {
        this.factory = factory;
        this.timer = new(this.config.Duration);
        this.timer.OnEnded += CallTimeEndEvent;
    }

    public void OnStartGame()
    {
        if (this.timer.Progress <= 0)
        {
            this.OnStarted?.Invoke(this);
        }

        this.timer.Play();
    }

    public void Dispose() => this.timer.OnEnded -= CallTimeEndEvent;

    public void SetupConfig(ChestConfig config) => this.config = config;
    public void Synchronize(float offlineSeconds) => this.timer.RemainingTime -= offlineSeconds;
    public bool CanReceiveReward() => this.timer.Progress >= 1;
    public float GetRemainingTime() => this.timer.RemainingTime;
    public void CallTimeEndEvent() => this.OnTimeEnded?.Invoke(this);

    [Button]
    public void ReceiveReward()
    {
        if (!CanReceiveReward())
        {
            Debug.Log("Can't receive reward!");
        }

        this.timer.ResetTime();
        this.timer.Play();

        this.OnStarted?.Invoke(this);

        var rewardConfig = this.config.GetRewardConfig();
        var reward = this.factory.CreateReward(rewardConfig);
        reward.Apply();
    }
}
