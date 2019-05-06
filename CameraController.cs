using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    public int deadzone = 5;
    public int speed = 5;
    public int deadzoneStart = 2;
    public float returnSpeed = 2;
    public bool cameraMode = true;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {

        if (cameraMode) {
            if (transform.position.x < Player.transform.position.x - deadzone) {
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
        } else {
            bool horizontal = true;
            if (transform.position.x < Player.transform.position.x + deadzone && Input.GetAxis("Horizontal") > 0) {
                Vector3 moveX;
                if ((Player.transform.position.x - transform.position.x) < deadzoneStart) {
                    moveX = new Vector3(System.Math.Abs((deadzone - deadzoneStart)/ Player.transform.position.x - transform.position.x) * speed, 0, 0);
                } else {
                    moveX = new Vector3(1 * speed, 0, 0);
                }
                rb.AddForce(moveX);
                horizontal = false;
            }
            if(transform.position.x > Player.transform.position.x - deadzone && Input.GetAxis("Horizontal") < 0) {
                Vector3 moveX = new Vector3(-1 * speed, 0, 0);
                rb.AddForce(moveX);
                horizontal = false;
            }
            bool vertical = true;
            if (transform.position.z < Player.transform.position.z + deadzone && Input.GetAxis("Vertical") > 0) {
                Vector3 moveX = new Vector3(0, 0, 1 * speed);
                rb.AddForce(moveX);
                vertical = false;
            }
            if(transform.position.z > Player.transform.position.z - deadzone && Input.GetAxis("Vertical") < 0){
                Vector3 moveX = new Vector3(0, 0, -1 * speed);
                rb.AddForce(moveX);
                vertical = false;
            }
            if (vertical && Player.transform.position.x != transform.position.x) {
                rb.AddForce(new Vector3((Player.transform.position.x - transform.position.x)*returnSpeed, 0, 0));
            }
            if (horizontal && Player.transform.position.z != transform.position.z) {
                rb.AddForce(new Vector3(0, 0, (Player.transform.position.z - transform.position.z)*returnSpeed));
            }
        }
    }
}
