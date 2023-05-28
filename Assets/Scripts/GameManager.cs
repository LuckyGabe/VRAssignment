using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float score = 0;
    public TextMeshProUGUI scoreText;
    public List<GameObject> spawnPoints;
    public List<GameObject> enemies;
    public int wavesNumb, spawnEnemiesNumb, additionalEnemiesInNextWave;
    [SerializeField]
    private int currentWaveNumb = 0;
    public bool bWaveCleared = true;
    public int enemiesToClear = 0;
    public TextMeshProUGUI enemiesNumbText, wavesNumbText, prepareForNextWaveText;
    public AudioManager audioManager;
   public bool bGameOver = false;
    [SerializeField]
    private GameObject losePanel, winPanel, mainMenuButton;
    private bool bGamePaused = true;

    private bool bCanSpawn = true;
    public Animator loadLevelPanelAnimator;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesToClear >= 0)
        {
            enemiesNumbText.text = "Enemies: " + enemiesToClear.ToString();
        }
        wavesNumbText.text = "Waves: " + currentWaveNumb.ToString() + "/" + wavesNumb.ToString();

        if (bGamePaused) { return; }


        if (bGameOver) { GameOver(); }

        if (enemiesToClear == 0 && !bWaveCleared)
        {
            bWaveCleared = true;
            StartCoroutine(SpawnEnemies());
            if (currentWaveNumb == wavesNumb) { GameWin(); }
        }



    }

    private IEnumerator SpawnEnemies()
    {
        if (!bWaveCleared || !bCanSpawn || currentWaveNumb == wavesNumb) { yield break; }
        bCanSpawn = false;
        prepareForNextWaveText.gameObject.SetActive(true);
        audioManager.Play("PrepareForNextWave");
        yield return new WaitForSeconds(5f);

        audioManager.Play("EnemySpawned");
        prepareForNextWaveText.gameObject.SetActive(false);

        currentWaveNumb++;
        //Increase number of enemies with new wave
        if (currentWaveNumb > 1) { spawnEnemiesNumb += additionalEnemiesInNextWave; }

        for (int i = 0; i < spawnEnemiesNumb; i++)
        {
            int spawnPoint = Random.Range(0, 10);
            Enemy enemyScript = enemies[i].GetComponent<Enemy>();
            enemyScript.MoveEnemy(spawnPoints[spawnPoint].transform.position);
            enemyScript.agent.enabled = true;
            enemyScript.bCanMove = true;

            enemiesToClear++;
            //Increase enemy movement speed with new wave
            if (currentWaveNumb > 1) { enemies[i].GetComponent<Enemy>().movementSpeed++; }
            yield return new WaitForSeconds(0.4f);
        }
        bWaveCleared = false;
        bCanSpawn = true;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        audioManager.Play("Lose");
        mainMenuButton.SetActive(true);
        losePanel.SetActive(true);
    }
    private void GameWin()
    {  
        Time.timeScale = 0;
        audioManager.Play("Win");
        mainMenuButton.SetActive(true);
        winPanel.SetActive(true);
    }
    private IEnumerator StartGame()
    {
        loadLevelPanelAnimator.SetTrigger("NewLevel");
        yield return new WaitForSeconds(5f);
        loadLevelPanelAnimator.gameObject.SetActive(false);
        bGamePaused = false;
        StartCoroutine(SpawnEnemies());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
}

