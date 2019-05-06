using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float health = 10.0f;
    public GameObject StatsObject;
    // Update is called once per frame

    private void Start() {
        StatsObject = GameObject.Find("Stats");
    }
    void Update() {
        if (health <= 0) {
            StatsObject.GetComponent<statsController>().enemiesKilled++;
            Destroy(transform.parent.gameObject);
            Debug.Log("Death");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Bullet")) {
            Debug.Log("Enemy danmaged");
            Fire damageTaken = other.gameObject.GetComponent<Fire>();
            health -= damageTaken.damage;
        }
    }
    public void Damage(int Damage) {
        health -= Damage;
    }
}