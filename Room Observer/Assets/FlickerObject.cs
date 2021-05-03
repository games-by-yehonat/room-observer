using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FlickerObject : MonoBehaviour
{
    [SerializeField] [Range(.1f, 1f)] private float frequency;

    private CanvasGroup _canvasGroup;
    private WaitForSeconds _delay;
    private IEnumerator _flickerRuntime;

    private bool _flicking = true;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _delay = new WaitForSeconds(frequency);
        _flickerRuntime = Flicker();

        StartCoroutine(_flickerRuntime);
    }

    private void OnDisable()
    {
        StopCoroutine(_flickerRuntime);
    }

    private IEnumerator Flicker()
    {
        var state = false;
        
        while (_flicking)
        {
            _canvasGroup.alpha = state ? 0 : 1;
            state = !state;
            yield return _delay;
        }
    }
}
