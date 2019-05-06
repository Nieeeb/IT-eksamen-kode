using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onload : MonoBehaviour
{
    private float time = 0f;
    public float textTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        time = Time.deltaTime;
        textTime = Time.deltaTime + textTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > textTime) {
            Destroy(gameObject);
        }
        time += Time.deltaTime;
    }
}
