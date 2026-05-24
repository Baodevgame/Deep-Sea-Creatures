using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HookTouchHandler : MonoBehaviour
{
    public float limitX = 4f;

    private Rigidbody2D rb;
    private Camera cam;

    private bool dragging;
    private float offsetX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (cam == null) cam = Camera.main;

        if (cam == null) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouse();
#else
    HandleTouch();
#endif
    }

    // ================= MOUSE (EDITOR) =================
    void HandleMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(cam.transform.position.z);

        Vector3 mouseWorld =
            cam.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            BeginDrag(mouseWorld);
        }
        else if (Input.GetMouseButton(0) && dragging)
        {
            Drag(mouseWorld);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    // ================= TOUCH (MOBILE) =================
    void HandleTouch()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        Vector3 touchWorld = cam.ScreenToWorldPoint(
            new Vector3(touch.position.x, touch.position.y, 0)
        );

        if (touch.phase == TouchPhase.Began)
        {
            BeginDrag(touchWorld);
        }
        else if (touch.phase == TouchPhase.Moved && dragging)
        {
            Drag(touchWorld);
        }
        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            EndDrag();
        }
    }

    // ================= SHARED =================
    void BeginDrag(Vector3 inputWorld)
    {
        dragging = true;

        float targetX = Mathf.Clamp(inputWorld.x, -limitX, limitX);
        rb.position = new Vector2(targetX, rb.position.y);

        offsetX = rb.position.x - inputWorld.x;
    }

    void Drag(Vector3 inputWorld)
    {
        float targetX = Mathf.Clamp(
            inputWorld.x + offsetX,
            -limitX,
            limitX
        );

        rb.position = new Vector2(targetX, rb.position.y);
    }

    void EndDrag()
    {
        dragging = false;
    }
}
