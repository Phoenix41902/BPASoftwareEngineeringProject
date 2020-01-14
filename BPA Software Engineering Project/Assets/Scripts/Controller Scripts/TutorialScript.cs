using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public static TutorialScript instance;
    // components
    public Text TutorialDialog;
    public GameObject FirstDoor;
    public GameObject SecondDoor;
    public GameObject ThridDoor;

    // triggers
    public bool HasEnteredSecondRoom = false;
    public bool HasEnteredThridRoom = false;
    public bool HasEnteredBossRoom = false;
    public bool BossIsKilled = false;
    public bool CanSelectedMissile = false;
    private bool hasMovedMissiles = false;

    // missiles
    public GameObject[] Missiles;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartText());
        GameControllerScript.instance.SetSelectedMissile("default");      
    }

    private void Update()
    {
        if (HasEnteredSecondRoom)
        {         
            StartCoroutine(SecondText());
            HasEnteredSecondRoom = false;
        }

        if (HasEnteredThridRoom)
        {
            StartCoroutine(ThirdText());
            HasEnteredThridRoom = false;
        }

        if (BossIsKilled)
        {
            if (!hasMovedMissiles)
            {
                for (int i = 0; i < Missiles.Length; i++)
                {
                    Missiles[i].transform.position = Missiles[i].transform.position + new Vector3(0, 10, 0);
                }
                hasMovedMissiles = true;
            }
            StartCoroutine(FinalText());
            BossIsKilled = false;
        }
    }

    // Coroutines for the player tutorial
    IEnumerator StartText()
    {
        // make it so the player cannot move
        SubScript.instance.playerIsAlive = false;
        // first text
        TutorialDialog.text = "Hello D-8739. I know you don't know who I am, but I know who you are. Earlier today our oceans were suddenly flooded with creatures beyond any of our worst nightmares.";
        yield return new WaitForSeconds(4f);
        TutorialDialog.text = "You were one of the best submarine operators in your day, D-8739. We need you to exterminate these nightmarish things for us.";
        yield return new WaitForSeconds(4f);
        TutorialDialog.text = "This submarine is controlled with a keyboard and mouse. Right click to move towards your cursor, then head to the next room.";
        SubScript.instance.playerIsAlive = true;
        yield return new WaitForSeconds(8f);
        Destroy(FirstDoor);
    }

    IEnumerator SecondText()
    {
        TutorialDialog.text = "Now that you can move, you need to learn how to shoot. Left click to fire a missile in whatever direction you are facing.";
        yield return new WaitForSeconds(4f);
        TutorialDialog.text = "Good. Now destroy that target to open the door and move to the next room.";
    }

    IEnumerator ThirdText() 
    {
        Destroy(SecondDoor);
        TutorialDialog.text = "Now you need to learn how to boost. This is very helpful when dealing with many enemies.";
        yield return new WaitForSeconds(4f);
        TutorialDialog.text = "To boost press left shift. You will shoot forward with a sudden burst of momentum.";
        yield return new WaitForSeconds(4f);     
        TutorialDialog.text = "Excellent work. Now you must save us all. In the next room you will find a nightmarish alien creature. Terminate it.";
        Destroy(ThridDoor);
    }

    IEnumerator FinalText()
    {
        TutorialDialog.text = "Well done. There are now three different missiles which you can choose from in front of you.";
        yield return new WaitForSeconds(4f);
        TutorialDialog.text = "The green Big Missile is slow but packs quite a mean punch. The purple Charge Missile can be charged to inflict massive damage upon your enemies. Finally the small Triple Missile is a high speed but low power three round burst.";
        yield return new WaitForSeconds(8f);
        TutorialDialog.text = "Drive into the one you want, and proceed on your quest. The fate of the world is in your hands. Good luck!";
        CanSelectedMissile = true;   
    }
}
