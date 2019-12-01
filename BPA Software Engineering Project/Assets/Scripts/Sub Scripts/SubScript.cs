using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScript : MonoBehaviour
{

    // variables
    [SerializeField]
    private Animator animator;

    // speed
    public float subSpeed = 100f;

    [SerializeField]
    private Rigidbody2D rb;

    // awake function called before first frame
    void Start() {
        transform.position = new Vector3(0f, 0f, 0f);
    }
    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 mousePos = Input.mousePosition;
        float step = subSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, mousePos, step);
        */

       faceAndMoveToMouse();
    }

    void faceAndMoveToMouse() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.right = direction;

        if (Input.GetMouseButton(1)) {
            rb.velocity = new Vector2(direction.x * subSpeed, direction.y * subSpeed);
        } else {
           rb.velocity = Vector2.zero;
        }
    }
}
