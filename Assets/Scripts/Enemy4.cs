using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float health = 5f;
    [SerializeField] private float attackDamage = 0.5f;
    [SerializeField] private float xScale = 22f;
    [SerializeField] private float yScale = 22f;
    [SerializeField] private float zScale = 1f;
    [SerializeField] private float attackRange = 9.5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private Animator anim;
    private string walkAnimation = "walk";
    private string attackAnimation = "attack";


    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        gameObject.tag = "Enemy";
    }

    private void Update()
    {
        getMovement(player);

        if (getHealth() <= 0) //if health is 0 or less, enemy is dead
        {
            Die();
            Debug.Log(gameObject + " being destroyed.");
        }
    }

    private void FixedUpdate()
    {
        MoveEnemy(movement);
        AnimateEnemy(movement);
    }

    public void setHealth(float health)
    {
        this.health = health;
    }

    public float getHealth()
    {
        return this.health;
    }


    void getMovement(Transform player)
    {
        Vector3 direction = player.position - transform.position; //tranform is the enemy since our script is attached to enemy character
        /*float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; ////Atan2- calculates the angle between enemy and player
        rb.rotation = angle;*/
        direction.Normalize();
        movement = direction;
    }


    void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        if (player.position.x > transform.position.x) //flip sprite
        {
            //player on right
            transform.localScale = new Vector3(xScale, yScale, zScale);
        }
        else
        {
            //player on left
            transform.localScale = new Vector3(-xScale, yScale, zScale);
        }
    }

    void AnimateEnemy(Vector2 movement)
    {
        if (movement.x != 0)
        {
            anim.SetBool(walkAnimation, true);
        }
        else
        {
            anim.SetBool(walkAnimation, false);
        }
        if (getDistance(player) <= attackRange) //attack animation
        {
            anim.SetBool(attackAnimation, true);
            Attack(player);
        }
        else
        {
            anim.SetBool(attackAnimation, false);
        }

    }

    public float TakeDamage(float damage)
    {
        float damageTaken = damage;
        float newHealth;

        if (damageTaken >= health)
        {
            damageTaken = health;
        }

        newHealth = getHealth() - damageTaken;
        setHealth(newHealth);
        Debug.Log("Damage taken, Health: " + getHealth());
        return damageTaken;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private float getDistance(Transform player)
    {
        float distance = Vector3.Distance(player.position, transform.position);
        return distance;
    }


    public void Attack(Transform player)
    {
        //call function to deal player damage
        Debug.Log(gameObject + " attacking " + player);

        player.GetComponent<PlayerCharacter>().TakeDamage(attackDamage);
    }
}
