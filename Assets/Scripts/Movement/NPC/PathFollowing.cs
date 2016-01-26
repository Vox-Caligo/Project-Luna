using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollowing : MonoBehaviour
{
		private List<Vector2> pathPoints;
		
		public PathFollowing(ArrayList pathPoints) {
			this.pathPoints = pathPoints;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}

