using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FishRodStringRenderer : MonoBehaviour
{
    [SerializeField] List<Transform> linePoints;
    LineRenderer lineRenderer;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = linePoints.Count;

    }

    void Update()
    {

        if (linePoints.Count <= 0) return;

        for (int i = 0; i < linePoints.Count; i++)
            lineRenderer.SetPosition(i, linePoints[i].position);

    }
}
