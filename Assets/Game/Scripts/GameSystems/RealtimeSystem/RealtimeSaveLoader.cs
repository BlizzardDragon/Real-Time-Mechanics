using System;
using System.Collections.Generic;
using System.Globalization;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;

public sealed class RealtimeSaveLoader : IInitGameListener, IDisposable
{
    private readonly HashSet<IRealtimeTimer> timers = new();
    private const string KEY_PREFIX = "GameTime/";


    [Inject]
    public void Construct(IRealtimeTimer[] timers)
    {
        foreach (var timer in timers)
        {
            RegisterTimer(timer);
        }
    }

    public void RegisterTimer(IRealtimeTimer timer)
    {
        if (this.timers.Add(timer))
        {
            timer.OnStarted += SaveTime;
        }
    }

    public void UnregisterTimer(IRealtimeTimer timer)
    {
        if (this.timers.Remove(timer))
        {
            timer.OnStarted -= SaveTime;
        }
    }

    public void OnInitGame()
    {
        var now = DateTime.Now;
        var culture = CultureInfo.InvariantCulture;

        foreach (var timer in this.timers)
        {
            SynchronizeTime(timer, now, culture);
        }
    }

    public void Dispose()
    {
        foreach (var timer in this.timers)
        {
            timer.OnStarted -= SaveTime;
        }
    }

    private void SynchronizeTime(IRealtimeTimer synchronizeble, DateTime now, CultureInfo culture)
    {
        if (!PlayerPrefs.HasKey(KEY_PREFIX + synchronizeble.ID)) return;

        string serializedTime = PlayerPrefs.GetString(KEY_PREFIX + synchronizeble.ID);
        DateTime previousTime = DateTime.Parse(serializedTime, culture);

        TimeSpan timeSpan = now - previousTime;
        var offlineSeconds = timeSpan.TotalSeconds;
        synchronizeble.Synchronize((float)offlineSeconds);

        Debug.Log($"Offline seconds for {synchronizeble.ID} = {offlineSeconds}");
    }

    private void SaveTime(IRealtimeTimer synchronizeble)
    {
        DateTime currentTime = DateTime.Now;
        string serializedTime = currentTime.ToString(CultureInfo.InvariantCulture);
        PlayerPrefs.SetString(KEY_PREFIX + synchronizeble.ID, serializedTime);

        Debug.Log($"Save timer {synchronizeble.ID}");
    }
}