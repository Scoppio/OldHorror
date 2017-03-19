using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Tracery;

public class NarrativeGenerator : MonoBehaviour {

	// Use this for initialization
	static Random rng = new Random();

	[SerializeField] private string playersName = null;
	[SerializeField] private int days = 7;
	[ReadOnly] [SerializeField] private string monster = null;
	[ReadOnly] [SerializeField] private string cultists = null;
	[ReadOnly] [SerializeField] private List<string> normalClues = null; 
	[ReadOnly] [SerializeField] private List<string> mainClues = null;
	[ReadOnly] [SerializeField] private List<string> specialClues = null;
	[ReadOnly] [SerializeField] private List<string> contradictingClues = null;
	[ReadOnly] [SerializeField] private int thisGameplayId;

	private LanguageSelection bagOfWords = null;

	void Awake() {
		bagOfWords = GameObject.FindGameObjectWithTag ("GameController")
			.GetComponent<LanguageSelection> ();
	}
	void OnEnable() {
		LanguageSelection.OnLanguageLoad += LanguageSelection_OnLanguageLoad;

	}

	void OnDisable() {
		LanguageSelection.OnLanguageLoad -= LanguageSelection_OnLanguageLoad;
	}

	void LanguageSelection_OnLanguageLoad (){
		OnLanguageLoad ();
	}

	void OnLanguageLoad() {
		this.monster = bagOfWords.getDialog (30);
		this.cultists = bagOfWords.getDialog (28);

	}

	void Start () {
		Grammar grammar = new Grammar();

		grammar.PushRules("origin", new string[]{"Essa é um#mensagem.a# gerada pelo Tracery, aqui veremos #coisa.s# cobertos por #sujeira#!"});
		grammar.PushRules("mensagem", "mensagem,mensaginha,mensajona,bagaça".Split(','));
		grammar.PushRules("coisa", "coisa,tren,bagulho,trambolho".Split(','));
		grammar.PushRules("sujeira", "porra,chocolate,terra,sujeira,gosma #cor#,tinta #cor#".Split(','));
		grammar.PushRules("cor", "verde,vermelha,azul,preta,roxa".Split(','));
		string expanded = grammar.Flatten("#origin#");
		print (expanded);

	}

	void CreateNewStory(Grammar grammar) {
		
		string path = Application.streamingAssetsPath + "/JSON/gameData.json";
		Narrative gameJson = new Narrative ();

		gameJson.monster = this.monster;
		gameJson.cultists = this.cultists;
		gameJson.normalClues = this.normalClues;
		gameJson.mainClues = this.mainClues;
		gameJson.specialClues = this.specialClues;
		gameJson.contradictingClues = this.contradictingClues;
		gameJson.name = this.playersName;
		gameJson.days = this.days;
		gameJson.id = this.GetHashCode();

		string json = JsonUtility.ToJson (gameJson);

		File.WriteAllText (path, json);

		json = File.ReadAllText (path);

		JsonUtility.FromJsonOverwrite (json, gameJson);

		gameJson = JsonUtility.FromJson<Narrative> (json);


	}

	// Update is called once per frame
	void Update () {
		
	}

	[System.Serializable]
	public class Narrative {
		// monster is the monster that is behind all of it.
		public string monster;
		// which is the "real cult" behind the monster awake
		public string cultists;
		// normal clues add flavor to what is happening
		public List<string> normalClues;
		// main clues are the ones that the allow the player to advance the story
		public List<string> mainClues;
		// special clues will reveal things about the monster or the culstists, add flavor to the story and allow for special things to happen
		public List<string> specialClues;
		// contradicting clues will hold the game, one is true and the other is false, if you have the wrong one, the game will stall and you 
		// cannot advance untill you have the true one
		public List<string> contradictingClues;
		// name of the player
		public string name;
		// id of the game
		public int id;
		// number of days already passed ingame
		public int days;
	}
}
