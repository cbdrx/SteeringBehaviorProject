using UnityEngine;
using System.Collections;

public class FlockManager : MonoBehaviour {

    public Transform target;
    public GameObject flockObject;
    public int numberOfFlocks, numberOfBoids;
    public bool chase;
    private Random r = new Random();

    private GameObject flockTemp;
    private int boidsLeft;
    private int BoidsInFlock;
    private GameObject[] flocks;
    private bool chaseLast;
	// Use this for initialization
	void Start () 
    {
        flocks = new GameObject[numberOfFlocks];

        boidsLeft = numberOfBoids;
        for(int i = 0; i < numberOfFlocks-1; i++)
        {
            flockTemp = Instantiate(flockObject);
            BoidsInFlock = (int) Random.Range(0,(boidsLeft / 2));
            flockTemp.GetComponent<Flock_Boids_Manager>().initialNumberOfBoids = BoidsInFlock;
            flockTemp.GetComponent<Flock_Boids_Manager>().target = target;
            boidsLeft -= BoidsInFlock;
            flocks[i] = flockTemp;
        }
        flockTemp = Instantiate(flockObject);
        flockTemp.GetComponent<Flock_Boids_Manager>().initialNumberOfBoids = boidsLeft;
        flockTemp.GetComponent<Flock_Boids_Manager>().target = target;
        flocks[numberOfFlocks - 1] = flockTemp;

	}

    // Update is called once per frame
    void Update() 
    {
        if (chase != chaseLast)
        {
            for (int i = 0; i < numberOfFlocks; i++)
            {
                flocks[i].GetComponent<Flock_Boids_Manager>().chase = chase;
            }
            chaseLast = chase;
        }
	}
}
