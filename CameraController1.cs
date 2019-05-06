using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    public GameObject Player;

    public int deadzone = 5;
    public int speed = 5;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x < Player.transform.position.x + deadzone || transform.position.x > Player.transform.position.x - deadzone) {
            Vector3 moveX = new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0);
            rb.AddForce(moveX);
        }
        if (transform.position.z < Player.transform.position.z + deadzone || transform.position.z > Player.transform.position.z - deadzone) {
            Vector3 moveX = new Vector3(0, 0, Input.GetAxis("Vertical") * speed);
            rb.AddForce(moveX);
        }

        /*
        if(transform.position.x < Player.transform.position.x - deadzone) {
            rb.AddForce(new Vector3(speed * (System.Math.Abs(Player.transform.position.x - transform.position.x) - deadzone), 0, 0));
        }
        if (transform.position.x > Player.transform.position.x + deadzone) {
            rb.AddForce(new Vector3(-speed * (System.Math.Abs(Player.transform.position.x - transform.position.x) - deadzone), 0, 0));
        }
        if (transform.position.z < Player.transform.position.z - deadzone) {
            rb.AddForce(new Vector3(0, 0, speed * (System.Math.Abs(Player.transform.position.z - transform.position.z) - deadzone)));
        }
        if (transform.position.z > Player.transform.position.z + deadzone) {
            rb.AddForce(new Vector3(0, 0, -speed * (System.Math.Abs(Player.transform.position.z - transform.position.z) - deadzone)));
        }
        */
    }
}
