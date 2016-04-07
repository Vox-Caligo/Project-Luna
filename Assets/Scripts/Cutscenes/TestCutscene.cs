using UnityEngine;
using System.Collections;

public class TestCutscene : CutsceneTrigger
{
	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
	}
	
	public override void startCutscene() {
		base.startCutscene ();
	}

	protected override void setCutsceneScript() { 
		cutsceneScript = new ArrayList ();
		cutsceneScript.Add (new ScriptLine ("Aegis", "Talk", "It's me!"));
		cutsceneScript.Add (new ScriptLine ("Aegis", "Talk", "I like to disco!"));
		cutsceneScript.Add (new ScriptLine ("Minion", "Talk", "What was that?"));
		cutsceneScript.Add (new ScriptLine ("Aegis", "Talk", "Um...nothing..."));
		cutsceneScript.Add (new ScriptLine ("Minion", "Talk", "Riiiight."));
		cutsceneScript.Add (new ScriptLine ("Minion", "Move", new Vector2(-126.5f, -168f)));
		cutsceneScript.Add (new ScriptLine ("Aegis", "Talk", "...come back"));
		cutsceneScript.Add (new ScriptLine ("Aegis", "Move", new Vector2(-127.98f, -168f)));
		cutsceneScript.Add (new ScriptLine("End"));
	}
}

