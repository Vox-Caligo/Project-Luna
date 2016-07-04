using UnityEngine;
using System.Collections;


public class ActivatedTerrainFeatures : MonoBehaviour
{
	// variables for the player
	private GameObject player;
	private PlayerMovement playerMovement;
	private PlayerCombat playerCombat;

	// variables for the terrain
	private TerrainPiece currentTerrain;
	private ColliderBoundingBox currentColliderBoundingBox;

	// grabs the player game object and the movement script
	public ActivatedTerrainFeatures(GameObject player, PlayerMovement playerMovement, PlayerCombat playerCombat) {
		this.player = player;
		this.playerMovement = playerMovement;
		this.playerCombat = playerCombat;
	}

    // activates the features of the current terrain
    public void activateTerrainFeature(TerrainPiece currentTerrain) {
        this.currentTerrain = currentTerrain;

        if (currentTerrain != null) {
            string currentTerrainType = currentTerrain.getTerrainType();
            // checks if the terrain causes sliding
            if (currentTerrainType == "slippery terrain") {
                slipperyTerrain();
            }

            if (currentTerrainType == "friction terrain") {
                frictionTerrain();
            }

            // checks if the terrain is a teleporter entrance
            if (currentTerrainType == "teleporter terrain") {
                teleporterTerrain();
            }

            // checks if the current terrain is the type to climb
            if (currentTerrainType == "climable terrain") {
                climableTerrain();
            }

            if (currentTerrainType == "speed manipulator terrain") {
                speedManipulationTerrain();
            }

            if (currentTerrainType == "health manipulator terrain") {
                healthManipulationTerrain();
            }

            if (currentTerrainType == "mana manipulator terrain") {
                manaManipulationTerrain();
            }

            if (currentTerrainType == "water terrain") {
                // clip away with an alpha mask from the bottom to somwhere in the middle
                // that or add a new sprite just for water
                // better way but more time consuming 
            }
        }
    }

    public void deactivateTerrainFeatures() {
        if (!playerMovement.IsSliding || !playerMovement.IsFrictionStopNeeded) {
            playerMovement.IsSliding = false;
            playerMovement.IsFrictionStopNeeded = false;
            playerMovement.TerrainModifier = 1;
        }

        playerMovement.IsClimbing = false;
        
        if (!playerCombat.Health.HealthRegeneration) {
            playerCombat.Health.HealthRegeneration = true;
        }

        if (!playerCombat.Mana.ManaRegeneration) {
            playerCombat.Mana.ManaRegeneration = true;
        }
    }

    private void teleporterTerrain() {
        if (((TeleporterTerrain)currentTerrain).sender && !((TeleporterTerrain)currentTerrain).TeleporterOnFreeze && ((TeleporterTerrain)currentTerrain).isSisterAReceiver()) {
            player.transform.position = ((TeleporterTerrain)currentTerrain).teleportCoordinates();
            int newTeleportDirection = ((TeleporterTerrain)currentTerrain).isSisterADirectional();

            if (newTeleportDirection != -1) {
                playerMovement.CurrentDirection = newTeleportDirection;
                playerMovement.CharacterAnimator.walk(playerMovement.CurrentDirection);
            }
        }
    }

    private void climableTerrain() {
        playerMovement.IsClimbing = true;
    }

    private void slipperyTerrain() {
        playerMovement.IsSliding = true;

        // checks if the terrain is sliding and needs a stop piece
        if (((SlipperyTerrain)currentTerrain).needsFrictionStop) {
            playerMovement.IsFrictionStopNeeded = true;
        }

        int xVelocity = 0, yVelocity = 0;

        if (playerMovement.CurrentDirection == 0) {
            xVelocity = -1;
        } else if (playerMovement.CurrentDirection == 1) {
            yVelocity = 1;
        } else if (playerMovement.CurrentDirection == 2) {
            xVelocity = 1;
        } else {
            yVelocity = -1;
        }

        playerMovement.SlideValue = new Vector2(xVelocity, yVelocity);
    }

    private void frictionTerrain() {
        playerMovement.IsSliding = false;
        playerMovement.IsFrictionStopNeeded = false;
    }

    private void speedManipulationTerrain() {
        if (((SpeedManipulatorTerrain)currentTerrain).isSlowdown) {
            playerMovement.TerrainModifier = ((SpeedManipulatorTerrain)currentTerrain).slowdownSpeed;
        } else if (((SpeedManipulatorTerrain)currentTerrain).isSpeedup) {
            playerMovement.TerrainModifier = ((SpeedManipulatorTerrain)currentTerrain).speedupSpeed;
        }
    }

    private void healthManipulationTerrain() {
        int healthManipulation = ((HealthManipulatorTerrain)currentTerrain).healthManipulation;
        float maxHealth = ((HealthManipulatorTerrain)currentTerrain).overboost ? playerCombat.Health.MaxHealth * 1.5f : playerCombat.Health.MaxHealth;
        playerCombat.Health.addHealth(healthManipulation, (int)maxHealth);
    }

    private void manaManipulationTerrain() {
        int manaManipulation = ((ManaManipulatorTerrain)currentTerrain).manaManipulation;
        float maxMana = ((ManaManipulatorTerrain)currentTerrain).overboost ? playerCombat.Mana.MaxMana * 1.5f : playerCombat.Mana.MaxMana;
        playerCombat.Mana.addMana(manaManipulation, maxMana);
    }
}