using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;


public class GameplaySceneController : MonoBehaviour {


    public PlayableDirector FadeOutClip = null;
    public PlayableDirector ShakeScreenClip = null;

    public int NumberOfClicksToWin = 5;
    private int CurrentNumberOfClicks = 0;

    public float SecondsUntilLoss = 10;
    private float NumberOfSecondsElapsed = 0;

   public TextMeshProUGUI CounterText = null;

    private void Start()
    {
        StartCoroutine(Co_Update());
    }
    // I have this in here so that you can debug your level without getting exceptions when master scene isn't loaded
    // Consider this function to return true if makey makey was clicked this frame
    bool CheckMakey()
    {
        if (ControlFlowManager.Singleton)
        {
            if (ControlFlowManager.Singleton.WasMakeyMakeyClickedThisFrame())
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    IEnumerator Co_Update () {
        while(true)
        {
            if(NumberOfSecondsElapsed > SecondsUntilLoss)
            {
                // Lose Game
                Debug.Log("Game Lost");

                // Fade Out and Advance Scene
                FadeOutClip.Play();
                yield return new WaitForSeconds((float)FadeOutClip.duration);

                // Set win flag so that next scene can load based on that
                ControlFlowManager.Singleton.CurrentWinLoseState = ControlFlowManager.WIN_LOSE_STATE.LOSE;

                ControlFlowManager.Singleton.AdvanceScene();
            }

            if (Input.GetKeyDown(KeyCode.Space) || CheckMakey())
            {
                CurrentNumberOfClicks++;
                Debug.Log("Current clicks: " + CurrentNumberOfClicks + "\nTime Left: " + (SecondsUntilLoss - NumberOfSecondsElapsed));
                CounterText.text = "Counter = " + CurrentNumberOfClicks;

                // Play sequence of zooming in on pig
                ShakeScreenClip.Play();
                yield return new WaitForSeconds((float)ShakeScreenClip.duration);

                if (CurrentNumberOfClicks >= 5)
                {
                    // Win Game
                    Debug.Log("Game Won");

                    // Fade Out and Advance Scene
                    FadeOutClip.Play();
                    yield return new WaitForSeconds((float)FadeOutClip.duration);

                    // Set win flag so that next scene can load based on that
                    ControlFlowManager.Singleton.CurrentWinLoseState = ControlFlowManager.WIN_LOSE_STATE.WIN;

                    ControlFlowManager.Singleton.AdvanceScene();
                }
            }
            NumberOfSecondsElapsed += Time.deltaTime;
            yield return null;
        }
	}
}
