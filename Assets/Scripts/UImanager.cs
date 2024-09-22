using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    GameObject chestLid;

    // Start is called before the first frame update
    void Start()
    {
        Sequence chestSequence = DOTween.Sequence();
        //first animation = move the chest Y axis 5 units, Rotation & duration of 0.25 seconds
        chestSequence.Append(transform.DOMoveY(4f, 0.50f)).SetEase(Ease.InCirc); ;
        chestSequence.Join(transform.DORotate(Vector3.up * 180, 0.25f));
        //second animation = returns to og position & rotation within 0.50 seconds
        chestSequence.Append(transform.DOMoveY(0f, 0.50f)).SetEase(Ease.InCirc);
        chestSequence.Join(transform.DORotate(Vector3.up * 360, 0.25f));
        //third animation = shake (duration, strength, vibrato, randomness) & scaling
        chestSequence.Append(transform.DOShakeRotation(3f, 25f, 30, 10f));
        chestSequence.Join(transform.DOScaleY(transform.localScale.y * 3f, 0.50f));
        chestSequence.Join(transform.DOScaleX(transform.localScale.x * 3f, 0.50f));
        chestSequence.Join(transform.DOScaleZ(transform.localScale.z * 3f, 0.50f));
        //rotate chestlid & scale down
        chestSequence.Append(chestLid.transform.DORotate(Vector3.left * 120, 1.5f));
        chestSequence.Join(transform.DOScaleY(transform.localScale.y / 0.5f, 0.50f));
        chestSequence.Join(transform.DOScaleX(transform.localScale.x / 0.5f, 0.50f));
        chestSequence.Join(transform.DOScaleZ(transform.localScale.z / 0.5f, 0.50f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
