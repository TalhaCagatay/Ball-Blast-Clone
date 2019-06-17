using UnityEngine;
using System.Collections;

public class SelfDestroyScript : MonoBehaviour 
{
	
	void Start () 
	{
		Destroy (gameObject, 3.0f);
	}
}
