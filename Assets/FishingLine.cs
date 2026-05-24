using UnityEngine;

public class FishingLine : MonoBehaviour
{
    public LineRenderer line;
    public Transform startPoint; 
    public Transform hook;       

    void Update()
    {
        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, hook.position);
    }
}
