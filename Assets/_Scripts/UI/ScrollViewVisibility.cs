using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewVisibility : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform viewport;
    public RectTransform content;

    List<RectTransform> items = new List<RectTransform>();

    void Start()
    {
        foreach (Transform child in content)
        {
            RectTransform rt =
                child.GetComponent<RectTransform>();

            if (rt != null)
            {
                items.Add(rt);
            }
        }

        scrollRect.onValueChanged.AddListener(delegate { UpdateVisibility(); });

        Invoke(nameof(UpdateVisibility), 0.1f);
    }

    void UpdateVisibility()
    {
        float viewTop = viewport.rect.yMax;

        float viewBottom = viewport.rect.yMin;

        for (int i = 0; i < items.Count; i++)
        {
            RectTransform item = items[i];

            Vector3 itemPos = viewport.InverseTransformPoint(item.position);

            float itemTop = itemPos.y +(item.rect.height / 2);

            float itemBottom = itemPos.y -(item.rect.height / 2);

            bool visible = itemBottom < viewTop &&itemTop > viewBottom;

            if (item.gameObject.activeSelf != visible)
            {
                item.gameObject.SetActive(visible);
            }
        }
    }
}