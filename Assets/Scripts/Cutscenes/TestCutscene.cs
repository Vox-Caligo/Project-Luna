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
		cutsceneScript.Add (new ScriptLine("Aegis", "Special", "End"));
	}
}

