using UnityEngine;
using System.Collections;

public class Flock_Boids_Manager : MonoBehaviour {

    public float separationRadius;

    public float alignmentStrength;
    //Unintuitively, higher cohesionStrength leads to a less clustered flock
    public float cohesionStrength;

    public int initialNumberOfBoids;
    public Transform boid;

    private Transform[] flock;
    private Vector3 separationResult;
    private Vector3 alignmentResult;
    private Vector3 cohesionResult;
    private Vector3 temp;
    


	// Use this for initialization
	void Start () 
    {
        flock = new Transform[initialNumberOfBoids];
        for (int i = 0; i < initialNumberOfBoids; i++ )
        {
            flock[i] = Instantiate(boid);
        }	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    foreach (Transform member in flock)
        {
            separationResult = applySeparation(member);
            alignmentResult = applyAlignment(member);
            cohesionResult = applyCohesion(member);

            
        }
	}

    Vector3 applySeparation(Transform theBoid)
    {
        temp = Vector3.zero;

        foreach (Transform flockMate in flock)
        {
            if(flockMate != theBoid)
            {
                if(Vector3.Distance(theBoid.position,flockMate.position) < separationRadius)
                {
                    temp = temp - (flockMate.position - theBoid.position);
                }
            }
        }

        return temp;
    }

    Vector3 applyCohesion(Transform theBoid)
    {
        temp = Vector3.zero;

        foreach (Transform flockmate in flock)
        {
            if(flockmate != theBoid)
            {
                temp += flockmate.position;
            }
        }
        temp = temp / (flock.GetLength(0) -1);
        return (temp - theBoid.position) / cohesionStrength;
    }
    
    Vector3 applyAlignment(Transform theBoid)
    {
        return Vector3.zero;
    }
}
