using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BladeButton : MonoBehaviour
{
    public GameObject Blade;
    public BladeCutter BladeCutter;

    public float BladeMoveDy = -0.8f;

    private bool _ready;
    private bool _cutted;

    public void Start()
    {
        _ready = true;
        _cutted = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!_ready) return;

        _cutted = false;
        _ready = false;

        var posA = Blade.transform.position + new Vector3(0, BladeMoveDy, 0f);
        var posB = Blade.transform.position;

        var sequence = DOTween.Sequence();
        sequence.Append(Blade.transform.DOMove(posA, 1f));
        sequence.Append(Blade.transform.DOMove(posB, 1f));
        sequence.InsertCallback(1f, OnBlade);
        sequence.OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        _ready = true;
    }

    public void OnBlade()
    {
        if (_cutted) return;

        _cutted = true;
        BladeCutter.Cut();
    }
}
