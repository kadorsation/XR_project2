using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SimpleShoot : MonoBehaviour
{
    public SteamVR_Action_Boolean  isFire = null;

    public GameObject bulletPrefab;
    public GameObject line;
    public Transform GunEnd;
    public int shotField = 10;
    private Animator animator;
    [SerializeField]
    Camera cam;

    public Transform gunEnd;
    // Fire
    public float fireRate = 0.25f;
    private float nextFire;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

    private LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            animator.SetBool("IsFire", true);
        }
    }

    void simpleShoot()
    {
        if (bulletPrefab)
        {
            Instantiate(bulletPrefab, GunEnd.position, GunEnd.rotation).GetComponent<Rigidbody>().AddForce(GunEnd.forward * shotField);
        }
    }
    
}
