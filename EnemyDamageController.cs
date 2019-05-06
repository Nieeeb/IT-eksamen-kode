using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    public int damage = 5;
    public float attacksPerSecond = 2;
    public float timeSinceLastAttack = 0;
    public bool attackCharged = false;
    public bool readyToAttack = false;
    private EnemyAi aI;

    public AudioClip tone;
    public AudioSource source;
    public AudioReverbFilter reverb;


    private void Start() {
        aI = GetComponentInChildren<EnemyAi>();
        source.clip = tone;
        source.loop = true;
        source.Play();
    }
    private void Update() {
        if (timeSinceLastAttack <= 1 / attacksPerSecond) {
            attackCharged = false;
            timeSinceLastAttack += Time.deltaTime;
        }
        else {
            attackCharged = true;
        }
        if(aI.weeping == true) {
            if(attackCharged == true && aI.isObserved == false) {
                readyToAttack = true;
            } else {
                readyToAttack = false;
            }
        } else {
            if (attackCharged == true) {
                readyToAttack = true;
            } else {
                readyToAttack = false;
            }
        }
    }
}
    

