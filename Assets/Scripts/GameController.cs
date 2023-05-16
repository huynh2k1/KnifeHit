using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int knifeCount; // Số knife
    [Header("Knife Spawning")]
    public Vector2 knifeSpawnPos;
    public GameObject knifePrefab;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private void Start()
    {
        UIController.instance.InitKnifeIcon(knifeCount);
        SpawnKnife();
    }
    public void OnSuccessfullKnifeHit()
    {
        if(knifeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            StartGameOverSequence(true);
        }
    }
    private void SpawnKnife()
    {
        knifeCount--;
        Instantiate(knifePrefab, knifeSpawnPos, Quaternion.identity);
    }
    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequenceCoroutine", win);
    }
    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if(win)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            RestartGame();
        }
        else
        {
            UIController.instance.ShowRestartButton();
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
