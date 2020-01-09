using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public bool CanTarget;
    public float Health;

    private void Update()
    {
        // check if can agro
        if (Vector2.Distance(transform.position, target.position) < 20)
        {
            CanTarget = true;
        }
        else
        {
            CanTarget = false;
        }

        // target
        if (CanTarget)
        {
            // move toward the target
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // face the point
            // gets the new direction for where the player should face
            Vector2 direction = new Vector2(
                target.position.x - transform.position.x,
                target.position.y - transform.position.y
            );

            // changes the direction
            transform.right = direction;
        }

        // see if dead
        if (Health <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // do damage to sub
        if (collision.tag == "PlayerSub")
        {
            SubScript.instance.subHealth--;
        }

        // check if it is a missile and which one then subtract the health
        if (collision.tag == "ChargeMissile") Health -= SubScript.instance.chargeMissileDamage;
        if (collision.tag == "DefaultMissile") Health -= SubScript.instance.DefaultMissileDamage;
        if (collision.tag == "TripleMissile") Health -= SubScript.instance.TripleMissileDamage;
        if (collision.tag == "BigMissile") Health -= SubScript.instance.BigMissileDamage;
    }
}
