using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Tracery;

public class NarrativeScriptable : ScriptableObject {


	private string expanded;

	// Use this for initialization
	void Start () {
		string path = Application.streamingAssetsPath + "/_GameManager_/Resources/gameData.json";
		Narrative thisLevel = new Narrative ();

		thisLevel.expanded = this.expanded;
		thisLevel.name = "Teste JSON";
		thisLevel.id = 1;

		string json = JsonUtility.ToJson (thisLevel);

		File.WriteAllText (path, json);

		//thisLevel = JsonUtility.FromJson<Narrative> ();
		//JsonUtility.FromJsonOverwrite (json, thisLevel);
		//json = File.ReadAllText (Path);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Save() {

	}

	void setExpanded(string s_expanded) {
		this.expanded = s_expanded;
	}

	[System.Serializable]
	public class Narrative {
		public string expanded;
		public string name;
		public int id;

	}
}
