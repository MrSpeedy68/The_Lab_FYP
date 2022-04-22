using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class PauseMenuManager : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public XRNode inputSource;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public Toggle movementToggle;
    public Slider audioSlider;

    private bool isMenuPressed;
    private bool isAllowedToPress = true;
    private float interval = 0.25f;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);

        movementToggle.isOn = PlayerData.isTeleport; 

        SwitchMovementType();
        ChangeAudioLevel();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.menuButton, out isMenuPressed); //Get controller and the output joystick values

        //Debug.Log(isAllowedToPress);

        if (isMenuPressed && isAllowedToPress)
        {
            StartCoroutine(TimeDelayOnClick());
            
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    IEnumerator TimeDelayOnClick()
    {
        isAllowedToPress = false;
        yield return new WaitForSecondsRealtime(interval);
        isAllowedToPress = true;
        yield return null;
    }

    public void OptionsMenu()
    {
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void PauseMenu()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void SaveGame()
    {
        //Save Game
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        Player _player = playerObj.GetComponent<Player>();

        if (_player)
        {
            PlayerData.pistolAmmo = _player.pistolAmmo;
            PlayerData.shotgunAmmo = _player.shotgunAmmo;
            PlayerData.rifleAmmo = _player.rifleAmmo;

            PlayerData.playerPos = playerObj.gameObject.transform.position;
            PlayerData.health = _player.health;
            
            SaveData();
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("pistolAmmo", PlayerData.pistolAmmo);
        PlayerPrefs.SetInt("shotgunAmmo", PlayerData.shotgunAmmo);
        PlayerPrefs.SetInt("rifleAmmo", PlayerData.rifleAmmo);
        PlayerPrefs.SetFloat("health", PlayerData.health);
        
        PlayerPrefs.SetFloat("XPos", PlayerData.playerPos.x);
        PlayerPrefs.SetFloat("YPos", PlayerData.playerPos.y);
        PlayerPrefs.SetFloat("ZPos", PlayerData.playerPos.z);
    }

    public void SwitchMovementType()
    {
        //Switch From Teleportation to Locomotion or vise versa
        
        PlayerData.isTeleport = movementToggle.isOn; 
        
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        LocomotionController _locomotionController = playerObj.GetComponent<LocomotionController>();

        if (_locomotionController)
        {
            _locomotionController.SwitchMovementType(PlayerData.isTeleport);
        }

        ContinuousMovement _continuousMovement = playerObj.GetComponent<ContinuousMovement>();

        if (_continuousMovement)
        {
            _continuousMovement.SwitchState();
        }
    }

    public void ChangeAudioLevel()
    {
        //Read in slider value and change global audio volume
        AudioListener.volume = audioSlider.value;
    }

    public void QuitMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
