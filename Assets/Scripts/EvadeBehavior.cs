using UnityEngine;
using System.Collections;

public class EvadeBehavior : MonoBehaviour {

    public Transform target;
    public float EvadeStrength;
    public float Speed;

    private Vector3 moveTo;
    private Vector3 targetPreviousPosition;
    private Vector3 targetHeading;
    private Vector3 targetPredictedPos;
    private Vector3 targetMovement;

	// Use this for initialization
	void Start () 
    {
        targetPreviousPosition = target.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        targetMovement = (target.position - targetPreviousPosition);
        targetHeading = target.position - targetMovement;

        moveTo = transform.position - (target.position + targetMovement * EvadeStrength);




        transform.position += moveTo.normalized * Speed * Time.deltaTime;
        targetPreviousPosition = target.position;

        Debug.DrawLine(transform.position, transform.position + moveTo.normalized * 10, Color.red);
        Debug.DrawLine(transform.position, target.position, Color.green);
	}
}
