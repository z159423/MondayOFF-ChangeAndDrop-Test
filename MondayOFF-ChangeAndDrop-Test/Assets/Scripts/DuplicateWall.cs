using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateWall : MonoBehaviour
{
    [SerializeField] private int duplicateValue = 2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Ball>(out Ball ball))
        {
            if (ball.duplicateWalls.Contains(this))
                return;

            for(int i = 0; i < duplicateValue; i++)
            {
                var generatedBall = BallGenerator.instance.GenerateBall(other.bounds.center + new Vector3(0, Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));


                if (generatedBall.TryGetComponent<Ball>(out ball))
                    ball.duplicateWalls.Add(this);

            }
        }
    }
}
