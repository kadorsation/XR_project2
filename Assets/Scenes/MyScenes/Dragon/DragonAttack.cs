using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    public GameObject mouthEnd;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(20);
        //Destroy(gameObject);
        other.gameObject.GetComponent<Rigidbody>().AddForce(mouthEnd.transform.forward * 100);
        //ParticleSystem ps = Instantiate(explosionVFX, transform.position, transform.rotation) as ParticleSystem;

       // ps.Play();

        //Invoke(DestroyPS, 2.0f);
    }
}
