using DG.Tweening;
using UnityEngine;

public class Attachable : MonoBehaviour
{
    private void Start()
    {
        this.transform.DOScale(transform.localScale * .9f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
