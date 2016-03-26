using UnityEngine;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
	private DialogueControllerCutscene dialogueController;
	private CutsceneTrigger currentCutscene;
	private ArrayList currentScript;
	private int scriptRunner;

	void Start() {
		dialogueController = GameObject.Find("Dialogue Controller").GetComponent<DialogueControllerCutscene>();
	}

	public void initializeScript(CutsceneTrigger currentCutscene) {
		this.currentCutscene = currentCutscene;
		this.currentScript = this.currentCutscene.CutsceneScript;
		scriptRunner = 0;
	}

	// Update is called once per frame
	protected virtual void Update ()
	{
		if(currentCutscene != null) {
			ScriptLine nextLine = (ScriptLine)currentScript [scriptRunner];

			if (nextLine.Action == "Talk") {
				characterTalking (nextLine);
			} else if (nextLine.Action == "Special") {
				characterSpecialAction (nextLine);
			} else if (nextLine.Action == "Moving") {
				characterMoving (nextLine);
			}
		}
	}

	// used to have a character have dialogue
	public void characterTalking(ScriptLine currentLine) {
		print (currentLine.Character);
		dialogueController.displayCutsceneDialogue(new TalkingCharacterInformation (currentLine.Character, currentLine.LineOrAct));
		//scriptRunner++;
	}

	// used to move a character from place to place in a certain time
	public void characterMoving(ScriptLine currentLine) {

	}

	// used for things like pointing a weapon or disappearing
	public void characterSpecialAction(ScriptLine currentLine) {
		if (currentLine.LineOrAct == "End") {
			Destroy (currentCutscene);
		}
	}
}

