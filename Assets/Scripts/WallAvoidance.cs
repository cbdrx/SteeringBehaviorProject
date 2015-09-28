using UnityEngine;
using System.Collections;

public class WallAvoidance : MonoBehaviour {
    
    public float avoidanceStrength;
    public float anticipationLength;
    public string wallTag;

    private Vector3 lastPosition;
    private Vector3 perceivedVelocity;
    private Ray rayToCast;
    private RaycastHit hit;
    private Vector3 heading;

	// Use this for initialization
	void Start () 
    {
        lastPosition = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        lastPosition = transform.position;
        heading = transform.position + perceivedVelocity;

        rayToCast = new Ray(transform.position, heading);
        if (Physics.Raycast(rayToCast, out hit, anticipationLength, 1 << 8))
        {
                Vector3 moveAwayVector = Vector3.Cross((heading),transform.up).normalized;
                moveAwayVector = moveAwayVector - new Vector3(0,moveAwayVector.y,0);
                transform.position += moveAwayVector * avoidanceStrength * Time.deltaTime;
                Debug.Log("Hit Wall!");
        }
	    
        
	}

    void LateUpdate ()
    {
        perceivedVelocity = lastPosition - transform.position;
    }
}
