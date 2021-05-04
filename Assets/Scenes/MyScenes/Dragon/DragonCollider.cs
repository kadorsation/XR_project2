using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR;
using Photon.Pun;
using Photon.Realtime;
public class DragonCollider : MonoBehaviourPunCallbacks, IDamageable
{
	// ===================//
	

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
	{
        Debug.Log(gameObject.transform.parent.name);

		gameObject.transform.parent.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);
	}
}
