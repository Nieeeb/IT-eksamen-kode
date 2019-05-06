using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Tobii.Gaming;
using Tobii;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;

    public int Headthresholdx = 3;
    public int Headthresholdy = 2;
    public int Headthresholdz = 1;

    public int MagSize = 5;
    public int BulletsInMag = 5;
    public int ammo = 4;
    public Text Mag;
    private int prevAmmo;

    public AudioClip ammoPickupSound;
    public AudioClip reloadSound;
    public AudioClip shoot;
    public AudioClip dryfire;
    public AudioClip stepping0;
    public AudioClip stepping1;
    public AudioClip meleeHit;
    public AudioClip meleeMiss;
    public AudioClip music;
    public AudioSource playerSource;
    public AudioSource musicSource;
    public AudioSource steppingSource;
    public float stepSpeed = 1f;
    private float timeStep = 0f;
    private bool stepAlt = false;

    public float stepMaxVolume = 0.25f;
    public float stepMinVolume = 0.5f;

    public int moveSpeed;

    public GameObject spawnPos;
    public GameObject bullet;
    public float firerate;
    
    private Vector3 PlayerPos;
    private float angle;
    private Outline _xOutline;
    private Outline _yOutline;

    private bool keymode = false;

    private float blinkTimer;

    private LayerMask floorMask;

    private Vector3 prevPos = new Vector3(0, 0, 0);
    private int count = 0;

    private bool pause = false;

    private bool headmove = false;
    private Vector3 headPos = new Vector3(0,0,0);
    private int headCount = 0;
    private float firedelay = 0;
    private Vector3 mousePos;

    public float meleeRange = 3f;
    public float maxDistance;
    public LayerMask layerMask;

    public float startDamage = 0.5f;
    private float potDamage;
    private bool max = false;
    public float maxDamage = 3f;
    public GameObject chargeBar;


    public GameObject StatsObject;
    private statsController pc;

    // Start is called before the first frame update
    void Start() {
        pc = StatsObject.GetComponent<statsController>();
        potDamage = startDamage;
        prevAmmo = ammo;
        PlayerPos = new Vector3(Screen.currentResolution.width / 2, 0, Screen.currentResolution.height / 2);
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
        rb = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Ground");
        UpdateUI();
    }

    // Update is called once per frame
    void Update() {
        
        if (pause) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                pause = !pause;
            } 
            return;
        } else if (Input.GetKeyDown(KeyCode.Space)){
            pause = !pause;
        }
        HeadPose headPose = TobiiAPI.GetHeadPose();

        //Debug.Log("X: " + headPose.Position.x + " Y: " + headPose.Position.y + " Z: " + headPose.Position.z);

        GazePoint gazePoint = TobiiAPI.GetGazePoint();


        Vector2 roundedSampleInput = new Vector2(0, 0);
        if (keymode) {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            transform.LookAt(mousePos + Vector3.up * transform.position.y);
        } else if (gazePoint.IsValid) {
            Vector3 gazePosition = Camera.main.ScreenToWorldPoint(new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, Camera.main.transform.position.y));

            if ((headPos.x - Headthresholdx) <= headPose.Position.x && (headPos.y - Headthresholdy) <= headPose.Position.y && (headPos.z - Headthresholdz) <= headPose.Position.z && (headPos.x + Headthresholdx) >= headPose.Position.x && (headPos.y + Headthresholdy) >= headPose.Position.y && (headPos.z + Headthresholdz) >= headPose.Position.z) {
                headCount++;
                if (headCount >= 3) {
                    headmove = true;
                } else {
                    headmove = false;
                }
            } else {
                headCount = 0;
            }
            headPos = headPose.Position;

            if (prevPos == gazePosition) {
                count++;
                if (count == 3 && headmove && blinkTimer <= 0) {
                    blinkTimer = 1.5f;
                   
                    pc.blinks++;
                }
            } else {
                count = 0;
            }
            if(blinkTimer > 0) {
                blinkTimer -= Time.deltaTime;
            } else {
                
            }
            
            prevPos = gazePosition;
            roundedSampleInput = new Vector2(Mathf.RoundToInt(gazePosition.x), Mathf.RoundToInt(gazePosition.y));

            //Debug.Log(roundedSampleInput.x);
            //Debug.Log(roundedSampleInput.y);
            //Debug.Log(roundedSampleInput.z);

            transform.LookAt(gazePosition + Vector3.up * transform.position.y);
        }
        


        if (Input.GetKeyDown(KeyCode.F)) {
            keymode = !keymode;
        }
        if (Input.GetMouseButton(0)) {
            if (firedelay >= 1 / firerate) {
                if (BulletsInMag > 0) {
                    Vector3 bulletPos = spawnPos.transform.position;
                    Instantiate(bullet, bulletPos, transform.rotation);
                    firedelay = 0;
                    BulletsInMag--;
                        playerSource.clip = shoot;
                        playerSource.Play();
                    pc.shotsFired++;
                    
                } else {
                    playerSource.clip = dryfire;
                    playerSource.Play();
                }
            }
            
            UpdateUI();
        }
        firedelay += 1.0f * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R)) {
            pc.shotsWasted += BulletsInMag;
            BulletsInMag = ammo;
            if(BulletsInMag > MagSize) {
                BulletsInMag = MagSize;
            }
            ammo -= BulletsInMag;
            UpdateUI();
        }
        
        if (Input.GetMouseButton(1)) {
            if (!max) {
                potDamage += (Mathf.Pow(1.7f,potDamage)) * Time.deltaTime;
                if (potDamage >= maxDamage) {
                    max = true;
                    potDamage = maxDamage;
                }
            } else if(potDamage > startDamage){
                potDamage -= (Mathf.Pow(1.9f, potDamage)) * Time.deltaTime;
            } 
            
        } else if (Input.GetMouseButtonUp(1)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, meleeRange)) {
                hit.transform.Find("Health").SendMessage("Damage", potDamage);
                playerSource.clip = meleeHit;
                playerSource.Play();
                pc.meleeHits++;
            } else {
                pc.meleeMisses++;
                playerSource.clip = meleeMiss;
                playerSource.Play();
            }
            potDamage = startDamage;
            max = false;
            
        }
        chargeBar.GetComponent<UIController>().SendMessage("UpdateChargeBar", potDamage);
    }
    
    void FixedUpdate() {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * moveSpeed;

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

        if (moveHorizontal != 0 || moveVertical != 0) {
            if(timeStep >= 1 / stepSpeed) {
                
                if (stepAlt) {
                    steppingSource.clip = stepping0;
                } else {
                    steppingSource.clip = stepping1;
                }
                steppingSource.volume = Random.Range(stepMinVolume,stepMaxVolume);
                steppingSource.pitch = Random.Range(0.75f, 1.25f);
                steppingSource.Play();
                stepAlt = !stepAlt;
                timeStep = 0f;
            }
            timeStep += 1.0f * Time.deltaTime;
        }
    }
    public void UpdateUI() {
        Mag.text = ammo.ToString();
        if(prevAmmo < ammo) {
            playerSource.clip = ammoPickupSound;
            playerSource.Play();
        } else if(prevAmmo > ammo) {
            playerSource.clip = reloadSound;
            playerSource.Play();
        }
        prevAmmo = ammo;
    }
}
