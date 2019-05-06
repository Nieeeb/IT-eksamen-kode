using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 10;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0) {
            die();
        }
        if (Input.GetKeyUp(KeyCode.N)) {
            takeDamage(1);
        }
    }

    void die() {
        gameObject.SetActive(false);
    }

    void takeDamage(int damage) {
        currentHealth -= damage;
    }
}
