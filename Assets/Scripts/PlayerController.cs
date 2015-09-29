using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed;



	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (Application.loadedLevelName == "Evade") Application.LoadLevel("Flee");
            if (Application.loadedLevelName == "Seek") Application.LoadLevel("Evade");
            if (Application.loadedLevelName == "Pursuit") Application.LoadLevel("Seek");
            if (Application.loadedLevelName == "Interpose") Application.LoadLevel("Pursuit");
            if (Application.loadedLevelName == "Flocking") Application.LoadLevel("Interpose");

        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
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


        transform.position += 
            (horizontal * Vector3.right + vertical * Vector3.forward).normalized * speed * Time.deltaTime;
	}
}
