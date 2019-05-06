using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour {
    private PlayerController pC;
    public float maxHealth = 20f;
    public float health = 0f;
    private bool oneTime = false;
    private bool oneTime2 = false;
    private float time = 0;
    private float time2 = 0;

    // Start is called before the first frame update
    void Start() {
        pC = GetComponent<PlayerController>();
        health = maxHealth;
    }
    private void Update() {
        time += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ammo")) {
            Destroy(other.gameObject);
            if (!oneTime2) {
                oneTime2 = true;
                time = 0f;
                int ammoToAdd = other.gameObject.GetComponent<AmmoConsumableController>().ammoCount;
                pC.ammo += ammoToAdd;
                Debug.Log("AmmoToAdd: " + ammoToAdd);
                
                pC.UpdateUI();
            }
            if (time > 2f) {
                Debug.Log("Hek");
                oneTime2 = false;
            } 
            
        }
        if (other.gameObject.CompareTag("Enemy")) {
            EnemyDamageController dmgCon = other.gameObject.GetComponent<EnemyDamageController>();
            if (dmgCon.readyToAttack){
                int damageToDo = dmgCon.damage;
                if (health >= damageToDo) {
                    health -= damageToDo;
                    dmgCon.attackCharged = false;
                    dmgCon.readyToAttack = false;
                    dmgCon.timeSinceLastAttack = 0.0f;
                }
                else {
                    if ((health - damageToDo) <= 0) {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
