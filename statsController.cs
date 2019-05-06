using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statsController : MonoBehaviour
{
    public int shotsFired = 0;
    public int shotsWasted = 0;
    public int enemiesKilled = 0;
    public float gameTime = 0;
    public int meleeHits = 0;
    public int meleeMisses = 0;
    public int blinks = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
