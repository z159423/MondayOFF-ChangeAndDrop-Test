using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private int WallHP = 10;

    [SerializeField] private int currentHp;

    private int break1Hp, break2Hp, break3Hp;

    [SerializeField] private List<ParticleSystem> breakParticles = new List<ParticleSystem>();

    private bool ballCountCheck = false;

    private void OnEnable()
    {
        currentHp = WallHP;

        int num = WallHP / 4;

        break1Hp = num * 3;
        break2Hp = num * 2;
        break3Hp = num;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            StopCoroutine(BallCountCheck());
            StartCoroutine(BallCountCheck());

            currentHp--;

            if (currentHp == break1Hp)
            {
                animator.SetTrigger("Break1");
            }
            else if (currentHp == break2Hp)
            {
                animator.SetTrigger("Break2");
            }
            else if (currentHp == break3Hp)
            {
                animator.SetTrigger("Break3");
            }
            else if (currentHp <= 0)
            {
                animator.SetTrigger("Break4");
            }

        }
    }

    public void BreakWall()
    {
        GetComponent<BoxCollider>().enabled = false;

        for(int i = 0; i < breakParticles.Count; i++)
        {
            breakParticles[i].GetComponent<BoxCollider>().enabled = false;
            breakParticles[i].GetComponent<MeshRenderer>().enabled = false;

            breakParticles[i].Play();
        }
    }

    private IEnumerator BallCountCheck()
    {
        
        yield return new WaitForSeconds(3);

        if (BallGenerator.instance.generatedBallList.Count < WallHP)
            GameManager.instance.GameOver();
        
    }
}
