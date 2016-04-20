using UnityEngine;
using System.Collections;

/**
 * A line of the cutscene script
 */
public class ScriptLine {
	public string Character { get; set; }
	public string Action { get; set; }
	public string LineOrAct { get; set; }
	public Vector2 NewLocation { get; set; }

	public ScriptLine(string action) {
		this.Action = action;
	}

	// used for special action or dialogue
	public ScriptLine(string character, string action, string lineOrAct) {
		this.Character = character;
		this.Action = action;
		this.LineOrAct = lineOrAct;
	}

	// used for movement
	public ScriptLine(string character, string action, Vector2 newLocation) {
		this.Character = character;
		this.Action = action;
		this.NewLocation = newLocation;
	}
}

/**
 * A location that, when collided with, starts a cutscene
 */
public class CutsceneTrigger : MonoBehaviour
{
	// variables for running through a cutscene
	protected bool cutsceneActivated = false;
	protected CutsceneController controller;
	protected ArrayList cutsceneScript;

	// Use this for initialization
	protected virtual void Start ()
	{
		controller = GameObject.Find ("Databases").GetComponent<CutsceneController> ();
		setCutsceneScript ();
	}

	// sets a cutscene
	protected virtual void setCutsceneScript() { }

	// launches a cutscene
	public virtual void startCutscene() {
		cutsceneActivated = true;
		controller.initializeScript (this);
	}

	// whether the cutscene is activated
	public bool CutsceneActivated {
		get { return cutsceneActivated; }
	}

	// the script for the cutscene
	public ArrayList CutsceneScript {
		get { return cutsceneScript; }
	}
}

