using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IndividualityBoid : MonoBehaviour {

    public float separationRadius;
    public float cohesionFactor;
    public float alignmentFactor;
    public float neighborhoodRadius;

    private Collider[] neighbors;
    private Vector3 separationResult;
    private Vector3 alignmentResult;
    private Vector3 cohesionResult;
    private Vector3 previousPosition;
    private Vector3 temp;
    private Vector3 apparentVelocity;
    private Vector3 velocity;
    private GameObject flockMate;
    private IndividualityBoid flockMateBoid;

	// Use this for initialization
	void Start () 
    {
        previousPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        neighbors = Physics.OverlapSphere(transform.position, neighborhoodRadius);
        separationResult = applySeparation(ref neighbors);
        cohesionResult = applyCohesion(ref neighbors);
        alignmentResult = applyCohesion(ref neighbors);

        velocity += separationResult + alignmentResult + cohesionResult;
        previousPosition = transform.position;
        transform.position += velocity * Time.deltaTime;

        apparentVelocity = (previousPosition - transform.position);
	}

    Vector3 applySeparation(ref Collider[] flock)
    {
        temp = Vector3.zero;
        if (flock.Length != 1)
        {
            foreach (Collider flockMateCollider in flock)
            {
                flockMate = flockMateCollider.gameObject;

                if (flockMate.GetInstanceID() != this.gameObject.GetInstanceID())
                {
                    if (Vector3.Distance(flockMate.transform.position, transform.position) < separationRadius)
                    {
                        temp = temp - (flockMate.transform.position - transform.position);
                    }
                }
            }
        }
        return temp;
    }

    Vector3 applyCohesion(ref Collider[] flock)
    {
        temp = Vector3.zero;
        if (flock.Length != 1)
        {
            foreach (Collider flockMateCollider in flock)
            {
                if (flockMate.GetInstanceID() != this.gameObject.GetInstanceID())
                {
                    temp += flockMate.transform.position;
                }
            }
            temp = temp / (flock.Length - 1);
            return (temp - transform.position) * cohesionFactor;
        }
        return temp;
    }

    Vector3 applyAlignment(ref Collider[] flock)
    {
        temp = Vector3.zero;
        if (flock.Length != 1)
        {
            foreach (Collider flockMateCollider in flock)
            {
                if (flockMate.GetInstanceID() != this.gameObject.GetInstanceID())
                {
                    temp += flockMate.GetComponent<IndividualityBoid>().getVelocity();
                }
            }
            temp = temp / (flock.Length - 1);
            return (temp - velocity) * alignmentFactor; //removing the "Velocity" part makes them swarm around each other / - theBoid.velocity
        }
        return temp;
    }

    public Vector3 getVelocity()
    {
        return apparentVelocity;
    }
    
    
}
