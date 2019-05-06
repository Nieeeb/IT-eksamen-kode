using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerRev2 : MonoBehaviour{
    public float Speed = 1F;

    public float End = 10F;
    public GameObject parent;

    private float startTime;

    private Vector3 startPos = new Vector3(0, 10, 0);

    private Vector3 finalDesVector = new Vector3(0, 10, 0);

    float globalJourneyLength = 0F;

    private bool ready1 = true;
    private bool ready2 = true;
    private bool ready3 = true;
    private bool ready4 = true;

    private Vector3 Ppos;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    void Update() {
        Ppos = parent.transform.position;
        finalDesVector = new Vector3(0, 10, 0);

        if (Input.GetAxis("Horizontal") > 0) {
            finalDesVector = finalDesVector + new Vector3(End, 0, 0);
            if (ready1) {
                startTime = Time.time;
                startPos = Ppos - transform.position;
                ready1 = false;
            } else {
                ready1 = true;
            }
        }
        if (Input.GetAxis("Horizontal") < 0) {
            finalDesVector = finalDesVector + new Vector3(-End, 0, 0);
            if (ready2) {
            startTime = Time.time;
            startPos = Ppos - transform.position;
                ready2 = false;
            } else {
                ready2 = true;
            }
        }
        if (Input.GetAxis("Vertical") > 0) {
            finalDesVector = finalDesVector + new Vector3(0, 0, End);
            if (ready3) {
                startTime = Time.time;
                startPos = Ppos - transform.position;
                ready3 = false;
            } else {
                ready3 = true;
            }
        }
        if(Input.GetAxis("Vertical") < 0) {
            finalDesVector = finalDesVector + new Vector3(0, 0, -End);
        if (ready4) {
                startTime = Time.time;
                startPos = Ppos - transform.position;
                ready4 = false;
            } else {
                ready4 = true;
            }
        }
        
        
        

        globalJourneyLength = Vector3.Distance(startPos, finalDesVector);
        float distCovered = (Time.time - startTime) * Speed;
        float fracJourney = distCovered / globalJourneyLength;
        Debug.Log(startPos +" FinalDesVector: " + finalDesVector + " y: " + Input.GetAxis("Vertical") + " x: " + Input.GetAxis("Horizontal") + " Covered: " + distCovered + " fracJourney: " + fracJourney + " GlobalJourneyLength: " + globalJourneyLength);
        Vector3 temp = Vector3.Lerp(startPos, finalDesVector, fracJourney);
        transform.position = temp + Ppos;
    }
}
