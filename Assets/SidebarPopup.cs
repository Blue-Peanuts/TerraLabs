using UnityEngine;
using UnityEngine.EventSystems;

public class SidebarPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float defaultX;
    private RectTransform _rectTransform;
    private bool hovered = false;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        defaultX = _rectTransform.position.x;
    }

    private void Update()
    {
        float targetX = hovered ? 0 : defaultX;
        _rectTransform.position = Vector3.MoveTowards(_rectTransform.position,
            new Vector3(targetX, _rectTransform.position.y, 0), Time.deltaTime * 1800);
    }

    // Update is called once per frame
    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }
}
