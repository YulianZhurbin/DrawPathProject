using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private static LineDrawer instance;
    LineRenderer pathRenderer;

    public static LineDrawer Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        pathRenderer = GetComponent<LineRenderer>();
    }

    public void AddPoint(Vector3 newPoint)
    {
        pathRenderer.positionCount++;
        pathRenderer.SetPosition(pathRenderer.positionCount - 1, newPoint);
    }
}
