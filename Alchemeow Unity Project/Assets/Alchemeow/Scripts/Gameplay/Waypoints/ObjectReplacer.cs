using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReplacer : MonoBehaviour
{
    public GameObject[] gripPoints;
    public GameObject objectToSpawn;

    // Update is called once per frame
    void Update()
    {
        checkGripPoints();
    }

    void checkGripPoints()
    {
        foreach (GameObject point in gripPoints)
        {
            if (point.gameObject.CompareTag("Untagged"))
            {
                Instantiate(objectToSpawn, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}
