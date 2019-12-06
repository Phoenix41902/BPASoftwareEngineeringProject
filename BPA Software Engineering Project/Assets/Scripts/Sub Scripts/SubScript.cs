﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScript : MonoBehaviour
{
    // create instance of the sub for use on the other parts of the sub
    public static SubScript instance;
    // vars
    // sub attachment prefs
    private string SelectedMissiles;
    private string SelectedBoost;
    private string SelectedArms;

    // animators
    [SerializeField]
    private Animator exhaustAnimator;
    [SerializeField]
    private Animator missileAnimator;
    [SerializeField]
    private Animator chargeAnimator;

    // sub speed
    public float subSpeed = 100f;
    public float subDecelSpeed = 2f;

    // boost speed and var
    private bool isBoosting = false;
    public float defaultBoostSpeed;

    // special vars for the charged boost
    public float chargeBoostSpeed = 10f;
    public float maxChargeBoostSpeed = 20f;
    public float chargeBoostCounter = 0f;

    // rigid body
    [SerializeField]
    private Rigidbody2D rb;

    // missiles vars
    public Transform FirePoint;
    public GameObject DefaultMissilePrefab;
    public GameObject TripleMissilePrefab;
    public GameObject BigMissilePrefab;
    public GameObject ChargeMissilePrefab;
    private bool canFire = true;

    // special vars for the charged missile
    public float chargeMissileSpeed = 10f;
    public float chargeMissileDamage = 5f;
    public float maxChargeMissileDamage = 20f;
    public float maxChargeMissileSpeed = 20f;
    private float chargeCounter = 0;

    // function to create the sub
    void MakeSingleTon() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    // set the player prefs
    void Awake() {
        MakeSingleTon();
        //SelectedMissiles = GameControllerScript.instance.GetSelectedMissile();
        SelectedMissiles = "charge";
        SelectedBoost = GameControllerScript.instance.GetSelectedBoost();
        SelectedArms = GameControllerScript.instance.GetSelectedArms();
    }

    // set pos to 0, 0 (REMOVE LATER)
    void Start() {
        transform.position = new Vector3(0f, 0f, 0f);

        // set what shows up on the sub
        // missiles
        if (SelectedMissiles ==  "triple") 
        {
            missileAnimator.SetTrigger("TripleSelected");
        }
        else if (SelectedMissiles == "big") 
        {
            missileAnimator.SetTrigger("BigSelected");
        } 
        else if (SelectedMissiles == "charge") {
            missileAnimator.SetTrigger("ChargeSelected");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check if boosting
        if (!isBoosting) {
            // call movement
            faceAndMoveToMouse();

            // call missiles
            if (canFire && Input.GetButton("FireMissiles")) {
                StartCoroutine(fireMissiles());
            }

            // charged missiles has special call
            if (Input.GetButton("FireMissiles")) {
                if (SelectedMissiles == "charge") {
                    fireChargeMissile();              
                }
            }

            // create object after charge
            if (Input.GetButtonUp("FireMissiles")) {
                if (SelectedMissiles == "charge") {
                    Instantiate(ChargeMissilePrefab, FirePoint.position, FirePoint.rotation);
                }   
            }
        }

        if (Input.GetButtonDown("Boost")) {
            defaultBoost();
        }


    }

    // create delay between missile shots
    IEnumerator fireMissiles() {
        // call missiles
        if (Input.GetButtonDown("FireMissiles")) {
            // check which missile to use
            if (SelectedMissiles == "default") {
                fireDefaultMissile();
            }
            else if (SelectedMissiles == "big") {
                fireBigMissile();
            } 
            else if (SelectedMissiles == "triple") {
                fireTripleMissile();
            }
        }

        // delay
        // charge does not need delay
        canFire = false;
        yield return new WaitForSeconds(0.3f);
        canFire = true;
    }

    // calls after all other updates, has the button up for the charge missile
    void LateUpdate() {
        if(Input.GetButtonUp("FireMissiles")) {
            chargeMissileDamage = 5f;
            chargeMissileSpeed = 5f;
            chargeCounter = 0f;
            chargeAnimator.ResetTrigger("StartedCharging");
            chargeAnimator.ResetTrigger("ContCharging");
            chargeAnimator.ResetTrigger("FinishedCharging");
            chargeAnimator.SetTrigger("Fired");
        }
    }

    // basic movement function
    private void faceAndMoveToMouse() {
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

    // Missile functions
    // default
    private void fireDefaultMissile() {
        Instantiate(DefaultMissilePrefab, FirePoint.position, FirePoint.rotation);
    }

    // triple
    private void fireTripleMissile() {
        StartCoroutine(fireTripleMissileWaiter());
    }
    // delay for the missiles to fire
    IEnumerator fireTripleMissileWaiter() {
        Instantiate(TripleMissilePrefab, FirePoint.position, FirePoint.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(TripleMissilePrefab, FirePoint.position, FirePoint.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(TripleMissilePrefab, FirePoint.position, FirePoint.rotation);
    }

    // big missile
    private void fireBigMissile() {
        Instantiate(BigMissilePrefab, FirePoint.position, FirePoint.rotation);
    }

    // charge missile
    private void fireChargeMissile() {
        if (Input.GetButton("FireMissiles")) {
            chargeCounter += 0.15f;
            
            // set the values of the speed and damage
            chargeMissileDamage = chargeCounter;
            chargeMissileSpeed = chargeCounter;

            // set the animator
            if (chargeCounter > 0 && chargeCounter <= 10) {
                chargeAnimator.SetTrigger("StartedCharging");
            }
            else if (chargeCounter > 10 && chargeCounter <= 20) {
                chargeAnimator.SetTrigger("ContCharging");
            }
            else if (chargeCounter > 20) {
                chargeAnimator.SetTrigger("FinishedCharging");
            } else {
                chargeAnimator.SetTrigger("Fired");
            }

            // make sure they are under the limit
            if (chargeMissileDamage > maxChargeMissileDamage) chargeMissileDamage = 25f;
            if (chargeMissileSpeed > maxChargeMissileSpeed) chargeMissileSpeed = 25f;
        }
    }

    // section for boosts

    // boost function
    private void defaultBoost() {
        // gets mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // gets the new direction for where the player should face
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        // move in that direction with a fixed speed
        StartCoroutine(defaultBoostDelay(direction));
    }

    // create boost delay
    IEnumerator defaultBoostDelay(Vector2 direction) {
        rb.velocity = transform.right * defaultBoostSpeed;
        Debug.Log(rb.velocity);
        isBoosting = true;
        yield return new WaitForSeconds(0.45f);
        isBoosting = false;
    }

    // create charge boost
   private void chargeBoost() {
        if (Input.GetButton("FireMissiles")) {
            chargeBoostCounter += 0.15f;
            
            // set the values of the speed and damage
            chargeMissileDamage = chargeCounter;
            chargeMissileSpeed = chargeCounter;

            // set the animator
            if (chargeCounter > 0 && chargeCounter <= 10) {
                chargeAnimator.SetTrigger("StartedCharging");
            }
            else if (chargeCounter > 10 && chargeCounter <= 20) {
                chargeAnimator.SetTrigger("ContCharging");
            }
            else if (chargeCounter > 20) {
                chargeAnimator.SetTrigger("FinishedCharging");
            } else {
                chargeAnimator.SetTrigger("Fired");
            }

            // make sure they are under the limit
            if (chargeMissileDamage > maxChargeMissileDamage) chargeMissileDamage = 25f;
            if (chargeMissileSpeed > maxChargeMissileSpeed) chargeMissileSpeed = 25f;
        }
    }
}
