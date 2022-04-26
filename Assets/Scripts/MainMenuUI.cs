using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void PlayButton()
    {
        PlayerData.health = 100f;
        PlayerData.pistolAmmo = 30;
        PlayerData.rifleAmmo = 90;
        PlayerData.shotgunAmmo = 20;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void Continue()
    {
        PlayerData.health = PlayerPrefs.GetFloat("health");
        PlayerData.pistolAmmo = PlayerPrefs.GetInt("pistolAmmo");
        PlayerData.rifleAmmo = PlayerPrefs.GetInt("rifleAmmo");
        PlayerData.shotgunAmmo = PlayerPrefs.GetInt("shotgunAmmo");
        
        SceneManager.LoadScene(PlayerPrefs.GetInt("level"));
    }
}
