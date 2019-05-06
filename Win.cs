using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public bool win = false;
    private float time = 0;
    public float winTime = 10f;
    public GameObject wonText;
    public GameObject statsText;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            win = true;
            time = Time.deltaTime;
            winTime = Time.deltaTime + winTime;
            wonText.transform.localPosition = new Vector3(0, 0, 0);
            statsText.transform.localPosition = new Vector3(-250, -300, 0);
        }
    }
    private void Update() {
        if (win) {
            if (time > winTime) {
                Application.Quit();
            }
            time += Time.deltaTime;
        }
    }
}
