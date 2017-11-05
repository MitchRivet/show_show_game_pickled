using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class WinLoseSceneController : MonoBehaviour {

    public PlayableDirector FadeOutClip = null;
    public float TimeUntilTransitionToNextScene = 10f;
    private float CurrentTimeElapsed = 0.0f;

    public TextMeshProUGUI TimeLeftText = null;
    public TextMeshProUGUI TitleText = null;
    public TextMeshProUGUI DescriptionText = null;

    public string WinTitleText = null;
    public string WinDescriptionText = null;

    public string LoseTitleText = null;
    public string LoseDescriptionText = null;


    private void Start()
    {
        if(ControlFlowManager.Singleton)
        {
            // if loading from master scene, see win state
            bool DidWin = ControlFlowManager.Singleton.CurrentWinLoseState == ControlFlowManager.WIN_LOSE_STATE.WIN ? true : false;
            if(DidWin)
            {
                // replace placeholder text with win text
                TitleText.text = WinTitleText;
                DescriptionText.text = WinDescriptionText;
            }
            else
            {
                // replace placeholder text with lose text
                TitleText.text = LoseTitleText;
                DescriptionText.text = LoseDescriptionText;
            }
        }
        else
        {
            // if just testing in the editor, force a win
            TitleText.text = WinTitleText;
            DescriptionText.text = WinDescriptionText;
        }
        StartCoroutine(Co_Update());
    }

    // Update is called once per frame
    IEnumerator Co_Update () {
        while(true)
        {
            TimeLeftText.text = "Transitioning to blank scene in " + (int)(TimeUntilTransitionToNextScene - CurrentTimeElapsed) + " seconds.";
            if (CurrentTimeElapsed > TimeUntilTransitionToNextScene)
            {
                // Fade Out and Advance Scene
                FadeOutClip.Play();
                yield return new WaitForSeconds((float)FadeOutClip.duration);
                ControlFlowManager.Singleton.AdvanceScene();
            }
            CurrentTimeElapsed += Time.deltaTime;
            yield return null;
        }
	}
}
