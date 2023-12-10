using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    [SerializeField] private TMP_Text timer;
    [SerializeField] private Button button;

    public event UnityAction OnClicked
    {
        add { this.button.onClick.AddListener(value); }
        remove { this.button.onClick.RemoveListener(value); }
    }

    public void UpdateTaimer(string value) => this.timer.text = value;
    public void SetActive(bool value) => this.button.gameObject.SetActive(value);
}