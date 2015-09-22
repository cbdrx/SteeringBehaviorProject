using UnityEngine;
using System.Collections;

public class Flock_Boids_Manager : MonoBehaviour {

    public float separationRadius;
    public float flockRadius;
    public float boidSpeed;
    public Transform target;
    public float chaseStrength;
    public float dodgeStrength;
    public float dodgeRadius;
    public bool chase;

    public float alignmentStrength;
    //Unintuitively, higher cohesionStrength leads to a less clustered flock
    public float cohesionStrength;

    public int initialNumberOfBoids;
    public Transform boid;

    public struct algorithmBoid
    {
        public Transform boidTransform;
        public Vector3 lastPosition;
        public Vector3 velocity;
    }
    private algorithmBoid[] flock;
    private Vector3 separationResult;
    private Vector3 alignmentResult;
    private Vector3 cohesionResult;
    private Vector3 chaseResult;
    private Vector3 dodgeResult;
    private Vector3 temp;
    


	// Use this for initialization
	void Start () 
    {
        chase = true;
        flock = new algorithmBoid[initialNumberOfBoids];
        for (int i = 0; i < initialNumberOfBoids; i++ )
        {
            flock[i].boidTransform = Instantiate(boid);
            flock[i].boidTransform.parent = gameObject.transform;
            flock[i].boidTransform.position = new Vector3(Random.Range(-flockRadius, flockRadius), 0.5f, Random.Range(-flockRadius, flockRadius));
            flock[i].lastPosition = flock[i].boidTransform.position;
            flock[i].velocity = Vector3.zero;
        }	
	}
	
	// Update is called once per frame
	void Update () 
    {
        for(int i= 0; i < initialNumberOfBoids; i++)
        {
            separationResult = applySeparation(flock[i]);
            alignmentResult = applyAlignment(flock[i]);
            cohesionResult = applyCohesion(flock[i]);

            if(chase) chaseResult = applyChase(flock[i]);
            else chaseResult = Vector3.zero;

            dodgeResult = applyDodge(flock[i]);

            //flock[i].velocity = Vector3.zero; //added this to test something : results - no benefit
            //flock[i].velocity = (flock[i].boidTransform.position - flock[i].lastPosition); //For some reason, removing this line makes it work much better. What.
            flock[i].velocity += separationResult + alignmentResult + cohesionResult + chaseResult + dodgeResult;
            //Debug.DrawLine(flock[i].boidTransform.position, flock[i].boidTransform.position + flock[i].velocity,Color.red);
            flock[i].boidTransform.position += flock[i].velocity * boidSpeed* Time.deltaTime;
            
            flock[i].lastPosition = flock[i].boidTransform.position;
        }
	}

    Vector3 applySeparation(algorithmBoid theBoid)
    {
        temp = Vector3.zero;

        foreach (algorithmBoid flockMate in flock)
        {
            if (flockMate.boidTransform.gameObject.GetInstanceID() != theBoid.boidTransform.gameObject.GetInstanceID())
            {
                if(Vector3.Distance(theBoid.boidTransform.position,flockMate.boidTransform.position) < separationRadius)
                {
                    temp = temp - (flockMate.boidTransform.position - theBoid.boidTransform.position);
                }
            }
        }

        return temp;
    }

    Vector3 applyCohesion(algorithmBoid theBoid)
    {
        temp = Vector3.zero;

        foreach (algorithmBoid flockmate in flock)
        {
            if(flockmate.boidTransform.gameObject.GetInstanceID() != theBoid.boidTransform.gameObject.GetInstanceID())
            {
                temp += flockmate.boidTransform.position;
            }
        }
        temp = temp / (initialNumberOfBoids -1);
        return (temp - theBoid.boidTransform.position) / cohesionStrength;
    }
    
    Vector3 applyAlignment(algorithmBoid theBoid)
    {
        temp = Vector3.zero;
        foreach( algorithmBoid flockmate in flock)
        {
            if (flockmate.boidTransform.gameObject.GetInstanceID() != theBoid.boidTransform.gameObject.GetInstanceID())
            {
                temp += (flockmate.boidTransform.position - flockmate.lastPosition);
            }
        }
        temp = temp / (initialNumberOfBoids - 1);
        return (temp - theBoid.velocity) / alignmentStrength; //removing the "Velocity" part makes them swarm around each other
    }
    
    Vector3 applyChase(algorithmBoid theBoid)
    {
        temp = Vector3.zero;

        temp = target.position - theBoid.boidTransform.position;

        return temp.normalized * chaseStrength;

    }

    Vector3 applyDodge(algorithmBoid theBoid)
    {
        temp = Vector3.zero;

        if(Vector3.Distance(theBoid.boidTransform.position, target.position) < dodgeRadius)
        {
            temp = theBoid.boidTransform.position - target.position;
        }
        return temp.normalized * dodgeStrength;
    }

}
