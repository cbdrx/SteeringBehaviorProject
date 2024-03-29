﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flock_Boids_Manager : MonoBehaviour {

    public float separationRadius;
    public float flockRadius;
    public float boidSpeed;
    public Transform target;
    public float chaseStrength;
    public float dodgeStrength;
    public float dodgeRadius;
    public float detectWallDistance;
    public float wallDetectionStrength;
    public float wallMoveStrength;
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
    private List<algorithmBoid> flock;
    private Vector3 separationResult;
    private Vector3 alignmentResult;
    private Vector3 cohesionResult;
    private Vector3 chaseResult;
    private Vector3 dodgeResult;
    private Vector3 temp;
    private algorithmBoid tempBoid;
    
    

	// Use this for initialization
	void Start () 
    {
        chase = false;
        flock = new List<algorithmBoid>(initialNumberOfBoids);
        for (int i = 0; i < initialNumberOfBoids; i++ )
        {
            tempBoid.boidTransform = Instantiate(boid);
            tempBoid.boidTransform.parent = gameObject.transform;
            tempBoid.boidTransform.position = new Vector3(Random.Range(-flockRadius, flockRadius), 0.0f, Random.Range(-flockRadius, flockRadius));
            tempBoid.lastPosition = tempBoid.boidTransform.position;
            tempBoid.velocity = Vector3.zero;
            flock.Add(tempBoid);
        }	
	}
	
	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i < flock.Count; i++)
        {
            tempBoid = flock[i];
            separationResult = applySeparation(flock[i]);
            alignmentResult = applyAlignment(flock[i]);
            cohesionResult = applyCohesion(flock[i]);

            if (chase) chaseResult = applyChase(flock[i]);
            else chaseResult = Vector3.zero;
            dodgeResult = applyDodge(flock[i]);

            tempBoid.velocity += separationResult + alignmentResult + cohesionResult + chaseResult + dodgeResult;

            Ray findWall = new Ray(flock[i].boidTransform.position, flock[i].velocity);

            //If we would have hit a wall, we will move away from it, because that'd just be unboidlike
            RaycastHit myRaycastHit;
            if (Physics.Raycast(findWall, out myRaycastHit, detectWallDistance, 1 << 8))
            {
                Vector3 moveAwayVector = (flock[i].boidTransform.position - myRaycastHit.point).normalized;
                moveAwayVector = moveAwayVector - new Vector3(0, moveAwayVector.y, 0);
                tempBoid.velocity += moveAwayVector * wallDetectionStrength
                    + Vector3.Cross(flock[i].velocity, flock[i].boidTransform.up).normalized * wallMoveStrength;
                //gets a vector "sideways away" from the wall
            }
            tempBoid.lastPosition = flock[i].boidTransform.position;
            tempBoid.boidTransform.position += flock[i].velocity * boidSpeed * Time.deltaTime;
            flock[i] = tempBoid;

            //flock[i].lastPosition = flock[i].boidTransform.position;
        }
	}

    //Below are implementations of the boids algorithm functions provided to us

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
        return (temp - theBoid.velocity) / alignmentStrength; //removing the "Velocity" part makes them swarm around each other / - theBoid.velocity
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

    public void eatBoid(GameObject boid)
    {
        for(int i = 0; i < flock.Count; i++)
        {
            if(flock[i].boidTransform.gameObject.GetInstanceID() == boid.GetInstanceID())
            {
                flock.Remove(flock[i]);
            }
            boid.SetActive(false);
        }
    }

}
