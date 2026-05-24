using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundScaler : MonoBehaviour
{
    public enum ScaleType
    {
        Expand,
        Width
    }

    public ScaleType scaleType;

    private int lastWidth;
    private int lastHeight;

    void Start()
    {
        ApplyScale();

        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }

    void Update()
    {
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            lastWidth = Screen.width;
            lastHeight = Screen.height;

            ApplyScale();
        }
    }

    void ApplyScale()
    {
        var cam = Camera.main;
        var sr = GetComponent<SpriteRenderer>();

        if (cam == null || sr.sprite == null) return;

        // ===== BACKGROUND =====
        if (scaleType == ScaleType.Expand)
        {
            float worldHeight = cam.orthographicSize * 2f;
            float worldWidth = worldHeight * Screen.width / Screen.height;

            Vector2 spriteSize = sr.sprite.bounds.size;

            float scaleX = worldWidth / spriteSize.x;
            float scaleY = worldHeight / spriteSize.y;

            float scale = Mathf.Max(scaleX, scaleY);

            transform.localScale = new Vector3(scale, scale, 1f);

            transform.position = new Vector3(
                cam.transform.position.x,
                cam.transform.position.y,
                transform.position.z
            );
        }

        // ===== WATER =====
        else if (scaleType == ScaleType.Width)
        {
            float worldHeight = cam.orthographicSize * 2f;
            float worldWidth = worldHeight * Screen.width / Screen.height;

            float spriteWidth = sr.sprite.bounds.size.x;

            float scaleX = worldWidth / spriteWidth;

            Vector3 scale = transform.localScale;
            scale.x = scaleX;

            transform.localScale = scale;
        }
    }
}