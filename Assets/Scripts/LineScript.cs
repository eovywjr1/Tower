using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer.startWidth = 2f;
        lineRenderer.endWidth = 2f;
    }
}
