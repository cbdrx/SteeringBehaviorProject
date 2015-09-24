using UnityEngine;
using System.Collections;

public class PlayerControllerFlocks : MonoBehaviour
{

    public float speed;
    public FlockManager manager;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal, vertical;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) manager.chase = !(manager.chase);

        //if (Input.GetKeyDown(KeyCode.KeypadMinus) && manager.dodgeRadius > 10) manager.dodgeRadius -= 5;
        //if (Input.GetKeyDown(KeyCode.KeypadPlus) && manager.dodgeRadius < 50) manager.dodgeRadius += 5;

        transform.position +=
            (horizontal * Vector3.right + vertical * Vector3.forward).normalized * speed * Time.deltaTime;

    }
}
