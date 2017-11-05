using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class RulesSceneController : MonoBehaviour {

    public PlayableDirector FadeOutClip = null;
    public float TimeUntilTransitionToNextScene = 15f;
    private float CurrentTimeElapsed = 0.0f;

    public TextMeshProUGUI TimeLeftText = null;

    private void Start()
    {
        StartCoroutine(Co_Update());
    }

    // Update is called once per frame
    IEnumerator Co_Update () {
        while(true)
        {
            TimeLeftText.text = "Transitioning to next scene in " + (int)(TimeUntilTransitionToNextScene - CurrentTimeElapsed) + " seconds.";
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
