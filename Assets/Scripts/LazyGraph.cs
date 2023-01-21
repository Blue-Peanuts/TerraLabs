using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class LazyGraph : MonoBehaviour
{
    public Vector2 minValues;
    public Vector2 maxValues;
    public float[] values;
    [FormerlySerializedAs("pointObject")] [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform pointParent;

    [SerializeField] private RectTransform topLeftRectTransform;
    [SerializeField] private RectTransform bottomRightRectTransform;

    private Vector2 TopLeftPos => new(topLeftRectTransform.position.x, topLeftRectTransform.position.y);
    private Vector2 BottomRightPos => new(bottomRightRectTransform.position.x, bottomRightRectTransform.position.y);

    private void Start()
    {
        UpdateGraph();
    }

    private void Update()
    {
        //UpdateGraph();
    }

    private void UpdateGraph()
    {
        foreach (Transform child in pointParent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < values.Length; i++)
        {
            SpawnPoint(ValueToPosition(i, values[i]));
            if (i < values.Length - 1)
            {
                DrawLine(ValueToPosition(i, values[i]), ValueToPosition(i + 1, values[i + 1]));
            }
            
        }
    }

    Vector2 ValueToPosition(int i, float value)
    {
        return new Vector2(Mathf.Lerp(TopLeftPos.x, BottomRightPos.x, 
                1.0f * i / (values.Length - 1)), 
            Mathf.Lerp(bottomRightRectTransform.position.y,topLeftRectTransform.position.y, 
                1f * value / maxValues.y));
    }

    void SpawnPoint(Vector2 position)
    {
        GameObject pointObject = Instantiate(pointPrefab, pointParent, true);
        pointObject.GetComponent<RectTransform>().position = position;
    }

    void DrawLine(Vector2 position, Vector2 targetPosition)
    {
        GameObject lineObject = Instantiate(linePrefab, pointParent, true);
        RectTransform lineRectTransform = lineObject.GetComponent<RectTransform>();
        lineObject.GetComponent<RectTransform>().position = position;
        lineRectTransform.sizeDelta =
            new Vector2(Vector3.Distance(position, targetPosition), lineRectTransform.sizeDelta.y);
        print(Mathf.Atan2((targetPosition - position).y, (targetPosition - position).x) * Mathf.Rad2Deg);
        lineRectTransform.rotation =
            Quaternion.AngleAxis(Mathf.Atan2((targetPosition - position).y, (targetPosition - position).x) * Mathf.Rad2Deg, Vector3.forward);
    }
}
