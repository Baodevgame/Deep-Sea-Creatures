using UnityEngine;

public class FishCtrl : MonoBehaviour
{
    public CollectionType collectionType;

    public float speed = 1f;
    private int direction = 1;
    private float leftLimit;
    private float rightLimit;

    private bool isHooked = false;
    private bool isCaught = false;
    public bool IsCaught => isCaught;

    [Header("Reward")]
    [SerializeField] private int reward;

    public int GetReward()
    {
        return reward;
    }

    void Start()
    {
        Camera cam = Camera.main;

        Vector3 left = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 right = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));

        leftLimit = left.x;
        rightLimit = right.x;
    }

    void Update()
    {
        if (isHooked) return;
        if (speed <= 0) return;

        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x >= rightLimit)
            direction = -1;
        else if (transform.position.x <= leftLimit)
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
}