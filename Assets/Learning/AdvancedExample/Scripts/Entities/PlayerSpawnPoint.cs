using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour {

    public float sphereRadius = 0.5f;
    public float playerHeight = 2f;

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, sphereRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * playerHeight);
        Gizmos.DrawWireSphere(transform.position + Vector3.up * playerHeight, 0.2f);
    }
}
