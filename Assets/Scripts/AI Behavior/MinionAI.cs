using UnityEngine;
using System.Collections;

public class MinionAI : DefaultAI
{
	// Use this for initialization
	public MinionAI() : base() {}
	
	protected override void processDecisions ()
	{
		print ("I am a minion making minion decisions");
	}
}

