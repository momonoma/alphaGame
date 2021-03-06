using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour
{
    Animator anim;
    int TotalHealth = 100;
    Transform player;
    public GameObject projectile;
    [SerializeField] private float cooldown = 5;
    private float cooldownTimer;
    public Slider health;
    public GameObject finalExit;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TotalHealth > 0)
        {
            transform.LookAt(player);
            TargetedAttack();
        }
        else
        {
            anim.SetInteger("AttackPattern", 3);
            finalExit.SetActive(true);
            
        }
    }

    public void TakeDamage(int dmg)
    {
        int tempHealth = TotalHealth - dmg;
        TotalHealth = tempHealth;
        health.value = TotalHealth;
        Debug.Log(TotalHealth);
    }

    void TargetedAttack()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer > 0) return;

        cooldownTimer = cooldown;

        GameObject tempBullet = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        tempBullet.transform.LookAt(player);
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * 50f, ForceMode.Impulse);
        Destroy(tempBullet, 2f);
    }
}
