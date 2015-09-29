using UnityEngine;
using System.Collections;

public class PlayerControllerBoids : MonoBehaviour {

    public float speed;
    public Flock_Boids_Manager manager;

    private int boidsEaten;
    private bool dinnerTime;


	// Use this for initialization
	void Start () 
    {
        dinnerTime = false;
        boidsEaten = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (Application.loadedLevelName == "Evade") Application.LoadLevel("Flee");
            if (Application.loadedLevelName == "Seek") Application.LoadLevel("Evade");
            if (Application.loadedLevelName == "Pursuit") Application.LoadLevel("Seek");
            if (Application.loadedLevelName == "Interpose") Application.LoadLevel("Pursuit");
            if (Application.loadedLevelName == "Flocking") Application.LoadLevel("Interpose");

        }
        if(Input.GetKeyDown(KeyCode.RightBracket))
        {
            if (Application.loadedLevelName == "Flee") Application.LoadLevel("Evade");
            if (Application.loadedLevelName == "Evade") Application.LoadLevel("Seek");
            if (Application.loadedLevelName == "Seek") Application.LoadLevel("Pursuit");
            if (Application.loadedLevelName == "Pursuit") Application.LoadLevel("Interpose");
            if (Application.loadedLevelName == "Interpose") Application.LoadLevel("Flocking");
        }


        float horizontal, vertical;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) manager.chase = !(manager.chase);

        if (Input.GetKeyDown(KeyCode.KeypadMinus) && manager.dodgeRadius > 10) manager.dodgeRadius -= 5;
        if (Input.GetKeyDown(KeyCode.KeypadPlus) && manager.dodgeRadius < 50) manager.dodgeRadius += 5;

        if (Input.GetKeyDown(KeyCode.E))
        {
            dinnerTime = !dinnerTime;
            if (dinnerTime && manager.dodgeRadius < 40) manager.dodgeRadius = 40;
        }

        transform.position += 
            (horizontal * Vector3.right + vertical * Vector3.forward).normalized * speed * Time.deltaTime;


        transform.localScale = Vector3.one * (1 + boidsEaten);
	}

    void OnTriggerEnter(Collider other)
    {
        if (dinnerTime)
        {
            boidsEaten++;
            manager.dodgeRadius += manager.dodgeRadius * .01f;
            speed -= speed * .01f;
            //Debug.Log("Eating Boid #" + boidsEaten);
            manager.eatBoid(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
