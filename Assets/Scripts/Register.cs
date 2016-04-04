using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using LitJson; 
using System.IO;

public class Register : MonoBehaviour {

	public GameObject nickname_form;
	public GameObject email_form;
	public GameObject password_form;
	public GameObject confPassword_form;
	public GameObject name_form;
	public GameObject lastname_form;
	
	private static string Nickname;
	private static string Email;
	private static string Password;
	private static string ConfPassword;
	private static string Name;
	private static string LastName;
	
	private string form;
	private bool EmailValid = false;

	JsonData json;

	// Use this for initialization
	void Start () {

	}

	public void RegisterButton()
	{
		if (Nickname != "" && Name != "" && LastName != "" && Email != "") 
		{

			if(Password==ConfPassword)
			{
				PostData ();
			}else{
				Debug.Log ("La contraseña no coincide");
			}

		} else {
			Debug.Log("Todos los campos deben ser rellenados");
		}



	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) 
		{
			if(nickname_form.GetComponent<InputField>().isFocused)
			{
				email_form.GetComponent<InputField>().Select();
			}
			if(email_form.GetComponent<InputField>().isFocused)
			{
				password_form.GetComponent<InputField>().Select();
			}
			if(password_form.GetComponent<InputField>().isFocused)
			{
				confPassword_form.GetComponent<InputField>().Select();
			}
		}
		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			if(Nickname != "" && Email!= "" && Password!="" && ConfPassword!= "")
				RegisterButton();
		}

		Nickname = nickname_form.GetComponent<InputField>().text;
		Email = email_form.GetComponent<InputField>().text;
		Password = password_form.GetComponent<InputField>().text;
		ConfPassword = confPassword_form.GetComponent<InputField>().text;
		Name = name_form.GetComponent<InputField>().text;
		LastName = lastname_form.GetComponent<InputField>().text;

	}

	void PostData(){

		User user = new User (new BasicUser(Nickname,Name,LastName,Email,"","",Password));

		string postURL = "http://api.estudiobox.es:3000/v1/users";
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
