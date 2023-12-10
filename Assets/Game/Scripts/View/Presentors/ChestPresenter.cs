using System;
using UnityEngine;
using Zenject;

public class ChestPresenter : IInitializable, IDisposable, ILateTickable
{
    private readonly Chest chest;
    private readonly ChestView view;

    public ChestPresenter(Chest chest, ChestView view)
    {
        this.chest = chest;
        this.view = view;
    }


    public void Initialize()
    {
        this.view.OnClicked += OnClicked;
        this.chest.OnTimeEnded += ShowButton;
    }

    public void Dispose()
    {
        this.view.OnClicked -= OnClicked;
        this.chest.OnTimeEnded -= ShowButton;
    }

    public void LateTick()
    {
        var totalSeconds = this.chest.GetRemainingTime();
        var roundedSeconds = Mathf.Ceil(totalSeconds);
        var timeSpan = TimeSpan.FromSeconds(roundedSeconds);
        var formattedTime =
            string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        this.view.UpdateTaimer(formattedTime);
    }

    private void OnClicked()
    {
        this.chest.ReceiveReward();
        this.view.SetActive(false);
    }

    private void ShowButton(Chest _) => this.view.SetActive(true);
}