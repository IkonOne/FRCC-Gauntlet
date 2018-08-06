using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    public string GameplaySceneName = "worldMap";
    public PlayerSelectorData PlayerSelector;

	// Use this for initialization
	void Start () {
        PlayerSelector.Reset();
	}

    public void OnPlayeClicked() {
        SceneManager.LoadScene(GameplaySceneName);
    }
}
