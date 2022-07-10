using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public List<DuplicateWall> duplicateWalls = new List<DuplicateWall>();

    public Color ballColor;

    [SerializeField] private Material[] blueMat;
    [SerializeField] private Gradient blueColor;

    [SerializeField] private Material[] orangeMat;
    [SerializeField] private Gradient orangeColor;

    [Space]

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Rigidbody rigidbody;


    public void ChangeBallCurrentColor(Color color)
    {
        ballColor = color;

        if (color == Color.Orange)
        {
            meshRenderer.materials = GameManager.instance.orangeMat;
            trailRenderer.colorGradient = GameManager.instance.orangeColor;
        }
        else if (color == Color.Blue)
        {
            meshRenderer.materials = GameManager.instance.blueMat;
            trailRenderer.colorGradient = GameManager.instance.blueColor;
        }
    }

    public void BallColorChange()
    {
        if (ballColor == Color.Blue)
        {
            ballColor = Color.Orange;
            meshRenderer.materials = GameManager.instance.orangeMat;
            trailRenderer.colorGradient = GameManager.instance.orangeColor;
        }
        else if(ballColor == Color.Orange)
        {
            ballColor = Color.Blue;
            meshRenderer.materials = GameManager.instance.blueMat;
            trailRenderer.colorGradient = GameManager.instance.blueColor;
        }
            
    }

    public void Addforce(Vector3 origin, float force)
    {
        rigidbody.AddForce((transform.position - origin) * force, ForceMode.Force);
    }
}

public enum Color { Blue, Orange }
