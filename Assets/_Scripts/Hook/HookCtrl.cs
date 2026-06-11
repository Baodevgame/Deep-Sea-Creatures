using UnityEngine;

public class HookCtrl : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameManagerUI gameManagerUI;

    [Header("Config")]
    public Transform pointStart;
    public float pullUpSpeed = 2f;
    public float pullUpTime = 2f;

    private bool isFullCalculated = false;
    private float cachedSpeed = 0f;

    [SerializeField] private float maxLineLength;
    [SerializeField] private bool isPullingUp = false;

    private float startY;

    public bool canCatchFish = false;
    private bool isFinished = false;
    private bool canStartFishing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManagerUI = FindObjectOfType<GameManagerUI>();
    }

    private void Start()
    {
        rb.gravityScale = 0f;
        startY = pointStart.position.y;
        maxLineLength = FishingGearManager.Instance.maxLineLength;
    }

    private void FixedUpdate()
    {
        if (!canStartFishing)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (isFinished)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        UpdateDepth();
        if (!isPullingUp)
        {
            rb.velocity = Vector2.down * FishingGearManager.Instance.fallSpeed;
            CheckMaxDepth();
        }
        else
        {
            PullUp();
        }
    }
    public void StartFishing()
    {
        canStartFishing = true;
    }

    void CheckMaxDepth()
    {
        float depth = startY - rb.position.y;

        FishingGearManager.Instance.currentDepth = Mathf.Clamp(depth, 0, maxLineLength);

        if (depth >= maxLineLength)
        {
            rb.position = new Vector2(rb.position.x, startY - maxLineLength);

            BubbleCollision bubble = GetComponentInChildren<BubbleCollision>();

            if (bubble != null && bubble.gameObject.activeSelf)
            {
                bubble.BreakBubble();
            }
            else
            {
                StopFallingAndPullUp();
            }
        }
    }

    void UpdateDepth()
    {
        float depth = startY - rb.position.y;

        FishingGearManager.Instance.currentDepth = Mathf.Clamp(depth, 0, maxLineLength);
    }

    void PullUp()
    {
        Vector2 targetPos = pointStart.position;

        rb.velocity = Vector2.zero;

        HookCatchFish catchFish = GetComponent<HookCatchFish>();

        float speed = pullUpSpeed;

        if (catchFish.CurrentCatchCount >= FishingGearManager.Instance.hookSlots)
        {
            if (!isFullCalculated)
            {
                float distance = Vector2.Distance(rb.position, targetPos);

                cachedSpeed = distance / pullUpTime;
                cachedSpeed = Mathf.Clamp(cachedSpeed, 2f, 50f);

                isFullCalculated = true;
            }

            speed = cachedSpeed;
        }

        rb.position = Vector2.MoveTowards(rb.position,targetPos,speed * Time.fixedDeltaTime);

        if (Vector2.Distance(rb.position, targetPos) < 0.01f)
        {
            StopAtStartPoint();
        }
    }

    void StopAtStartPoint()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        isPullingUp = false;
        rb.position = pointStart.position;
        FishingGearManager.Instance.currentDepth = 0f;

        isFinished = true;

        gameManagerUI.OnGameOverPanel();  
    }

    public void StopFallingAndPullUp()
    {
        if (isPullingUp) return;

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        isPullingUp = true;
    }

    public void DisableCatch()
    {
        canCatchFish = false;
    }
}