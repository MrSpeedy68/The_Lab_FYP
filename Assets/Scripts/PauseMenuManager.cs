using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PauseMenuManager : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public XRNode inputSource;
    public GameObject pauseMenuUI;

    private bool isMenuPressed;
    private bool isAllowedToPress = true;
    private float interval = 0.25f;

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.menuButton, out isMenuPressed); //Get controller and the output joystick values

        Debug.Log(isAllowedToPress);

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
        
    }

    public void QuitMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
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
