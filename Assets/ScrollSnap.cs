using UnityEngine;
using UnityEngine.UI;

public class ScrollSnap : MonoBehaviour
{
    //public ScrollRect scrollRect;
    //public RectTransform content;

    //public float snapSpeed = 10f;

    //private RectTransform[] pages;

    //private Vector2 targetPos;

    //private bool isDragging;

    //void Start()
    //{
    //    int count = content.childCount;

    //    pages = new RectTransform[count];

    //    for (int i = 0; i < count; i++)
    //    {
    //        pages[i] =
    //            content.GetChild(i)
    //            .GetComponent<RectTransform>();
    //    }

    //    targetPos = content.anchoredPosition;
    //}

    //void Update()
    //{
    //    if (!isDragging)
    //    {
    //        content.anchoredPosition =
    //            Vector2.Lerp(
    //                content.anchoredPosition,
    //                targetPos,
    //                Time.deltaTime * snapSpeed
    //            );
    //    }
    //}

    //public void OnBeginDrag()
    //{
    //    isDragging = true;
    //}

    //public void OnEndDrag()
    //{
    //    isDragging = false;

    //    RectTransform closestPage = pages[0];

    //    float closestDistance =
    //        Mathf.Abs(
    //            content.anchoredPosition.y +
    //            pages[0].anchoredPosition.y
    //        );

    //    for (int i = 1; i < pages.Length; i++)
    //    {
    //        float distance =
    //            Mathf.Abs(
    //                content.anchoredPosition.y +
    //                pages[i].anchoredPosition.y
    //            );

    //        if (distance < closestDistance)
    //        {
    //            closestDistance = distance;
    //            closestPage = pages[i];
    //        }
    //    }

    //    targetPos =
    //        new Vector2(
    //            content.anchoredPosition.x,
    //            -closestPage.anchoredPosition.y
    //        );
    //}
}