using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScript : MonoBehaviour
{
    // create instance of the sub for use on the other parts of the sub
    public static SubScript instance;
    // vars
    // living var
    public bool playerIsAlive = true;

    // sub attachment prefs
    private string SelectedMissiles;
    private string SelectedBoost;

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

    // sub health
    public float subHealth = 3;

    // boost speed and var
    private bool isBoosting = false;
    public float defaultBoostSpeed;

    // special vars for the charged boost
    public float chargeBoostSpeed = 10f;
    public float maxChargeBoostSpeed = 20f;
    public float chargeBoostCounter = 0f;

    // health bar sprite
    public SpriteRenderer HealthBar;
    public Sprite[] HealthBars;

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

    // missile damage vars
    public float DefaultMissileDamage;
    public float TripleMissileDamage;
    public float BigMissileDamage;

    // special vars for the charged missile
    public float chargeMissileSpeed = 10f;
    public float chargeMissileDamage = 1f;
    public float maxChargeMissileDamage = 4f;
    public float maxChargeMissileSpeed = 20f;
    public float chargeCounter = 0;

    // function to create the sub
    void MakeSingleTon() {
        instance = this;
    }

    // set the player prefs
    void Awake() {
       MakeSingleTon();
    }

    void Start() {
        GameControllerScript.instance.SetSelectedMissile("default");
        SelectedMissiles = GameControllerScript.instance.GetSelectedMissile();
        //SelectedMissiles = "big";
        SelectedBoost = GameControllerScript.instance.GetSelectedBoost();
        //SelectedBoost = "default";
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
        if (playerIsAlive) {
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
        checkForDeath();
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
            chargeCounter += 0.01f;
            
            // set the values of the speed and damage
            chargeMissileDamage = chargeCounter;
            chargeMissileSpeed = chargeCounter * 2;

            // set the animator
            if (chargeCounter > 0 && chargeCounter <= 1) {
                chargeAnimator.SetTrigger("StartedCharging");
            }
            else if (chargeCounter > 2 && chargeCounter <= 4) {
                chargeAnimator.SetTrigger("ContCharging");
            }
            else if (chargeCounter > 20) {
                chargeAnimator.SetTrigger("FinishedCharging");
            } else {
                chargeAnimator.SetTrigger("Fired");
            }

            // make sure they are under the limit
            if (chargeMissileDamage > maxChargeMissileDamage) chargeMissileDamage = 5f;
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

    // death function
    void checkForDeath() {
        // check health
        if (subHealth <= 0) {
            playerIsAlive = false;
        }

        // render the sprite
        if (subHealth == 4) HealthBar.sprite = HealthBars[3];
        if (subHealth == 3) HealthBar.sprite = HealthBars[2];
        if (subHealth == 2) HealthBar.sprite = HealthBars[1];
        if (subHealth == 1) HealthBar.sprite = HealthBars[0];
    }
}
