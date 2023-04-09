using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FishRodStringRenderer : MonoBehaviour
{
    [SerializeField] Transform linePointParent;
    LineRenderer lineRenderer;
    public Transform lastPoint;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = linePointParent.childCount + 1;

    }

    void Update()
    {

        if (linePointParent.childCount <= 0) return;

        int i = 0;
        for (i = 0; i < linePointParent.childCount; i++)
            lineRenderer.SetPosition(i, linePointParent.GetChild(i).position);

        lineRenderer.SetPosition(i, lastPoint.position);



    }
}
