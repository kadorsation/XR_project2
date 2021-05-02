using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneObj : MonoBehaviour
{
    public GameObject GM_obj;
    public List<GameObject> ll;
    private Vector3 loc;
    // Start is called before the first frame update
    void Start()
    {
      GM_obj = GameObject.Find("attackBot");
      ll.Add(GM_obj);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Clone()
    {
      GameObject objectToClone = ll[ll.Count-1];
      GameObject clonedObject = Instantiate(objectToClone, objectToClone.transform.position, objectToClone.transform.rotation);
      ll.Insert(0, clonedObject);
      // GM_obj.GetComponent<Rigidbody>().useGravity = true;
      // return clonedObject;
    }
}
