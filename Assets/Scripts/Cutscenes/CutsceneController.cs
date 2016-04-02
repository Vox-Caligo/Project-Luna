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

	// the player
	PlayerMovement playerMovement;

	private GameObject currentActor;

	void Start() {
		dialogueController = GameObject.Find("Dialogue Controller").GetComponent<DialogueControllerCutscene>();
		keyChecker = GameObject.Find ("Databases").GetComponent<KeyboardInput> ();
		playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>().currentCharacterMovement();
	}

	public void initializeScript(CutsceneTrigger currentCutscene) {
		playerMovement.InCutscene = true;
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
		DefaultAI aiCharacter = currentActor.GetComponent<DefaultAI> ();

		// checks if it is an npc otherwise it's the player
		if (aiCharacter != null) {
			if(!inMovement) {
				aiCharacter.NpcMovement.TargetPoint = newLocation;
				aiCharacter.NpcMovement.CurrentAction = "pursue";
				aiCharacter.InCutscene = true;
			}
		} else {
			int newWalkingDirection;
			playerMovement.InCutscene = true;

			if(currentActor.transform.position.x != newLocation.x) {
				newWalkingDirection = currentActor.transform.position.x < newLocation.x ? 2 : 0;
			} else {
				newWalkingDirection = currentActor.transform.position.y < newLocation.y ? 1 : 3;
			}

			playerMovement.CharacterAnimator.walk(newWalkingDirection);
			currentActor.transform.position = Vector2.MoveTowards (currentActor.transform.position, newLocation, .02f);
		}

		if (Vector2.Distance (currentActor.transform.position, newLocation) < .02) {
			inMovement = false;

			if (aiCharacter != null) {
				aiCharacter.InCutscene = false;
			}
		}
	}

	private void endCutscene() {
		inMovement = false;
		inDialogue = false;
		inAction = false;
		currentCutscene = null;
		dialogueController.endCutsceneDialogue ();
		playerMovement.InCutscene = false;
		Destroy (currentCutscene);
	}

	public bool isCurrentlyInCutscene() {
		return (inMovement || inAction || inDialogue);
	}
}

