using UnityEngine;
using System.Collections;

public class RangeAttackArea : DamageArea {

    // range area
    public Vector2 endLoc;
	protected float attackDistance;
    
	public RangeAttackArea()
	{	
		//attackDistance = weapon.ShotDistance;
	}

    protected override void Update ()
	{
        Vector2 currentLocation = this.gameObject.transform.position;

        if(Vector2.Distance(currentLocation, endLoc) < .5f) {
            if (weapon.AOE != -1) {
                this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(weapon.AOE, weapon.AOE);
            } else {
                Destroy(this.gameObject);
            }
        } else {
            Vector2 directionOfTravel = endLoc - currentLocation;
            directionOfTravel.Normalize();

            this.gameObject.transform.Translate(
                directionOfTravel.x * 1 * Time.deltaTime,
                directionOfTravel.y * 1 * Time.deltaTime,
                0, Space.World);
        }
    }
}

