using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Interact : MonoBehaviour
{
    public string mention;
    [HideInInspector]
    public Hand m_ActiveHand = null;
}
