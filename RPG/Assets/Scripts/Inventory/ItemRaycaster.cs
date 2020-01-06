using UnityEngine;

public class ItemRaycaster : ItemComponent
{
    [SerializeField]
    private float m_Delay = 0.1f;

    [SerializeField]
    private float m_Range = 10f;
    [SerializeField]
    private LayerMask m_layerMask;
    
    private RaycastHit[] m_Results = new RaycastHit[10];

    private void Awake()
    {
        m_layerMask = LayerMask.GetMask("Default");
    }

    public override void Use()
    {
        m_NextUseTime = Time.time + m_Delay;

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2);
        int hits = Physics.RaycastNonAlloc(ray, m_Results, m_Range, m_layerMask, QueryTriggerInteraction.Collide);

        RaycastHit nearest= new RaycastHit();
        double nearestDistance = double.MaxValue;
        
        for (int i = 0; i < hits; i++)
        {
            var distance = Vector3.Distance(transform.position, m_Results[i].point);
            if (distance < nearestDistance)
            {
                nearest = m_Results[i];
                nearestDistance = distance;
            }
        }
        
        if (nearest.transform != null)
        {
            Transform hitCube = GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<Transform>();
            hitCube.localScale = Vector3.one * 0.1f;
            hitCube.position = nearest.point;
        }
    }
}