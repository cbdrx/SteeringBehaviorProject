using UnityEngine;
using System.Collections;

public class FleeBehavior : MonoBehaviour {

    public Transform target;
    public float FleeStrength;

    private Vector3 moveTo;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        moveTo = transform.position - target.position;
        transform.position += moveTo.normalized * FleeStrength * Time.deltaTime;
        Debug.DrawLine(transform.position, transform.position + moveTo.normalized * 10, Color.red);
	}
}
