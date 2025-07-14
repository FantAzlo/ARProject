using UnityEngine;

public class SetOnFloor : MonoBehaviour
{
    [SerializeField] private LineRenderer? _lineRenderer = null;
    [SerializeField] private Color _meshColor = Color.white;
    private Vector3 _baseScale = Vector3.one * 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = transform.parent.GetComponent<LineRenderer>();
        GetComponent<SpriteRenderer>().color = _lineRenderer.endColor;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _lineRenderer.GetPosition(1);
        transform.localScale = _baseScale / Mathf.Clamp ( Vector3.Distance(_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1)), 1f, 10);
    }
}
