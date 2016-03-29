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

	private Vector2 newLocation;

	private GameObject currentActor;

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
				characterMoving ();
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
				} else if (nextLine.Action == "Move") {
					currentActor = GameObject.Find (nextLine.Character);
					dialogueController.endCutsceneDialogue ();
					newLocation = nextLine.NewLocation;
					characterMoving ();
					inMovement = true;
				} else if (nextLine.Action == "End") {
					endCutscene();
				} else {
				}
			}
		}
	}

	// used to have a character have dialogue
	private void characterTalking(ScriptLine currentLine) {
		dialogueController.displayCutsceneDialogue(new TalkingCharacterInformation (currentLine.Character, currentLine.LineOrAct));
	}

	// used to have a character move
	private void characterMoving() {
		if (currentActor.GetComponent<DefaultMovementController> () != null && !inMovement) {
			DefaultMovementController currentActorMovement = currentActor.GetComponent<DefaultMovementController> ();
			currentActorMovement.TargetPoint = newLocation;
			currentActorMovement.CurrentAction = "pursue";
			print ("Assumptions are made this works...need to really animation test this...");
		} else {
			print ("This is for the player");
			currentActor.transform.position = Vector2.MoveTowards (currentActor.transform.position, newLocation, .02f);
		}

		if (Vector2.Distance (currentActor.transform.position, newLocation) < .02) {
			inMovement = false;
		}
	}

	private void endCutscene() {
		inMovement = false;
		inDialogue = false;
		inAction = false;
		currentCutscene = null;
		dialogueController.endCutsceneDialogue ();
		Destroy (currentCutscene);
	}

	public bool isCurrentlyInCutscene() {
		return (inMovement || inAction || inDialogue);
	}
}

