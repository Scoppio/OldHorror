using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testCSV : MonoBehaviour {

	[SerializeField] private TextAsset csvFile = null;

	void Awake() {
		
		List<Dictionary<string,object>> data = CSVReader.Read (csvFile);
		List<string> keys = new List<string>( data[0].Keys);
		foreach (string k in keys) {
			print (k);
		}
		string output = "";

		foreach (Dictionary<string, object> dt in data) {
			foreach (string k in dt.Keys) {
				output += " " + k + ": " + dt[k];
			}
			print (output);
			output = "";
		}

	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}
}