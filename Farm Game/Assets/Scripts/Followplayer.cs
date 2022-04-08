using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followplayer : MonoBehaviour
{
    public float interpVelocity;
	public Transform target;
	public Vector3 offset;

    public float minX;
    public float maxX;
    public float minY;   
    public float maxY;
	
	// Use this for initialization
	void Start () 
    {
		transform.position = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (target)
		{
		 
			transform.position = Vector3.Lerp( transform.position, target.position + offset, interpVelocity * Time.fixedDeltaTime);

		}

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
	}
}
