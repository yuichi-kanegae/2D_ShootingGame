using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    private GameObject title;
    public GameObject gameClearText;
    public GameObject gameOverText;
    public Text scoreText;
    public Text highScoreText;
    public Text timeText;

    public int score = 0;
    public int highScore ;
    private float CountUp = 0;
    private bool isCalledOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("SCORE", 0);
        title = GameObject.Find ("Title");
        gameOverText.SetActive(false);
        scoreText.text = "SCORE:"+ score;
        highScoreText.text = "HIGHSCORE:" + highScore ;
        isCalledOnce = false;
    }

    void Update()
    {   
        // ゲーム中ではなく、Xキーが押されたらゲームスタート
        if (IsPlaying () == false && Input.GetKeyDown (KeyCode.X)) 
        {
            GameStart (); 
        }

        // カウントアップ
        if (IsPlaying () == true)
        {
            CountUp += Time.deltaTime;
            timeText.text = "Time :" + (int)CountUp +" s" ;
        }

        //ゲームオーバー時・ゲームクリア時のリセット
        if (gameOverText.activeSelf == true)
        {
            SaveScore();
            Reset();
        }

        if (gameClearText.activeSelf == true)
        {    
            
        if(!isCalledOnce)
        {
            isCalledOnce = true;
            score += 500 - (int)CountUp;
            scoreText.text = "SCORE:" + score;
        }

        SaveScore();
        Reset();

        }
    }

    void GameStart ()
    {
        // ゲームスタート時に、タイトルを非表示
        title.SetActive (false);
    }

    public int AddScore()
    {
        score += 100;
        scoreText.text = "SCORE:" + score;
        return score;
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
    }

    public bool IsPlaying ()
    {
        // ゲーム中かどうかはタイトルの表示/非表示で判断する
        return title.activeSelf == false;
    }

    public void GameClear()
    {
        gameClearText.SetActive(true);        
    }

    public void SaveScore()
    {
        //ハイスコアを超えた場合に更新
        if (highScore < score) 
        {
            highScore = score;
            PlayerPrefs.SetInt("SCORE", highScore);
            PlayerPrefs.Save();
        }
    }

    public void Reset()
    {
        //Spaceで初期画面に戻る
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
