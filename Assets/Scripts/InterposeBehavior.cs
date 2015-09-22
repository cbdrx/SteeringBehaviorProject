using UnityEngine;
using System.Collections;

public class InterposeBehavior : MonoBehaviour {

    [SerializeField]
    private Transform target1;
    [SerializeField]
    private Transform target2;
    [SerializeField]
    private float speed;
    public float tolerance;

    //The weights are defaulted at 1. A heavier weight means it will be closer
    // to that object
    public float target1Weight;
    public float target2Weight;
    private float sumOfWeights;

    private Vector3 toMatch;
    private Vector3 movement;

	// Use this for initialization
	void Start () 
    {
        sumOfWeights = target1Weight + target2Weight;
	}
	
	// Update is called once per frame
	void Update () 
    {
        toMatch = //weighted average of the two points
            (target1Weight * target1.position +  target2Weight * target2.position)
            / sumOfWeights;

        movement = toMatch - transform.position;
        //If the point is outside the tolerable range, we want to move it
        //towards the correct position
        if(
               (transform.position.x < (toMatch.x - Mathf.Abs(toMatch.x * tolerance)))
            || (transform.position.x > (toMatch.x + Mathf.Abs(toMatch.x * tolerance)))
            || (transform.position.y < (toMatch.y - Mathf.Abs(toMatch.y * tolerance)))
            || (transform.position.y > (toMatch.y + Mathf.Abs(toMatch.y * tolerance))))
        {
            transform.position += movement.normalized * speed * Time.deltaTime;
        }
        else
        {
            transform.position = toMatch;
        }
	}
}
