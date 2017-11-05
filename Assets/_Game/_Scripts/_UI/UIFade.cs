using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour {

    public bool StartDarkToFadeIn = true;

    private Image FadeImage = null;

	// Use this for initialization
	void Awake () {
        FadeImage = gameObject.GetComponent<Image>();
		if(StartDarkToFadeIn)
        {
            if (!FadeImage.enabled)
                FadeImage.enabled = true;
        }
	}
}
