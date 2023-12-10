using System;

public interface IRealtimeTimer
{
    string ID { get; }
    ChestType Type { get; }
    event Action<IRealtimeTimer> OnStarted;

    void Synchronize(float offlineSeconds);
}
