using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // public ParticleSystem explosionVFX;

    void OnTriggerEnter(Collider other)
    {
    	Debug.Log(other);
        if (transform.gameObject.tag == other.gameObject.tag) return;

        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(20);
        Destroy(gameObject);

        // if (explosionVFX) {
        //     ParticleSystem ps = Instantiate(explosionVFX, transform.position, transform.rotation) as ParticleSystem;
        //
        //     ps.Play();
        // }

    	//Invoke(DestroyPS, 2.0f);
    }

    private void DestroyPS()
    {
    	Destroy(gameObject);
    }
}
