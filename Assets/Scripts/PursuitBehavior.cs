using UnityEngine;
using System.Collections;

public class PursuitBehavior : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float pursuitStrength;
    [SerializeField]
    private float criticalDistance;

    private Vector3 targetLastPosition;
    private Vector3 targetMovement;
    private Vector3 targetHeading;
    private Vector3 interceptTarget;
    private Vector3 movement;


	// Use this for initialization
	void Start () 
    {
        targetLastPosition = target.position;
	}
	

	void Update () 
    {  
        //otherwise, we need to lead it off, minimizing the distance travelled
        targetMovement = target.position - targetLastPosition;
        targetHeading = target.position + targetMovement;
        interceptTarget = targetHeading + targetMovement * pursuitStrength;
        movement = (interceptTarget - transform.position).normalized * speed * Time.deltaTime;      

        transform.position += movement;
        targetLastPosition = target.position;
        
        Debug.DrawLine(transform.position, transform.position + movement.normalized * 10, Color.red);
	}
}