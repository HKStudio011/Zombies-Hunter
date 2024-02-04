using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Zombie Controller")]
public class ZombieController : MonoBehaviour
{
    [Header("Speed")]
    public float MinMoveSpeed = 1.0f;
    public float MaxMoveSpeed = 3.0f;
    public float MinAttackSpeed = 1.0f;
    public float MaxAttackSpeed = 3.0f;
    private float lastTImeOfAttack;
    private float moveSpeed;
    private float attackSpeed;
    [Header("Range")]
    public float AttackRange = 1.0f;

    private GameObject player;
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
        SelectAttackSpeed();
    }

    private void SelectAttackSpeed()
    {
        attackSpeed = Random.Range(MinAttackSpeed, MaxAttackSpeed);
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

    void UpdateLastTimeOfAttack()
    {
        lastTImeOfAttack = Time.time;
    }

    void SetAttack()
    {
        UpdateLastTimeOfAttack();
        animator.SetTrigger("Attack");
        animator.SetTrigger("Idle Affter Attack");
    }

    void MoveToPlayer()
    {
        if(player == null)
        {
            return;
        }
        this.transform.LookAt(player.transform);

        if(Vector3.Distance(player.transform.position,this.transform.position) >= AttackRange  && moveSpeed != 0.0f)
        {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            moveSpeed = 0.0f;
        }

        if(moveSpeed == 0.0f && !IsDead)
        {
            if(lastTImeOfAttack == 0.0f)
            {
                SetAttack();
            }
            else if((lastTImeOfAttack + attackSpeed) <= Time.time)
            {
                SetAttack();
            }
        }

    }
    public void SetDead()
    {
        IsDead = true;
        var childComponents = gameObject.GetComponentsInChildren<MeshCollider>();
        foreach(var childComponent in childComponents)
        {
            childComponent.enabled = false;
        }
        animator.SetTrigger("Dead");
    }
    // animation event
    void Dead()
    {
        moveSpeed = 0.0f;
        Destroy(this.gameObject, DestroyTime);
    }

    private void Attack()
    {
        
    }
}
