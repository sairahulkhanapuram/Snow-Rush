using UnityEngine;
public class FrostRunCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5.2f, -7.5f);
    public float followSmoothness = 7f;
    public float lookAhead = 7f;
    private void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desired,
            1f - Mathf.Exp(-followSmoothness * Time.deltaTime));
        Vector3 lookPoint = target.position + Vector3.up * 0.7f + Vector3.forward * lookAhead;
        Quaternion rot = Quaternion.LookRotation(lookPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot,
            1f - Mathf.Exp(-followSmoothness * Time.deltaTime));
    }
}
