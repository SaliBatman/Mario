using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
	// Start is called before the first frame update
	void OnCollisionEnter2D(Collision2D target)
	{
		if (target.gameObject.tag == Tags.PlayerTag)
		{
			// DAMAGE THE PLAYER
			//target.gameObject.GetComponent<PlayerDamage>().DealDamage();
		}
		gameObject.SetActive(false);
	}

}
