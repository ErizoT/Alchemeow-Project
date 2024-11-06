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
        objectToSpawn.transform.position = transform.position;
        objectToSpawn.transform.rotation = transform.rotation;
    }

    void checkGripPoints()
    {
        foreach (GameObject point in gripPoints)
        {
            if (point.gameObject.CompareTag("Untagged"))
            {
                //GameObject prefabSpawning = Instantiate(objectToSpawn, this.transform.position, this.transform.rotation);

                objectToSpawn.SetActive(true);
                objectToSpawn.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);

                Destroy(this.gameObject);

                point.GetComponent<GripPoint>().pC.LetGo();
            }
        }
    }
}
