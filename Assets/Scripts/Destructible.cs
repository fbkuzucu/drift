using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public GameObject destroyedVersion;	// Reference to the shattered version of the object

	
	void OnCollisionEnter ()
	{
		Instantiate(destroyedVersion, transform.position, transform.rotation);
		Destroy(gameObject);
	}

}
