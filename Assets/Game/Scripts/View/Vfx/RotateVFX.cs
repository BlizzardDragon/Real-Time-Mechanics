using DG.Tweening;
using UnityEngine;

public class RotateVFX : MonoBehaviour
{
    private void Start()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 3, 0, 1).SetLoops(-1);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, -10) * Time.deltaTime);
    }
}
