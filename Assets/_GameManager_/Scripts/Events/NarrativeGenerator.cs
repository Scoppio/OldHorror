using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Tracery;

public class NarrativeGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Grammar grammar = new Grammar();
		
		grammar.PushRules("mensagem", "mensagem,mensaginha,mensajona,bagaça".Split(','));
		grammar.PushRules("coisas", "coisas,trens,bagulhos,trambolhos".Split(','));
		grammar.PushRules("sujeira", "porra,chocolate,terra,sujeira,gosma #cor#,tinta #cor#".Split(','));
		grammar.PushRules("origin", new string[]{"Essa é uma #mensagem# gerada pelo Tracery, aqui veremos #coisas# cobertos por #sujeira#!"});
		grammar.PushRules("cor", "verde,vermelha,azul,preta,roxa".Split(','));
		string expanded = grammar.Flatten("#origin#");
		print (expanded);

		string path = Application.streamingAssetsPath + "/JSON/gameData.json";
		Debug.Log (path);

		//---------------------------

		Narrative thisLevel = new Narrative ();

		thisLevel.expanded = expanded;
		thisLevel.name = "Teste JSON";
		thisLevel.id = 1;

		File.WriteAllText (path, JsonUtility.ToJson(thisLevel));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[System.Serializable]
	public class Narrative {
		public string expanded;
		public string name;
		public int id;

	}
}
