using UnityEngine;
using System.Collections;

public class SelfDestroyScript : MonoBehaviour 
{
	public float lifeTime;
	void Start () 
	{
		Destroy (gameObject, lifeTime);
	}
}
