using UnityEngine;
using System.Collections;

public class WallAvoidance : MonoBehaviour {
    
    public float avoidanceStrength;
    public float anticipationLength;

    private Vector3 lastPosition;
    private Vector3 velocity;

	// Use this for initialization
	void Start () 
    {
        lastPosition = Vector3.zero;
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	
	}
}
