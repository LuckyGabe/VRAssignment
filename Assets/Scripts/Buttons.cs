
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    public GameObject menuPanel, howToPlayPanel;
    public Animator loadLevelPanelAnimator;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void OpenLevel()
    {
        if(loadLevelPanelAnimator != null)
        {
            loadLevelPanelAnimator.SetTrigger("LoadLevel");
        }
        StartCoroutine(LoadLevel());
    }

    public IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

