using UnityEngine;
using System.Collections;

public class SeekBehavior : MonoBehaviour {

    [SerializeField]
    private Transform target;
    
    [SerializeField]
    private float seekSpeed;

    private Vector3 moveTo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        moveTo = target.position - transform.position;
        transform.position += seekSpeed * moveTo.normalized * Time.deltaTime;
	
	}
}
