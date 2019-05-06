using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject player;
    public Transform healthBar;
    public Transform meleeBar;
    public float maxHealth;
    public float health;
    private float maxCharge;
    private PlayerController pC;
    private PlayerCollisionDetection pCD;

    // Start is called before the first frame update
    void Start(){
        pC = player.GetComponent<PlayerController>();
        pCD = player.GetComponent<PlayerCollisionDetection>();
        maxHealth = pCD.maxHealth;
        maxCharge = pC.maxDamage;
    }

    // Update is called once per frame
    void Update(){
        health = pCD.health;
        UpdateHealthBar();
    }
    void UpdateChargeBar(float currentCharge) {
        float chargeNormalized = (currentCharge - pC.startDamage) / (maxCharge - pC.startDamage);
        meleeBar.localScale = new Vector3(chargeNormalized, 1f, 1f);
    }

    void UpdateHealthBar() {
        float healtNormalized = health / maxHealth;
        healthBar.localScale = new Vector3(healtNormalized, 1f, 1f);
    }
}
