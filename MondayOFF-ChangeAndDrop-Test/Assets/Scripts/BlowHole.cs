using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowHole : MonoBehaviour
{
    [SerializeField] private Vector3 forceDir;
    [SerializeField] private float force = 1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            ball.GetComponent<Rigidbody>().AddForce(forceDir * force);
        }
    }
}
