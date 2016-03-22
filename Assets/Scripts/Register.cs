using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using LitJson; 
using System.IO;

public class Register : MonoBehaviour {

	public GameObject nickname;
	public GameObject email;
	public GameObject password;
	public GameObject confPassword;
	public GameObject name;
	public GameObject lastname;
	
	private static string Nickname;
	private static string Email;
	private static string Password;
	private static string ConfPassword;
	private static string Name;
	private static string LastName;
	
	private string form;
	private bool EmailValid = false;

	JsonData json;

	public User user = new User (new BasicUser("Dumy","Gaby","Dumitru","yo_hayduk@yahoo.es","","","gaby"));
	// Use this for initialization
	void Start () {

	}

	public void RegisterButton()
	{
		PostData ();
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) 
		{
			if(nickname.GetComponent<InputField>().isFocused)
			{
				email.GetComponent<InputField>().Select();
			}
			if(email.GetComponent<InputField>().isFocused)
			{
				password.GetComponent<InputField>().Select();
			}
			if(password.GetComponent<InputField>().isFocused)
			{
				confPassword.GetComponent<InputField>().Select();
			}
		}
		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			if(Nickname != "" && Email!= "" && Password!="" && ConfPassword!= "")
				RegisterButton();
		}

		Nickname = nickname.GetComponent<InputField>().text;
		Email = email.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;
		ConfPassword = confPassword.GetComponent<InputField>().text;
		Name = name.GetComponent<InputField>().text;
		LastName = lastname.GetComponent<InputField>().text;

	}

	void PostData(){

		string postURL = "http://api.estudiobox.es:3000/v1/users";
		json = JsonMapper.ToJson (user);
		
		string jsonString = json.ToString();

		Debug.Log (jsonString);

		var encoding = new System.Text.UTF8Encoding();

		/*Hashtable postHeader = new Hashtable();*/

		Dictionary<string, string> dict = new Dictionary<string, string>();

		dict.Add("Content-Type", "text/json");

		byte[] body = encoding.GetBytes (jsonString);
		
		WWW www = new WWW(postURL,body,dict);

		StartCoroutine ("PostDataEnumerator",www);
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
				Debug.Log(www.error);
			}
		}




	public class User
	{
		public BasicUser userData;
		
		public User( BasicUser userData)
		{
			this.userData = userData;
		}
	}
	
	public class BasicUser
	{
		public string nickname;
		public string name;
		public string last_name;
		public string email;
		public string avatar;
		public string cover;
		public string password;
		
		public BasicUser(string nickname,string name,string last_name,string email,string avatar, string cover,string password)
		{
			this.nickname = nickname;
			this.name = name;
			this.last_name =last_name;
			this.email = email;
			this.avatar = avatar;
			this.cover = cover;
			this.password = password;
			
		}
	}
}
