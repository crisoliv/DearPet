using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

	private int id;
	private string name;
	private string username;
	private string password;
	private string sex;
	private string birthday;

	public Player (int id, string name, string username, string password, string sex, string birthday)
	{
		this.id = id;
		this.name = name;
		this.username = username;
		this.password = password;
		this.sex = sex;
		this.birthday = birthday;
	}
	public Player(){
	}

	public int Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}
	
	public string Name {
		get {
			return this.name;
		}
		set {
			name = value;
		}
	}

	public string Username {
		get {
			return this.username;
		}
		set {
			username = value;
		}
	}

	public string Password {
		get {
			return this.password;
		}
		set {
			password = value;
		}
	}

	public string Sex {
		get {
			return this.sex;
		}
		set {
			sex = value;
		}
	}

	public string Birthday{
		get {
			return this.birthday;
		}
		set {
			birthday = value;
		}
	}
}
