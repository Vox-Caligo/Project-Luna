using UnityEngine;
using System.Collections;

/**
 * Follows a target point (pursue), rushes without changing direction (dash),
 * or runs away (flee)
 */
public class Teleport : BaseMovement
{
    // gets the current direction and start point
    private Vector2 currentDirectionVelocity = new Vector2();
    private Vector2 homePoint;

    // timer variables
    private float teleportTimeDelay = 2;
    private UtilTimer teleportDelayTimer;

    // sets the point where it starts
    public Teleport(GameObject character) : base(character) {
        homePoint = character.GetComponent<Rigidbody2D>().position;
        teleportDelayTimer = new UtilTimer(teleportTimeDelay, teleportTimeDelay);
    }

    public void targetedTeleport(Vector3 target, float teleportDistance, int direction) {
        Vector3 teleportLocation;

        if (direction == 0) {
            teleportLocation = new Vector3(target.x - teleportDistance, target.y);
        } else if(direction == 1) {
            teleportLocation = new Vector3(target.x, target.y + teleportDistance);
        } else if (direction == 2) {
            teleportLocation = new Vector3(target.x + teleportDistance, target.y);
        } else {
            teleportLocation = new Vector3(target.x, target.y - teleportDistance);
        }

        teleport(teleportLocation);
    }

    public void randomTeleport(float allowableDistance) {
        float randomX = Random.Range(-allowableDistance, allowableDistance);
        float randomY = Random.Range(-allowableDistance, allowableDistance);

        Vector3 teleportLocation = new Vector3(homePoint.x + randomX, homePoint.y + randomY);
        teleport(teleportLocation);
    }

    private void teleport(Vector3 newLocation) {
        if (!teleportDelayTimer.runningTimerCountdown()) {
            print("hit");
            character.transform.position = newLocation;
        }
    }
}