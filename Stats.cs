using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    // Start is called before the first frame update
    public Text ShotsFired;
    public Text BulletsWasted;
    public Text EnemiesKilled;
    public Text Gametime;
    public Text Meleehits;
    public Text MeleeMisses;
    public Text Blinks;

    public GameObject win;
    public GameObject player;

    private bool win2 = false;
    private statsController pc;
    void Start()
    {
        //pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(win.GetComponent<Win>().win && !win2) {
            string[] temp = Time.realtimeSinceStartup.ToString().Split(',');
            pc = player.GetComponent<statsController>();
            win2 = true;
            ShotsFired.text = "Shots Fired: " + pc.shotsFired;
            BulletsWasted.text = "Bullets Wasted: " + pc.shotsWasted;
            EnemiesKilled.text = "Enemies Killed: " + pc.enemiesKilled;
            Gametime.text = "Game Time: " + temp[0];
            Meleehits.text = "Melee Hits: " + pc.meleeHits;
            Blinks.text = "Blinks: " + pc.blinks;
            Debug.Log("Misses: " + pc.meleeMisses);
            MeleeMisses.text = "Melee Misses: " + pc.meleeMisses;
        }
    }
}
