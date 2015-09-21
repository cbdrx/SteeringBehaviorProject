using UnityEngine;
using System.Collections;

public class PursuitBehavior : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float pursuitSpeed;
    [SerializeField]
    private float leadOff;
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
        //if 
        if (Vector3.Distance(target.position, transform.position) < criticalDistance)
        {
            //if the target is close enough, we want to move directly towards the target
            movement = (target.position - transform.position).normalized * pursuitSpeed * Time.deltaTime;
            Debug.Log("Going at him!");
        }
        else
        {
            //otherwise, we need to lead it off, minimizing the distance travelled
            targetMovement = target.position - targetLastPosition;
            targetHeading = target.position + targetMovement;
            interceptTarget = targetHeading + targetMovement * leadOff;
            movement = (interceptTarget - transform.position).normalized * pursuitSpeed * Time.deltaTime;      
        }

        transform.position += movement;
        targetLastPosition = target.position;
        
        Debug.DrawLine(transform.position, transform.position + movement.normalized * 10, Color.red);
	}
}