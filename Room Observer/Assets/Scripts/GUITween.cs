using DG.Tweening;
using UnityEngine;

public class GUITween : MonoBehaviour
{
    [SerializeField] private TweenValue values;
    
    protected TweenCallback inCallback;
    protected TweenCallback outCallback;
    
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.DOAnchorPos(values.initPos, 0f);
    }
    
    public void EnterFadeInScene()
    {
        Tween anim = _rectTransform.DOAnchorPos(values.endPos, values.duration);
        anim.SetDelay(values.delay);
        anim.SetEase(values.ease).OnComplete(inCallback);
    }

    public void ExitFadeInScene()
    {
        Tween anim = _rectTransform.DOAnchorPos(values.initPos, values.duration);
        anim.SetDelay(values.delay);
        anim.SetEase(values.ease).OnComplete(outCallback);
    }
}