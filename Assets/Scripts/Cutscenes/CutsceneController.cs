using UnityEngine;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
	private DialogueControllerCutscene dialogueController;
	private CutsceneTrigger currentCutscene;
	private KeyboardInput keyChecker;
	private ArrayList currentScript;
	private int scriptRunner;

	private bool inMovement = false;
	private bool inDialogue = false;
	private bool inAction = false;

	void Start() {
		dialogueController = GameObject.Find("Dialogue Controller").GetComponent<DialogueControllerCutscene>();
		keyChecker = GameObject.Find ("Databases").GetComponent<KeyboardInput> ();
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
			if(inMovement) {
				
			} else if(inDialogue) {
				if(dialogueController.CompletedTalkingPoint && keyChecker.useKey (KeyCode.E)) {
					inDialogue = false;
					dialogueController.endCurrentDialogue();
				}
			} else if(inAction) {

			} else {
				ScriptLine nextLine = (ScriptLine)currentScript [scriptRunner];
				scriptRunner++;

				if (nextLine.Action == "Talk") {
					inDialogue = true;
					characterTalking (nextLine);
				} else if (nextLine.Action == "Special") {
					dialogueController.endCutsceneDialogue();
					inAction = true;
					characterSpecialAction (nextLine);
				} else if (nextLine.Action == "Moving") {
					dialogueController.endCutsceneDialogue();
					inMovement = true;
					characterMoving (nextLine);
				}
			}
		}
	}

	// used to have a character have dialogue
	private void characterTalking(ScriptLine currentLine) {
		dialogueController.displayCutsceneDialogue(new TalkingCharacterInformation (currentLine.Character, currentLine.LineOrAct));
	}

	// used to move a character from place to place in a certain time
	private void characterMoving(ScriptLine currentLine) {

	}

	// used for things like pointing a weapon or disappearing
	private void characterSpecialAction(ScriptLine currentLine) {
		if (currentLine.LineOrAct == "End") {
			Destroy (currentCutscene);
		}
	}
}

