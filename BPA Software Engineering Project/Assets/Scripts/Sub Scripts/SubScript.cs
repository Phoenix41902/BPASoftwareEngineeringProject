using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScript : MonoBehaviour
{
    // vars
    // animators
    [SerializeField]
    private Animator subAnimator;
    [SerializeField]
    private Animator exhaustAnimator;

    // speed and boost
    public float subSpeed = 100f;
    public float subDecelSpeed = 2f;
    private float subDefaultBoostSpeed = 5f;
    private float subDefaultBoostTime;
    private float startDefaultDashTime = 1f;
    private bool isBoosting = false;

    // rigid body
    [SerializeField]
    private Rigidbody2D rb;

    // set pos to 0, 0
    void Start() {
        transform.position = new Vector3(0f, 0f, 0f);
        subDefaultBoostTime = startDefaultDashTime;
    }
    // Update is called once per frame
    void Update()
    {
        // call movement
        faceAndMoveToMouse();

        // call boost
        if (!isBoosting) {
            //defaultBoost();
        }
    }

    // basic movement function
    void faceAndMoveToMouse() {
        // gets mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // gets the new direction for where the player should face
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        // changes the direction
        transform.right = direction;

        // if the player inputs move, if not, decellerate
        if (Input.GetButton("MoveSub")) {
            rb.velocity = new Vector2(direction.x * subSpeed, direction.y * subSpeed);
            exhaustAnimator.SetTrigger("NormalMovementStart");
        } else {
           // decelleration (for some reason the var breaks this will investigate later *subDecelSpeed*)
           if (rb.velocity.x > 0) {
               rb.velocity = new Vector2(rb.velocity.x - 2f * Time.deltaTime, rb.velocity.y);
           }
           if (rb.velocity.x < 0) {
               rb.velocity = new Vector2(rb.velocity.x + 2f * Time.deltaTime, rb.velocity.y);
           }
           if (rb.velocity.y > 0) {
               rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 2f * Time.deltaTime);
           }
           if (rb.velocity.y < 0) {
               rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 2f * Time.deltaTime);
           }
           exhaustAnimator.SetTrigger("NormalMovementStop");
        }
    }

    // default boost function
    /* finish later
    void defaultBoost() {
        if (Input.GetButtonDown("Boost")) {
            Debug.Log("workds");
            isBoosting = true;
        } else {
            if (subDefaultBoostTime <= 0) {
                isBoosting = false;
                subDefaultBoostTime = startDefaultDashTime;
            } else {
                rb.velocity = Vector2. * subDefaultBoostSpeed;
                subDefaultBoostTime -= Time.deltaTime;
            }
        }
    }
    */
}
