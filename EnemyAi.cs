using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour {
    // Start is called before the first frame update

    public Transform target = null;
    public GameObject player;
    private NavMeshAgent agent;
    private float timer = 0f;
    public bool wander = true;
    public float maxTimeBeforeWander = 5f;
    private float timeBeforeWander;
    public float timeBeforeLost = 10f;
    public float wanderRadius = 5f;
    private float chaseTime = 0f;
    private bool hunting = false;
    private FieldOfView fov;
    public bool isObserved = false;
    public bool weeping = false;
    private GameObject parent;
    private Rigidbody parentRb;

    void Start() {
        agent = GetComponentInParent<NavMeshAgent>();
        timeBeforeWander = Random.Range(0f, maxTimeBeforeWander);
        fov = GetComponentInParent<FieldOfView>();
        parent = transform.parent.gameObject;
        parentRb = parent.GetComponent<Rigidbody>();
    }

    void Update() {
        if (weeping) {
            checkIfObserved();
            weepingMovement(isObserved);
        }

        checkForTargets(fov.visibleTargets);

        timer += Time.deltaTime;

        if (target) {
            agent.destination = target.position;
        }
        else if (wander && timer >= timeBeforeWander) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0f;
            timeBeforeWander = Random.Range(0f, maxTimeBeforeWander);
        }

        if (hunting) {
            chaseTime += Time.deltaTime;
            if (chaseTime >= timeBeforeLost) {
                target = null;
                hunting = false;
                chaseTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            target = other.transform;
            chaseTime = 0f;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    void checkForTargets(List<GameObject> listOfTargets) {
        Transform newTarget;
        if (listOfTargets.Count <= 0) {
            hunting = true;
            return;
        }
        else {
            foreach (GameObject visTarget in listOfTargets) {
                if (visTarget.CompareTag("Player")) {
                    newTarget = visTarget.transform;
                    target = newTarget;
                    return;
                }
            }
        }
    }
    void checkIfObserved() {
        isObserved = false;
        FieldOfView playerFov = player.GetComponentInParent<FieldOfView>();
        List<GameObject> playerTargets = playerFov.visibleTargets;
        foreach (GameObject plTarget in playerTargets) {
            if (plTarget == gameObject.transform.parent.gameObject) {
                isObserved = true;
            }
        }
        return;
    }

    void weepingMovement(bool observed) {
        if (observed) {
            agent.velocity = Vector3.zero;
            parentRb.velocity = Vector3.zero;
            agent.isStopped = true;
        }
        else {
            agent.isStopped = false;
        }
        return;
    }
}