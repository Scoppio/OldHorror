using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSelection : MonoBehaviour {

	public delegate void LanguageAction();
	public static event LanguageAction OnLanguageLoad;

	[SerializeField] private TextAsset csvFile;
	[SerializeField] private string pathFile = "/OldHorrors/Resources/Dialogs/BagOfWords.csv";
	[SerializeField] private List<string> languages = null;
	[SerializeField] private string selectedLanguage = null;
	[SerializeField] private List<string> dialogsBag = null;
	[SerializeField] private bool reload = false;

	private List<Dictionary<string,object>> dialog = null;

	void Awake() {
		Builder ();
	}

	private void Builder () {
		if (csvFile == null) {
			string file = Application.dataPath + pathFile;
			csvFile = (TextAsset) Resources.Load (file);
		}
		dialog = CSVReader.Read (csvFile);
		languages = new List<string>( dialog[0].Keys);
		languages.Remove("id");
		languages.Remove("annotation");
		if (!languages.Contains (selectedLanguage)) {
			SetLanguage (languages [0]);
		}
		PopulateDialogs ();
	}

	public void SetLanguage(string lang) {
		if (languages.Contains (lang)) {
			if (lang != selectedLanguage) {
				selectedLanguage = lang;
				PopulateDialogs ();
			} else {
				selectedLanguage = lang;
			}
		}
	}

	private void PopulateDialogs () {
		if (selectedLanguage != null) {
			dialogsBag.Clear ();
			foreach (Dictionary<string, object> dt in dialog) {
				dialogsBag.Add (dt [selectedLanguage].ToString ());
			}
		} else {
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
			Builder ();
			OnLanguageLoad ();
		}

	}
}
