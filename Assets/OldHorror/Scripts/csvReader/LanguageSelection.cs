using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tracery;

public class LanguageSelection : MonoBehaviour {

	public delegate void LanguageAction();
	public static event LanguageAction OnLanguageLoad;

	[SerializeField] private TextAsset csvFile;
	[SerializeField] private string pathFile = "/OldHorrors/Resources/Dialogs/BagOfWords.csv";
	[SerializeField] private List<string> languages = null;
	[SerializeField] private string selectedLanguage = null;
	[SerializeField] private List<string> dialogsBag = null;
	[SerializeField] private bool reload = false;

	private Grammar grammar = new Grammar();

	private List<Dictionary<string,object>> dialog = null;

	void Awake() {
		Debug.Log ("First load of LanguageSelection");
		dialog = CSVReader.Read (csvFile);

		languages = new List<string>( dialog[0].Keys);
		Builder ();
	}

	public bool IsPopulated ( ) {
		if (dialogsBag != null && dialogsBag.Count > 0) {
			return true;
		} else {
			return false;
		}
	}

	private void Builder () {
		if (csvFile == null) {
			string file = Application.dataPath + pathFile;
			csvFile = (TextAsset) Resources.Load (file);
		}

		if (!languages.Contains (selectedLanguage)) {
			Debug.Log ("Setting language in Builder");
			SetLanguage (languages [2]);
		}
	}

	public void SetLanguage(string lang, bool Io = false) {
		Debug.Log (lang + " selected");
		if (languages.Contains (lang)) {
			if (lang != selectedLanguage || Io == true) {
				selectedLanguage = lang;
				PopulateDialogs ();
				OnLanguageLoad ();
			}
		}
	}

	private void PopulateDialogs () {
		Debug.Log ("Entering into PopulateDialogs()");
		if (selectedLanguage != null) {
			Debug.Log ("clearing dialogs bag");
			dialogsBag.Clear ();
			Debug.Log ("Entering into foreach");
			foreach (Dictionary<string, object> dt in dialog) {
				if (dt ["annotation"].ToString ().StartsWith ("#")) {
					Debug.Log (dt ["annotation"].ToString ().Substring (1) + " : " + dt [selectedLanguage].ToString ());
					grammar.PushRules (dt ["annotation"].ToString ().Substring(1), dt [selectedLanguage].ToString ().Split (','));
				}
				grammar.PushRules("origin", new string[] {dt [selectedLanguage].ToString()});
				dialogsBag.Add (grammar.Flatten("#origin#"));
			}
		} else {
			Debug.LogError ("selected language is NULL!");
			throw new PlayerPrefsException ("selectedLanguage is " + selectedLanguage.ToString());
		}
	}

	public string getDialog(int i) {
		if (dialogsBag == null || dialogsBag.Count == 0) {
			try {
				Builder();
			} catch {
				throw new PlayerPrefsException ("Bag of words is empty");
			}
		}
		if (i <= dialogsBag.Count - 1) {
			return dialogsBag [i];
		}
		Debug.Log (i.ToString () + " " + (dialogsBag.Count -1 ).ToString() );
		return "IndexOutOfBounds";
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (reload) {
			reload = false;
			SetLanguage (selectedLanguage, true);
		}
	}
}
