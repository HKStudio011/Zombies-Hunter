using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Zombie Controller")]
public class ZombieController : MonoBehaviour
{
    [Header("Speed")]
    public float MinMoveSpeed = 0.05f;
    public float MaxMoveSpeed = 0.3f;
    private float moveSpeed;
    [Header("Stop Position")]
    public float StopPositionOfZ = 1.0f;
    private GameObject player;
    private Vector3 target;
    private Animator animator;
    [Header("DesTroy")]
    public float DestroyTime = 2.0f;
    private bool IsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        SelectMoveSpeed();
    }

    private void SelectMoveSpeed()
    {
        moveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);
        if(animator != null) 
        {
            if(moveSpeed >= (MinMoveSpeed+ MaxMoveSpeed) / 2)
            {
                animator.SetTrigger("Run");
            }
            else
            {
                animator.SetTrigger("Walk");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if(target == null)
        {
            return;
        }
        this.transform.LookAt(player.transform);

        if((player.transform.position.z - this.transform.position.z) >= StopPositionOfZ && moveSpeed != 0.0f)
        {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            moveSpeed = 0.0f;
        }

        if(moveSpeed == 0.0f && !IsDead)
        {
            animator.SetTrigger("Attack");
        }

    }

    public void SetDead()
    {

        animator.SetTrigger("Dead");
    }

    // animation event
    void Dead()
    {
        IsDead = true;
        moveSpeed = 0.0f;
        Destroy(this.gameObject, DestroyTime);
    }
}
