using UnityEngine;
using System.Collections;

public class ScriptLine {
	public string Character { get; set; }
	public string Action { get; set; }
	public string LineOrAct { get; set; }
	public Vector2 NewLocation { get; set; }

	public ScriptLine(string action) {
		this.Action = action;
	}

	public ScriptLine(string character, string action, string lineOrAct) {
		this.Character = character;
		this.Action = action;
		this.LineOrAct = lineOrAct;
	}

	public ScriptLine(string character, string action, Vector2 newLocation) {
		this.Character = character;
		this.Action = action;
		this.NewLocation = newLocation;
	}
}

public class CutsceneTrigger : MonoBehaviour
{
	protected bool cutsceneActivated = false;
	protected CutsceneController controller;
	protected ArrayList cutsceneScript;

	// Use this for initialization
	protected virtual void Start ()
	{
		controller = GameObject.Find ("Databases").GetComponent<CutsceneController> ();
		setCutsceneScript ();
	}

	protected virtual void setCutsceneScript() { }

	public virtual void startCutscene() {
		cutsceneActivated = true;
		controller.initializeScript (this);
	}

	public bool CutsceneActivated {
		get { return cutsceneActivated; }
	}

	public ArrayList CutsceneScript {
		get { return cutsceneScript; }
	}
}

