using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FishRodStringRenderer : MonoBehaviour
{
    [SerializeField] Transform linePointParent;
    LineRenderer lineRenderer;
    public Transform lastPoint
    {
        get { return linePointParent.GetChild(linePointParent.childCount - 1); }
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = linePointParent.childCount;

    }

    void Update()
    {

        if (linePointParent.childCount <= 0) return;

        for (int i = 0; i < linePointParent.childCount; i++)
            lineRenderer.SetPosition(i, linePointParent.GetChild(i).position);

    }
}
