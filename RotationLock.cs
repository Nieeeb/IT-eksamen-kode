﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLock : MonoBehaviour
{
    public GameObject player;
    public int height = 15;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void FixedUpdate() {
       Camera.main.transform.position = new Vector3(player.transform.position.x,player.transform.position.y+height, player.transform.position.z);
    }
}
