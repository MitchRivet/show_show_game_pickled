using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour {
    public List<GameObject> bars;

	void Update () {
		for(int i = 0; i < MMWSServer.Instance.makeyMakeys.Count; i++)
        {
            Vector3 newScale = bars[i].transform.localScale;
            //We divide by 30 here because that is the max number of clicks per second in the simulator.
            //In the live verson it won't be locked down
            newScale.y = MMWSServer.Instance.makeyMakeys[i].cps / 30.0f * 5.0f;
            bars[i].transform.localScale = newScale;
        }
	}
}
