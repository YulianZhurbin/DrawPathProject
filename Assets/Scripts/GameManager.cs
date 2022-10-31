using UnityEngine;
using TMPro;
using System.Collections;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text launchHintText;
    [SerializeField] TMP_Text drawPathHintText;

    private static GameManager instance;

    private bool isGameOver;
    private bool isLaunched;
    private bool isGamePaused;

    public static GameManager Instance
    {
        get { return instance; }
    }

    public bool IsGameOver
    {
        get { return isGameOver; }
    }

    public bool IsLaunched
    {
        get { return isLaunched; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(PlayerController.Instance.DoesPathExist)
            {
                isLaunched = true;
                launchHintText.gameObject.SetActive(false);
            }
        }

        if(Input.GetMouseButton(0))
        {
            drawPathHintText.gameObject.SetActive(false);
        }

        if(!isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                menu.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                menu.SetActive(true);
                Time.timeScale = 0.0f;
            }

            isGamePaused = !isGamePaused;
        }
    }

    public void StopGame()
    {
        isGameOver = true;
        StartCoroutine(ShowMenu(1.0f));
    }

    private IEnumerator ShowMenu(float delay)
    {
        yield return new WaitForSeconds(delay);
        menu.SetActive(true);
    }
}
