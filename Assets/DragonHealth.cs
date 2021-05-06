using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Photon.Pun;
// using Photon.Realtime;

public class DragonHealth : MonoBehaviour, IDamageable
{
    const float maxHealth = 100f;
    float currentHealth = maxHealth;
    PlayerManager playerManager;
    Rigidbody rb;
    private Animator animator;
    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        Debug.Log("I do");
        animator.SetInteger("dragon_motion", 4);

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            //Die();
        }

    }
}
