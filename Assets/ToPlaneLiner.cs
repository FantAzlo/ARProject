using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class ToPlaneLiner : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private RadarPlane _radarPlane;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _radarPlane = RadarPlane.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, new Vector3(transform.position.x, _radarPlane.transform.position.y, transform.position.z));
    }
}
