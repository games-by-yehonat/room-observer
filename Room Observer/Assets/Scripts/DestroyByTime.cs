using System.Collections;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] private float time = 1f;

    private WaitForSeconds _delay;
    private void Start()
    {
        _delay = new WaitForSeconds(time);
        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return _delay;
        Destroy(gameObject);
    }
}
