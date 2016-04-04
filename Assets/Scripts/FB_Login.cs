using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using LitJson;

public class FB_Login : MonoBehaviour {
	//public GameObject DialogLoggedIn;
	public GameObject DialogLoggedOut;
	private static string accesToken;
	//public GameObject DialogUserName;
	//public GameObject DialogProfilePic;
	JsonData json;
	
	void Awake()
	{
		
		FB.Init (SetInit,OnHideUnity);
	}
	
	void SetInit()
	{
		if (FB.IsLoggedIn) {
			Debug.Log ("FB is logged in");
		} else {
			Debug.Log ("FB is not logged in");
		}
		DealWithFBMenus(FB.IsLoggedIn);
	}
	void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale=1;
		}
	}
	public void FBlogin()
	{
		List<string> permissions = new List<string>();
		permissions.Add ("public_profile");
		
		FB.LogInWithReadPermissions (permissions,AuthCallBack);
	}
	
	void AuthCallBack(IResult result)
	{
		if(result.Error != null)
		{
			Debug.Log (result.Error);
		}else{
			if (FB.IsLoggedIn) {
				Debug.Log ("FB is logged in");
			} else {
				Debug.Log ("FB is not logged in");
				PostData();
			}
			DealWithFBMenus(FB.IsLoggedIn);
		}
	}
	
	void DealWithFBMenus(bool isLoggedIn)
	{
		if (isLoggedIn) {
			//DialogLoggedIn.SetActive (true);
			DialogLoggedOut.SetActive(false);
			
			
		//	FB.API("/me?fields=first_name",HttpMethod.GET,DisplayUserName);
		//	FB.API("/me/picture?type=square&height=128&width=128",HttpMethod.GET,DisplayProfilePic);

			PostData();
			Debug.Log (accesToken);
			
		} else {
			//DialogLoggedIn.SetActive (false);
			DialogLoggedOut.SetActive(true);
		}
	}


	void PostData()
	{
		accesToken = AccessToken.CurrentAccessToken.TokenString;
		Login_FB_API fb_token = new Login_FB_API(accesToken);
		Debug.Log (fb_token);
		string postURL = "http://api.estudiobox.es:3000/v1/users/fb";
		json = JsonMapper.ToJson (fb_token);
		Debug.Log (json);
		string jsonString = json.ToString();

		
		Dictionary<string, string> dict = new Dictionary<string, string>();
		
		dict.Add("Content-Type", "application/json");
		
		byte[] body =System.Text.Encoding.UTF8.GetBytes(jsonString);
		
		WWW www = new WWW(postURL,body,dict);
		
		StartCoroutine (PostDataEnumerator(www));
	}
	
	IEnumerator PostDataEnumerator(WWW www)
	{
		yield return www;
		if (www.error != null)
		{
			Debug.Log( www.error);
			Debug.Log (www.text);
		}
		else
		{
			Debug.Log( www.error);
			Debug.Log(www.text);
		}
	}
	public class Login_FB_API
	{
		public string fb_token;

		public Login_FB_API( string fb_token)
		{
			this.fb_token = fb_token;
		}
	}

	/*void DisplayUserName(IResult result)
	{
		Text UserName = DialogUserName.GetComponent<Text>();
		
		if (result.Error == null) {
			UserName.text = "Hi there, "+result.ResultDictionary["first_name"];
		} else {
			Debug.Log (result.Error);
			
			
		}
	}
	
	void DisplayProfilePic(IGraphResult result)
	{
		if (result.Texture != null) {
			Image ProfilePic= DialogProfilePic.GetComponent<Image>();
			ProfilePic.sprite=Sprite.Create(result.Texture,new Rect	(0,0,128,128),new Vector2());
			
		} else {
			Debug.Log("Picture loading error");
		}
	}*/
	
}