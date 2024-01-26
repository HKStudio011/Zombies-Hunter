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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //this.transform.position = new Vector3(0, 0, 0);
        SelectMoveSpeed();
    }

    private void SelectMoveSpeed()
    {
        moveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed + 0.1f);
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

        if((player.transform.position.z - this.transform.position.z) >= StopPositionOfZ)
        {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

    }
}
