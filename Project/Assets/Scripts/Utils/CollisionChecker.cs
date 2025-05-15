using UnityEngine;

public class CollisionChecker
{
    private const float k_groundRayDistance = .3f;

    private Collider m_groundCheckCollider;

    public CollisionChecker(Transform groundCheckTransform)
    {
        m_groundCheckCollider = groundCheckTransform.GetComponent<Collider>();
    }

    public bool IsGrounded()
    {
        Vector3 colliderCenter = m_groundCheckCollider.bounds.center;
        Ray downwardsRay = new Ray(colliderCenter, Vector3.down);

        bool isGrounded = Physics.Raycast(downwardsRay, k_groundRayDistance, 
            GameManager.Instance.groundLayerMask, QueryTriggerInteraction.Ignore);

        return isGrounded;
    }
}
