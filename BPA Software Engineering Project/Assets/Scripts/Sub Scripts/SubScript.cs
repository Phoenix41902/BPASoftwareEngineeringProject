using System.Collections;
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
    private Animator subAnimator;
    [SerializeField]
    private Animator exhaustAnimator;
    [SerializeField]
    private Animator missileAnimator;

    // speed and boost
    public float subSpeed = 100f;
    public float subDecelSpeed = 2f;

    // rigid body
    [SerializeField]
    private Rigidbody2D rb;

    // missiles vars
    public Transform FirePoint;
    public GameObject DefaultMissilePrefab;
    public GameObject TrippleMissilePrefab;
    public GameObject BigMissilePrefab;
    public GameObject ChargeMissilePrefab;

    // special vars for the charged missile
    public float chargeMissileSpeed = 10f;
    public float chargeMissileDamage = 10f;
    private int chargeCounter = 0;

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
        SelectedMissiles = "default";
        SelectedBoost = GameControllerScript.instance.GetSelectedBoost();
        SelectedArms = GameControllerScript.instance.GetSelectedArms();
    }

    // set pos to 0, 0 (REMOVE LATER)
    void Start() {
        transform.position = new Vector3(0f, 0f, 0f);

        // set what shows up on the sub
        // missiles
        if (SelectedMissiles ==  "tripple") 
        {
            missileAnimator.SetTrigger("TrippleSelected");
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
        // call movement
        faceAndMoveToMouse();

        // call missiles
        if (Input.GetButton("FireMissiles")) {
            // check which missile to use
            if (SelectedMissiles == "default") {
                fireDefaultMissile();
            }
            else if (SelectedMissiles ==  "tripple") {
                fireTrippleMissile();
            }
            else if (SelectedMissiles == "big") {
                fireBigMissile();
            } 
            else if (SelectedMissiles == "charge") {
                fireChargeMissile();
            }
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

    // tripple
    private void fireTrippleMissile() {
        StartCoroutine(fireTrippleMissileWaiter());
    }
    // delay for the missiles to fire
    IEnumerator fireTrippleMissileWaiter() {
        Instantiate(TrippleMissilePrefab, FirePoint.position, FirePoint.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(TrippleMissilePrefab, FirePoint.position, FirePoint.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(TrippleMissilePrefab, FirePoint.position, FirePoint.rotation);
    }

    // big missile
    private void fireBigMissile() {
        Instantiate(BigMissilePrefab, FirePoint.position, FirePoint.rotation);
    }

    // charge missile
    private void fireChargeMissile() {
        if (Input.GetButton("FireMissiles")) {
            chargeCounter++;
            if(chargeCounter >= 100) {
                Debug.Log("Worked!");
            }
        }
        //Instantiate(ChargeMissilePrefab, FirePoint.position, FirePoint.rotation);
    }
}
