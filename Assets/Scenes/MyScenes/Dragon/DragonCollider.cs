using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR;
using Photon.Pun;
using Photon.Realtime;
public class DragonCollider : MonoBehaviourPunCallbacks, IDamageable
{
    // ===================//

    private GameObject dragon;
    // Start is called before the first frame update
    void Start()
    {
        dragon = GameObject.FindGameObjectWithTag("Dragon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
	{
        Debug.Log(gameObject.transform.parent.name);

		dragon.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);
	}
}
