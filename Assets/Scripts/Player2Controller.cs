using UnityEngine;
using System.Collections;

public class Player2Controller : MonoBehaviour
{

    [SerializeField]
    private float speed;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal, vertical;
        horizontal = Input.GetAxis("Horizontal2");
        vertical = Input.GetAxis("Vertical2");


        transform.position +=
            (horizontal * Vector3.right + vertical * Vector3.forward).normalized * speed * Time.deltaTime;
    }
}
