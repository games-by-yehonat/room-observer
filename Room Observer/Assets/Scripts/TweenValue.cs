using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class TweenValue
{
    public Vector2 initPos;
    public Vector2 endPos;
    public float duration;
    public float delay;
    public Ease ease;
}