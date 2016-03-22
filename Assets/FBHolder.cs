using UnityEngine;
using System.Collections;
using Facebook;

public class FBHolder : MonoBehaviour {

	void Awake()
	{
		FB.Init (SetInit, OnHideUnity);
	}

	private void SetInit()
	{
		Debug.Log("FB init Done");
		if (FB.IsLoggedIn) {
			Debug.Log("FB Logged In");
		}else 
		{
		}
	}

	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale=1;
		}
	}
}
