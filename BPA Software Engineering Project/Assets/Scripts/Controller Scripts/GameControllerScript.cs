using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    // create instance of the game controller
    public static GameControllerScript instance;

    // constants for the attachment settings
    private const string SELECTED_MISSILE = "missile";
    private const string SELECTED_BOOST = "boost";
    private const string SELECTED_ARMS = "arms";
    private const string CURRENT_STAGE = "stage";

    void Awake() {
        MakeSingleTon();
        StartGameForFirstTime();
    }

    // function to make only one game controller per scene
    void MakeSingleTon() {
        instance = this;
    }

    // function to set up default player prefs and unlocks
    void StartGameForFirstTime() {
        if(!PlayerPrefs.HasKey("GameHasStarted")) {
            PlayerPrefs.SetString(SELECTED_MISSILE, "default");
            PlayerPrefs.SetString(SELECTED_BOOST, "default");
            PlayerPrefs.SetString(SELECTED_ARMS, "default");
            PlayerPrefs.SetString(CURRENT_STAGE, "one");
            PlayerPrefs.SetInt("GameHasStarted", 0);
        }
    }

    // functions to access player prefs outside of this script

    // update functions
    // missiles
    public void SetSelectedMissile(string mis) {
        PlayerPrefs.SetString(SELECTED_MISSILE, mis);
    }

    // boost
    public void SetSelectedBoost(string bst) {
        PlayerPrefs.SetString(SELECTED_BOOST, bst);
    }

    // call functions
    // missiles
    public string GetSelectedMissile() {
        return PlayerPrefs.GetString(SELECTED_MISSILE);
    }

    // boost
    public string GetSelectedBoost() {
        return PlayerPrefs.GetString(SELECTED_BOOST);
    }

    public void SetStage(string stg)
    {
        PlayerPrefs.SetString(CURRENT_STAGE, stg);
    }

    public string GetStage()
    {
        return PlayerPrefs.GetString(CURRENT_STAGE);
    }
}
