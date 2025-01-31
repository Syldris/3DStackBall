using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPartController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.5f;

    private MeshRenderer meshRenderer;
    private new Rigidbody rigidbody;
    private new Collider collider;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void BreakingPart()
    {
        rigidbody.isKinematic = false;
        collider.enabled = false;

        Vector3 forcePoint = transform.parent.position;
        float parentXPosition = transform.parent.position.x;
        float xPosition = meshRenderer.bounds.center.x;

        Vector3 direction = (parentXPosition - xPosition < 0) ? Vector3.right : Vector3.left;
        direction = (Vector3.up * moveSpeed + direction).normalized;

        float force = Random.Range(20,40);
        float torque = Random.Range(110, 180);

        rigidbody.AddForceAtPosition(direction * force, forcePoint, ForceMode.Impulse);
        rigidbody.AddTorque(Vector3.left * torque);
        rigidbody.velocity = Vector3.down;
    }
}
