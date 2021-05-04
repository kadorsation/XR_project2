using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR;

public class SingleShotGun : Gun
{

    public GameObject bulletPrefab;
    [SerializeField] 
	Camera cam;
	
	public Transform gunEnd;
	// Fire
	public float fireRate = 0.25f;
	private float nextFire;

	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

	private LineRenderer laserLine;
    private GameObject[] currentBullets;
    private int id = 0;
    private float shotPower = 100f;
	void Start () 
	{
		// AudioSource
        //gunAudio = GetComponent<AudioSource>();
		laserLine = GetComponent<LineRenderer>();
	}

    public override void Use()
    {
    	Debug.Log("Using gun" + itemInfo.itemName);
    	
    	Shoot();
    }

    void Shoot()
    {
    	if(Time.time <= nextFire)
    		return;

    	nextFire = Time.time + fireRate;

    	StartCoroutine (ShotEffect());

        if (bulletPrefab)
        {
            Instantiate(bulletPrefab, gunEnd.position, gunEnd.rotation).GetComponent<Rigidbody>().AddForce(gunEnd.forward * shotPower);
        }

        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(gunEnd.position, gunEnd.forward, out hitInfo, 100);

        if (laserLine)
        {
            laserLine.SetPositions(new Vector3[] { gunEnd.position, hasHit ? hitInfo.point :
                gunEnd.position + gunEnd.forward * 100});
        }

        if(hasHit)
        {
            Debug.Log("We hit " + hitInfo.collider.gameObject.name);
            
            hitInfo.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
        }
    }

    private IEnumerator ShotEffect()
    {
		// 播放音效
		//unAudio.Play ();

		// 显示射击轨迹
		laserLine.enabled = true;

		// 等待0.07秒
        yield return shotDuration;

        // 等待结束后隐藏轨迹
        laserLine.enabled = false;
	}
}
