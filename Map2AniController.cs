using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2AniController : MonoBehaviour {
    private Animation Animator;
    private bool isUpOne = true;
    private bool isUpFour = false;
    private float timer = 0.0f;
    public float waitTime = 2.0f;
    private bool ready = false;
    // Start is called before the first frame update
    void Start() {
        Animator = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update() {
        if (ready) {
            if (isUpOne) {
                Animator["Cube.004|Cube.001Action"].normalizedTime = 0.0f;
                Animator["Cube.004|Cube.001Action"].speed = 1.0f;
                Animator.CrossFade("Cube.004|Cube.001Action");
                Animator.Play("Cube.004|Cube.001Action");
                isUpOne = false;
            }
            else {
                Animator["Cube.004|Cube.001Action"].normalizedTime = 1.0f;
                Animator["Cube.004|Cube.001Action"].speed = -1.0f;
                Animator.CrossFade("Cube.004|Cube.001Action");
                Animator.Play("Cube.004|Cube.001Action");
                isUpOne = true;
            }
            if (isUpFour) {
                Animator["Cube.001|Cube.004Action"].normalizedTime = 0.0f;
                Animator["Cube.001|Cube.004Action"].speed = 1.0f;
                Animator.CrossFade("Cube.001|Cube.004Action");
                Animator.Play("Cube.001|Cube.004Action");
                isUpFour = false;
            }
            else {
                Animator["Cube.001|Cube.004Action"].normalizedTime = 1.0f;
                Animator["Cube.001|Cube.004Action"].speed = -1.0f;
                Animator.CrossFade("Cube.001|Cube.004Action");
                Animator.Play("Cube.001|Cube.004Action");
                isUpFour = true;
            }
            ready = false;
        }
        else {
            timer += Time.deltaTime;
            if(timer >= waitTime) {
                ready = true;
                timer = 0.0f;
            }
            else {
                ready = false;
            }

        }
    }
}
