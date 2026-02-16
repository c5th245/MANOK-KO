using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Animator Anim;

    public float cooldown = 2;
    private float timer;

    private void Update()
    {
        if(timer > 0) {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if(timer <= 0)
        {
        }
    
    Anim.SetBool("isAttacking", true);
        timer = cooldown;
    }

    public void FinishAttacking()
        {
        Anim.SetBool("isAttacking", false);
    }
}