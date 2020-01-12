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
            StartCoroutine(ThridText());
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
        TutorialDialog.text = "Good evening. I know you do not know who I am, but I know who you are. Earlier today our oceans were flooded with nightmares beyond anything we were ever prepared for.";
        yield return new WaitForSeconds(4f);
        TutorialDialog.text = "Your were an excelent sub driver in your day. We need you to use this one to stop the nightmares.";
        yield return new WaitForSeconds(4f);
        TutorialDialog.text = "This sub is controlled with a keyboard and mouse. I will fill you in on how to use it. First right click to move towards your cursor, then head to the next room";
        SubScript.instance.playerIsAlive = true;
        yield return new WaitForSeconds(8f);
        Destroy(FirstDoor);
    }

    IEnumerator SecondText()
    {
        TutorialDialog.text = "Now that you can move, you need to learn to shoot. Left click to fire a missile in which ever direction you are facing.";
        yield return new WaitForSeconds(4f);
        TutorialDialog.text = "Good. Now destroy that target to open the door then move to the next room.";
    }

    IEnumerator ThridText()
    {
        Destroy(SecondDoor);
        TutorialDialog.text = "Now all you need to know is how to boost. This is very helpful when dealing with many enemys.";
        yield return new WaitForSeconds(4f);
        TutorialDialog.text = "To boost press enter. You will shoot forward with a sudden burst of momentum.";
        yield return new WaitForSeconds(4f);     
        TutorialDialog.text = "Excellent work. Now all that remains is to save us all. In the next room you will find a nightmare. Kill it.";
        Destroy(ThridDoor);
    }

    IEnumerator FinalText()
    {
        TutorialDialog.text = "Well done. Before you are three different missiles. Each works differently.";
        yield return new WaitForSeconds(4f);
        TutorialDialog.text = "The green one is the big missile, slow with high damage. The purple is the charge missile, it can be charged to do massive damage. Finally the small one is the triple missile, a low power three round burst.";
        yield return new WaitForSeconds(8f);
        TutorialDialog.text = "Drive into the one you want, and procede on your quest.";
        CanSelectedMissile = true;
    }
}
