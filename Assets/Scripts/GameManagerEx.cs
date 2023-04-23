using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerEx : MonoBehaviour
{
    [SerializeField] private Sprite[] m_cutScenes;
    [SerializeField] private string[] m_cutSceneTexts;

    private MealGenerator m_mealGenerator;
    private WorkGenerator m_workGenerator;

    private int m_bestScore;
    private int m_score;

    private void Start()
    {
        m_bestScore = PlayerPrefs.GetInt("DWD_score");
        DontDestroyOnLoad(this);
        StartCoroutine(IntroScene());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private IEnumerator IntroScene()
    {
        int cutSceneNum = 0;

        SpriteRenderer cutScene = GameObject.Find("CutScene").GetComponent<SpriteRenderer>();
        Text cutSceneText = GameObject.Find("CutSceneText").GetComponent<Text>();

        cutScene.sprite = m_cutScenes[cutSceneNum];
        cutSceneText.text = m_cutSceneTexts[cutSceneNum];

        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (cutSceneNum == m_cutScenes.Length - 1) break;
                cutSceneNum++;
                cutScene.sprite = m_cutScenes[cutSceneNum];
                cutSceneText.text = m_cutSceneTexts[cutSceneNum];
            }
        }

        StartCoroutine(ChangeScene("TitleScene", () => StartCoroutine(TitleScene())));
    }

    private IEnumerator TitleScene()
    {
        Text text = GameObject.Find("TitleSceneScore").GetComponent<Text>();
        text.text = $"전설의 졸업생의 기록 : {m_bestScore}개";

        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ChangeScene("GameScene", () => StartCoroutine(GameScene())));
                break;
            }
        }
    }

    private IEnumerator GameScene()
    {
        m_score = 0;
        m_mealGenerator = GameObject.FindObjectOfType<MealGenerator>();
        m_workGenerator = GameObject.FindObjectOfType<WorkGenerator>();

        StartCoroutine(DifficultyUpgradeCoroutine());
        yield return null;
    }

    private IEnumerator EndScene()
    {
        Text text = GameObject.Find("EndSceneScore").GetComponent<Text>();
        text.text = $"\r\n\r\n\r\n해치운 과제 : {m_score}개";

        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ChangeScene("TitleScene", () => StartCoroutine(TitleScene())));
                break;
            }
        }
    }

    private IEnumerator DifficultyUpgradeCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(10);
            m_mealGenerator.AddDelay(0.1f);
            m_workGenerator.AddDelay(-0.1f);
        }
    }

    private IEnumerator ChangeScene(string _scene, Action _action)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_scene);

        while (!operation.isDone)
            yield return null;

        _action.Invoke();
    }

    public void AddScore()
    {
        m_score++;
    }

    public void EndGame()
    {
        if (m_score > m_bestScore) m_bestScore = m_score;
        PlayerPrefs.SetInt("DWD_score", m_bestScore);
        StartCoroutine(ChangeScene("EndScene", () => StartCoroutine(EndScene())));
    }
}
