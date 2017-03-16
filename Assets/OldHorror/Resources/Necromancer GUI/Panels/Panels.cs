using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panels : MonoBehaviour {

	[SerializeField] private bool casesPanel = true;
	[SerializeField] private bool settingsPanel = false;
	[SerializeField] private bool cluePanel = false;

	public static bool s_cluePanel = false;

	public static string s_title = "Here should be a Title";
	public static string s_text = "If there is no text here, something is very wrong!";
	public static List<string> s_tags;

	private float leafOffset;
	private float frameOffset;
	private float skullOffset;

	private float RibbonOffsetX;
	private float FrameOffsetX;
	private float SkullOffsetX;
	private float RibbonOffsetY;
	private float FrameOffsetY;
	private float SkullOffsetY;

	private float WSwaxOffsetX;
	private float WSwaxOffsetY;
	private float WSribbonOffsetX;
	private float WSribbonOffsetY;

	private int spikeCount;

	// This script will only work with the Necromancer skin
	[SerializeField] GUISkin mySkin = null;

	//if you're using the spikes you'll need to find sizes that work well with them these are a few...
	private Rect windowRect0 = new Rect (500, 140, 350, 510);
	private Rect windowRect1 = new Rect (500, 140, 350, 510);
	private Rect windowRect4 = new Rect ( 0, 0, Screen.width * 0.4f, Screen.height * 0.98f );
	private Vector2 scrollPosition;

	void AddSpikes (float winX) {
		spikeCount = (int)Mathf.Floor (winX - 152) / 22;
		GUILayout.BeginHorizontal();
		GUILayout.Label ("", "SpikeLeft");//-------------------------------- custom
		for (int i = 0; i < spikeCount; i++)
		{
			GUILayout.Label ("", "SpikeMid");//-------------------------------- custom
		}
		GUILayout.Label ("", "SpikeRight");//-------------------------------- custom
		GUILayout.EndHorizontal();
	}

	void FancyTop (float topX) {
		leafOffset = (topX/2)-64;
		frameOffset = (topX/2)-27;
		skullOffset = (topX/2)-20;
		GUI.Label (new Rect(leafOffset, 18, 0, 0), "", "GoldLeaf");//-------------------------------- custom	
		GUI.Label (new Rect(frameOffset, 3, 0, 0), "", "IconFrame");//-------------------------------- custom	
		GUI.Label (new Rect(skullOffset, 12, 0, 0), "", "Skull");//-------------------------------- custom	
	}

	void WaxSeal (float x, float y) {
		WSwaxOffsetX = x - 120f;
		WSwaxOffsetY = y - 115f;
		WSribbonOffsetX = x - 114f;
		WSribbonOffsetY = y - 83f;
		GUI.Label (new Rect(WSribbonOffsetX, WSribbonOffsetY, 0, 0), "", "RibbonBlue");//-------------------------------- custom	
		GUI.Label (new Rect(WSwaxOffsetX, WSwaxOffsetY, 0, 0), "", "WaxSeal");//-------------------------------- custom	
	}

	void DeathBadge (float x, float y) {
		RibbonOffsetX = x;
		FrameOffsetX = x+3;
		SkullOffsetX = x+10;
		RibbonOffsetY = y+22;
		FrameOffsetY = y;
		SkullOffsetY = y+9;

		GUI.Label (new Rect(RibbonOffsetX, RibbonOffsetY, 0, 0), "", "RibbonRed");//-------------------------------- custom	
		GUI.Label (new Rect(FrameOffsetX, FrameOffsetY, 0, 0), "", "IconFrame");//-------------------------------- custom	
		GUI.Label (new Rect(SkullOffsetX, SkullOffsetY, 0, 0), "", "Skull");//-------------------------------- custom	
	}

	void DoCasesPanel (int windowID) {
		// use the spike function to add the spikes
		// note: were passing the width of the window to the function
		AddSpikes(windowRect0.width);

		GUILayout.BeginVertical ();
		GUILayout.Space (8);
		GUILayout.Label ("", "Divider");//-------------------------------- custom
		GUILayout.Label ("Standard Label");
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Short Label", "ShortLabel");//-------------------------------- custom
		GUILayout.Label ("Short Label", "ShortLabel");//-------------------------------- custom
		GUILayout.EndHorizontal ();
		GUILayout.Label ("", "Divider");//-------------------------------- custom
		GUILayout.Button ("Standard Button");
		GUILayout.Button ("Short Button", "ShortButton");//-------------------------------- custom
		GUILayout.Label ("", "Divider");//-------------------------------- custom
		GUILayout.Box ("This is a textbox\n this can be expanded by using \\n");
		GUILayout.TextField ("This is a textfield\n You cant see this text!!");
		GUILayout.TextArea ("This is a textArea\n this can be expanded by using \\n");
		GUILayout.EndVertical ();

		// Make the windows be draggable.
		GUI.DragWindow (new Rect (0,0,10000,10000));
	}

	void DoSettingsPanel (int windowID) {

	}
		/*
	void DoMyWindow1 (int windowID) {
		// use the spike function to add the spikes
		AddSpikes(windowRect1.width);

		GUILayout.BeginVertical();
		GUILayout.Label ("", "Divider");//-------------------------------- custom
		GUILayout.Label ("Plain Text", "PlainText");//------------------------------------ custom
		GUILayout.Label ("Italic Text", "ItalicText");//---------------------------------- custom
		GUILayout.Label ("Light Text", "LightText");//----------------------------------- custom
		GUILayout.Label ("Bold Text", "BoldText");//------------------------------------- custom
		GUILayout.Label ("Disabled Text", "DisabledText");//-------------------------- custom
		GUILayout.Label ("Cursed Text", "CursedText");//------------------- custom
		GUILayout.Label ("Legendary Text", "LegendaryText");//-------------------- custom
		GUILayout.Label ("Outlined Text", "OutlineText");//--------------------------- custom
		GUILayout.Label ("Italic Outline Text", "ItalicOutlineText");//---------------------------------- custom
		GUILayout.Label ("Light Outline Text", "LightOutlineText");//----------------------------------- custom
		GUILayout.Label ("Bold Outline Text", "BoldOutlineText");//----------------- custom
		GUILayout.EndVertical();
		// Make the windows be draggable.
		GUI.DragWindow (new Rect (0,0,10000,10000));
	}

	void DoMyWindow2 (int windowID) {
		// use the spike function to add the spikes
		AddSpikes(windowRect2.width);

		GUILayout.Space(8);
		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);
		GUILayout.Label (NecroText, "PlainText");
		GUILayout.EndScrollView();
		GUILayout.EndHorizontal();
		GUILayout.Space(8);

		HroizSliderValue = GUILayout.HorizontalSlider (HroizSliderValue, 0.0f, 1.1f);
		VertSliderValue = GUILayout.VerticalSlider(VertSliderValue, 0.0f, 1.1f, GUILayout.Height(70));
		DeathBadge(200,350);
		GUILayout.EndVertical();
		GUI.DragWindow (new Rect (0,0,10000,10000));
	}

	//bringing it all together
	void DoMyWindow3 (int windowID) {
		// use the spike function to add the spikes
		AddSpikes(windowRect3.width);
		bool alpha = false;
		//add a fancy top using the fancy top function
		FancyTop(windowRect0.width);

		GUILayout.Space(8);
		GUILayout.BeginVertical();
		GUILayout.Label("Necromancer");
		GUILayout.Label ("", "Divider");
		GUILayout.Label ("Necromancer is a free to use GUI for the unity community. this skin can be used in commercial and non-commercial products.", "LightText");
		GUILayout.Label ("", "Divider");
		GUILayout.Space(8);
		doWindow0 = GUILayout.Toggle(doWindow0, "Standard Components");
		doWindow1 = GUILayout.Toggle(doWindow1, "Text Examples");
		doWindow2 = GUILayout.Toggle(doWindow2, "Sliders");
		GUILayout.Space(8);
		GUILayout.Label ("", "Divider");
		GUILayout.Label ("Please read through the source of this script to see", "PlainText");
		GUILayout.BeginHorizontal();
		GUILayout.Label ("how to use special ", "PlainText");
		GUILayout.Label ("Components ", "LegendaryText");
		GUILayout.Label ("and ", "PlainText");
		alpha = GUILayout.Button ("Functions ", "CursedText");
		if (alpha) {
			Debug.Log ("Click");
		}
		GUILayout.Label ("!", "PlainText");
		GUILayout.EndHorizontal();
		GUILayout.Label ("", "Divider");
		GUILayout.Space(26);
		GUILayout.Label ("Created By Jason Wentzel 2011", "SingleQuotes");
		GUILayout.EndVertical();

		// add a wax seal at the bottom of the window
		WaxSeal(windowRect3.width , windowRect3.height);

		GUI.DragWindow (new Rect (0,0,10000,10000));
	}
	*/
	void DoClueStats (int windowID) {

		//AddSpikes (windowRect4.width);
		GUILayout.BeginVertical();
		GUILayout.Space(44);
		GUILayout.Label(s_title,  "BoldText");
		GUILayout.Label ("", "Divider");
		GUILayout.BeginHorizontal();
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
		GUILayout.Label (s_text, "PlainText");
		GUILayout.EndScrollView();
		GUILayout.EndHorizontal();
		GUILayout.Space(8);
		GUILayout.Label ("", "Divider");
		GUILayout.Space(8);
		if (s_tags != null) {
			foreach (string s_tag in s_tags) {
				GUILayout.Label (s_tag, "ShortLabel");//-------------------------------- custom
			}
			GUILayout.Label ("", "Divider");
			GUILayout.Space(8);
		}
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Reject")) {
			InteractScript.isHoldingObject = false;
		}
		GUILayout.Button ("Accept");
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical();

		GUI.DragWindow (new Rect (0,0,10000,10000));
	}

	void OnGUI ()
	{
		GUI.skin = mySkin;

		if (casesPanel) {
			windowRect0 = GUI.Window (0, windowRect0, DoCasesPanel, "");
		}
		//now adjust to the group. (0,0) is the topleft corner of the group.
		GUI.BeginGroup (new Rect (0,0,100,100));
		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();

		if (settingsPanel) {
			windowRect0 = GUI.Window (1, windowRect1, DoSettingsPanel, "");
		}
		//now adjust to the group. (0,0) is the topleft corner of the group.
		GUI.BeginGroup (new Rect (0,0,100,100));
		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();

		if (s_cluePanel) {
			windowRect4 = GUI.Window (2, windowRect4, DoClueStats, "");
		}
		//now adjust to the group. (0,0) is the topleft corner of the group.
		GUI.BeginGroup (new Rect (0,0,100,100));
		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();
	}

	// Use this for initialization
	void Start () {
		Debug.Log(Screen.height.ToString() + " " + Screen.width.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		cluePanel = s_cluePanel;
	}
}
