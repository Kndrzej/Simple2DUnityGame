using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMain();
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}