using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int knifeCount; // Số knife
    [Header("Knife Spawning")]
    public Vector2 knifeSpawnPos; //Vị trí spawn knive
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
    //kiểm tra đã ném hết dao hay chưa
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
    //Spawn knife
    private void SpawnKnife()
    {
        knifeCount--;
        Instantiate(knifePrefab, knifeSpawnPos, Quaternion.identity);
    }

    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequenceCoroutine", win);
    }
    //kiểm tra xem game đã kết thúc hay chưa
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
    //Restart game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
