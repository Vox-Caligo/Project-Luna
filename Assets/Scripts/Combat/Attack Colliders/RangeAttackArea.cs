using UnityEngine;
using System.Collections;

public class RangeAttackArea : DamageArea {
    public Vector2 endLoc;
	private bool hitObject = false;

	// AOE Stuff
	private float aoeSize = -1;
	private bool aoeGrowing = true;
	private float explosionSpeed = 8;
	private Vector2 aoeMaxSize;

    protected override void Update ()
	{
        Vector2 currentLocation = this.gameObject.transform.position;

		if(Vector2.Distance(currentLocation, endLoc) < .5f || hitObject) {
			finishAttack ();
        } else {
            Vector2 directionOfTravel = endLoc - currentLocation;
            directionOfTravel.Normalize();

            this.gameObject.transform.Translate(
                directionOfTravel.x * 1 * Time.deltaTime,
                directionOfTravel.y * 1 * Time.deltaTime,
                0, Space.World);
        }
    }

	protected override void finishAttack() {
		hitObject = true;

		if (weapon.AOE != -1) {
			if (aoeSize == -1) {
				this.gameObject.GetComponent<BoxCollider2D> ().size = new Vector2 (1, 1);
				aoeSize = 3;
				aoeMaxSize = new Vector2 (weapon.AOE, weapon.AOE);
			}

			if (aoeGrowing) {
				this.gameObject.transform.localScale = Vector2.MoveTowards(transform.localScale, aoeMaxSize, explosionSpeed * Time.deltaTime);
				if (this.gameObject.transform.localScale.magnitude == aoeMaxSize.magnitude) {
					aoeGrowing = false;
				}
			} else {
				this.gameObject.transform.localScale = Vector2.MoveTowards(transform.localScale, Vector2.zero, explosionSpeed * Time.deltaTime);
				if (this.gameObject.transform.localScale.magnitude == Vector2.zero.magnitude) {
					Destroy(this.gameObject);
				}
			}
		} else {
			Destroy(this.gameObject);
		}
	}
}
	