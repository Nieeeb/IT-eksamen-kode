using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public float speed = 100f;
    public float lifeTime = 10f;
    public float damage = 5f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update() {
        lifeTime -= 1.0F * Time.deltaTime;
        rb.velocity = transform.forward * speed;
        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other) {
        if (!other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
