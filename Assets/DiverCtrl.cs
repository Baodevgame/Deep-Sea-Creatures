using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverCtrl : MonoBehaviour
{
    [Header("Reward")]
    [SerializeField] private int reward;

    public CollectionType collectionType;

    public float speed;
    private int direction = 1;
    private float mapLimit = 2.5f;

    private bool isHooked = false;
    private bool isCaught = false;
    public bool IsCaught => isCaught;

    void Update()
    {
        if (isHooked) return;
        if (speed <= 0) return;

        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x >= mapLimit)
            direction = -1;
        else if (transform.position.x <= -mapLimit)
            direction = 1;

        UpdateVisualDirection();
    }

    public void SetHooked(bool value)
    {
        isHooked = value;
    }

    void UpdateVisualDirection()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    public void MarkCaught()
    {
        isCaught = true;
    }
    public int GetReward()
    {
        return reward;
    }
}
