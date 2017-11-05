using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


public class GameplaySceneController : MonoBehaviour {


    public PlayableDirector FadeOutClip = null;
    public PlayableDirector ShakeScreenClip = null;

    public int NumberOfClicksToWin = 5;
    private int CurrentNumberOfClicks = 0;

    public float SecondsUntilLoss = 10;
    private float NumberOfSecondsElapsed = 0;

   // public TextMeshPro CounterText = null;

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
                Debug.Log("Game Lost");
                // Lose Game
                // Set win flag so that next scene can load based on that
                ControlFlowManager.Singleton.CurrentWinLoseState = ControlFlowManager.WIN_LOSE_STATE.LOSE;

                // Fade Out and Advance Scene
                FadeOutClip.Play();
                yield return new WaitForSeconds((float)FadeOutClip.duration);

                ControlFlowManager.Singleton.AdvanceScene();
            }

            if (Input.GetKeyDown(KeyCode.Space) || CheckMakey())
            {
                CurrentNumberOfClicks++;
                Debug.Log("Current clicks: " + CurrentNumberOfClicks + "\nTime Left: " + (SecondsUntilLoss - NumberOfSecondsElapsed));

                if(CurrentNumberOfClicks >= 5)
                {
                    // Win Game
                    Debug.Log("Game Won");

                    // Set win flag so that next scene can load based on that
                    ControlFlowManager.Singleton.CurrentWinLoseState = ControlFlowManager.WIN_LOSE_STATE.WIN;
                    
                    // Fade Out and Advance Scene
                    FadeOutClip.Play();
                    yield return new WaitForSeconds((float)FadeOutClip.duration);

                    ControlFlowManager.Singleton.AdvanceScene();
                }
            }
            NumberOfSecondsElapsed += Time.deltaTime;
            yield return null;
        }
	}
}
