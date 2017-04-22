using UnityEngine;

public class BladeCutter : MonoBehaviour
{
    public Material capMaterial;
    public void Cut()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {

            GameObject victim = hit.collider.gameObject;

            GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

            foreach (var piece in pieces)
            {
                if (!piece.GetComponent<Rigidbody>())
                {
                    piece.AddComponent<Rigidbody>();
                }
                var coll = piece.GetComponent<MeshCollider>();
                if (!coll)
                {
                    coll = piece.AddComponent<MeshCollider>();
                }
                var meshFilter = piece.GetComponent<MeshFilter>();
                coll.sharedMesh = meshFilter.mesh;
                coll.inflateMesh = true;
                coll.skinWidth = 0.01f;
                coll.convex = true;
            }

        }
    }

    public void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

        Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * 0.5f);

    }
}
