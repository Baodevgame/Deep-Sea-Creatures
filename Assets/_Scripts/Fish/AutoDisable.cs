using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    public float disableDistance = 10f;

    private FishCtrl fishCtrl;
    private Rigidbody2D rb;
    private SpriteRenderer[] renderers;
    private Animator animator; 

    float timer;

    void Start()
    {
        fishCtrl = GetComponent<FishCtrl>();
        rb = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>(); 
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 0.5f) return;
        timer = 0;

        float dist = Vector2.Distance(transform.position, Camera.main.transform.position);
        bool isFar = dist > disableDistance;

        // tat logic
        if (fishCtrl != null)fishCtrl.enabled = !isFar;

        // tat physics
        if (rb != null)rb.simulated = !isFar;

        // tat render
        foreach (var r in renderers)r.enabled = !isFar;

        // tat animation
        if (animator != null)animator.enabled = !isFar;
    }
}