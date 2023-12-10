using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class ViewCurrency : MonoBehaviour
{
    [SerializeField] private CurrencyType type;
    [SerializeField] private TMP_Text addedValue;
    [SerializeField] private TMP_Text currency;
    
    private AudioManager audioManager;
    private Tween tween;

    public CurrencyType Type => type;
    

    [Inject]
    public void Construct(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }

    public void SetValue(string value) => this.currency.text = value;
    public void PlaySFX() => this.audioManager.PlayReward();

    public void PlayAddAnimation(string value)
    {
        this.addedValue.text = "+" + value;

        this.tween.Kill();
        this.addedValue.alpha = 1;
        this.tween = this.addedValue.DOFade(0, 2);
    }
}