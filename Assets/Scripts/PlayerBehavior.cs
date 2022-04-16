using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    CharacterController _controller;
    public float moveSpeed;
    public float gravity = 9.81f;
    Vector3 input, moveDirection;
    public Slider healthSlide;
    Animator anim;
    int playerHealth = 100;
    public AudioClip sword;
    public AudioClip hurt;
    public AudioClip dash;
    public bool attacking = false;
    public float dashSpeed = 50f;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Attack"))
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
        if (playerHealth > 0)
        {
            Move();
            Dash();
            if (Input.GetKey(KeyCode.E))
            {
                // attacking = true;
                anim.SetInteger("Movement", 3);
                AudioSource.PlayClipAtPoint(sword, transform.position);
            }
        }
        else
        {
            anim.SetInteger("Movement", 4);
        }

    }

    private void Move()
    {
        // attacking = false;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            anim.SetInteger("Movement", 1);
            input = Quaternion.Euler(0, 45, 0) * new Vector3(moveHorizontal, 0, moveVertical);
            input.Normalize();
            moveDirection = input;
            moveDirection.y -= gravity * Time.deltaTime;
            _controller.Move(moveDirection * moveSpeed * Time.deltaTime);
            if (moveDirection != Vector3.zero)
            {
                transform.forward = moveDirection;
            }
        }
        else
        {
            anim.SetInteger("Movement", 0);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (playerHealth > dmg)
        {
            playerHealth = playerHealth - dmg;
            AudioSource.PlayClipAtPoint(hurt, transform.position);
            healthSlide.value = playerHealth;

        }
        else
        {
            playerHealth = 0;
            healthSlide.value = playerHealth;
            FindObjectOfType<LevelManager>().LevelLost();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(10);
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetInteger("Movement", 2);
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time;
        AudioSource.PlayClipAtPoint(dash, transform.position);
        while (Time.time < startTime + 0.15f)
        {
            _controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
