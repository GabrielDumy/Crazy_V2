using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using LitJson;



public class Login : MonoBehaviour {

	public GameObject email_form;
	public GameObject password_form;

	private static string Email;
	private static string Password;

	JsonData json;

	// Use this for initialization
	void Start () {
	
	}

	public void LoginButon()
	{
		PostData ();
	}

	// Update is called once per frame
	void Update () {

		Email = email_form.GetComponent<InputField>().text;
		Password = password_form.GetComponent<InputField>().text;

	}


	void PostData()
	{
		Login_API user = new Login_API (new BasicLogin(Email,Password));
		
		string postURL = "http://api.estudiobox.es:3000/v1/users/login";
		json = JsonMapper.ToJson (user);
		
		string jsonString = json.ToString();
		
		Debug.Log (jsonString);
		
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




	public class Login_API
	{
		public BasicLogin userData;
		
		public Login_API( BasicLogin userData)
		{
			this.userData = userData;
		}
	}
	
	public class BasicLogin
	{
		public string email;
		public string password;
		
		public BasicLogin(string email,string password)
		{
			this.email = email;
			this.password = password;
		}
	}
}
