using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChestInstaller : MonoInstaller
{
    [Space]
    [SerializeField] private ChestView[] chestViews;

    [Space]
    [SerializeField] private ChestConfig[] configs;


    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ChestPresenter>().FromMethodMultiple(this.CreatePresenters);
    }

    private IEnumerable<ChestPresenter> CreatePresenters(InjectContext context)
    {
        var chests = context.Container.Resolve<Chest[]>();
        Debug.Log("chests = " + chests.Length);

        if (chests.Length != chestViews.Length || chests.Length != configs.Length)
        {
            throw new ArgumentException("Different array lengths!");
        }

        for (int i = 0; i < chests.Length; i++)
        {
            var chest = chests[i];
            var view = this.chestViews[i];
            
            chest.SetupConfig(this.configs[i]);
            
            yield return new ChestPresenter(chest, view);
        }
    }
}