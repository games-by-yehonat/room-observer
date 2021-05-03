using System.Collections;
using TMPro;
using UnityEngine;

public class WaveView : GUITween
{
    [SerializeField] private TextMeshProUGUI waveText;
    
    private void Start()
    {
        inCallback = () => { StartCoroutine(WaitToExitFade()); };
        outCallback = () => { GameController.Instance.StartGame(); };
        
        EnterFadeInScene();
    }
    
    public void SetWaveInText(string value)
    {
        waveText.text = value;
    }

    private IEnumerator WaitToExitFade()
    {
        yield return new WaitForSeconds(.5f);
        ExitFadeInScene();
    }
}
