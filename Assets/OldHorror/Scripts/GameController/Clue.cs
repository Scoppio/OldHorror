using System.Collections.Generic;
using UnityEngine;

public class Clue {

	private int id;
	private string name;
	private string description;
	private List<string> tag;

	public Clue() {}

	public int Id 
	{
		get { return this.id; }
		set { this.id = value; }
	}

	public string Name
	{
		get { return this.name; }
		set { this.name = value; }
	}

	public string Description
	{
		get { return this.description; }
		set { this.description = value; }
	}

	public List<string> Tag 
	{
		get { return this.tag; }
		set { this.tag = value; }
	}

	public void AddTag (string value) 
	{
		this.tag.Add (value);
	}

	public void RemoveTag (string value)
	{
		this.tag.Remove (value);
	}
}
