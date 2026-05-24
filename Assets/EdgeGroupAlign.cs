using UnityEngine;

public class EdgeGroupAlign : MonoBehaviour
{
    public enum Side
    {
        Left,
        Right
    }

    public Side side;
    public float offset = 0f;

    private Transform[] children;

    void Start()
    {
        children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        UpdatePosition();
    }

    void UpdatePosition()
    {
        var cam = Camera.main;
        if (cam == null) return;

        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * cam.aspect;
        float edgeX = worldWidth / 2f;

        //reset parent ve dung vi tri camera 
        Vector3 parentPos = transform.position;
        parentPos.x = cam.transform.position.x;
        transform.position = parentPos;

        //set CHILD theo WORLD
        foreach (var t in children)
        {
            Vector3 pos = t.position;

            if (side == Side.Left)
                pos.x = cam.transform.position.x - edgeX + offset;
            else
                pos.x = cam.transform.position.x + edgeX - offset;

            t.position = pos;
        }
    }
}