using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // fade to next level
    public Animator anim;

    IEnumerator fadeOut()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(3f);
        GameControllerScript.instance.SetStage("three");
        SceneManager.LoadScene("AbyssalZone");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerSub")
        {
            StartCoroutine(fadeOut());
        }
    }
}
