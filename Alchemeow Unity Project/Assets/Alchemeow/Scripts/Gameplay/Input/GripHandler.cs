using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripHandler : MonoBehaviour
{
    public GripPoint[] points;
    public int holds;
    public bool cooperativeHold;

    private void Start()
    {
        List<GripPoint> list = new List<GripPoint>();
        FindGripPointsInChildren(transform, list);
        points = list.ToArray();
    }

    private void Update()
    {
        if (holds == 2)
        {
            cooperativeHold = true;
        }
        else
        {
            cooperativeHold = false;
        }
    }

    void FindGripPointsInChildren(Transform parent, List<GripPoint> list)
    {
        GripPoint gP = parent.GetComponent<GripPoint>();
        if (gP != null)
        {
            list.Add(gP);
            gP.gripHandler = this;
        }

        foreach (Transform child in parent)
        {
            FindGripPointsInChildren(child, list);
        }
    }
}
