using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroSceneController : MonoBehaviour {


    public PlayableDirector FadeOutClip = null;

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
            if(Input.GetKeyDown(KeyCode.Space) || CheckMakey())
            {
                // Fade Out and Advance Scene
                FadeOutClip.Play();
                yield return new WaitForSeconds((float)FadeOutClip.duration);
                ControlFlowManager.Singleton.AdvanceScene();
            }
            yield return null;
        }
	}
}
