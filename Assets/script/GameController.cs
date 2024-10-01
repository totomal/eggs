using System.Collections;
using System.Text; // StringBuilderを使用するために追加
using UnityEngine;
using TMPro; // TextMeshProのために追加
using UnityEngine.SceneManagement; // シーン管理用

public class GameController : MonoBehaviour
{
    public int life = 3;                      // プレイヤーのライフ
    public int score = 0;                     // スコア
    public float timeRemaining = 60f;         // 残り時間
    public TMP_Text scoreText;                 // スコア表示用テキスト
    public TMP_Text lifeText;                  // ライフ表示用テキスト
    public TMP_Text timerText;                 // タイマー表示用テキスト
    public GameObject gameOverPanel;           // ゲームオーバーパネル
    public TMP_Text gameOverText;              // ゲームオーバーを表示するテキスト
    public TMP_Text gameEndText;               // ゲームエンドを表示するテキスト
    public TMP_Text finalScoreText;            // 最終スコア表示用テキスト
    public TMP_Text countdownText;             // カウントダウン表示用テキスト
    public GameObject eggPrefab;                // 卵のプレハブ
    public Transform eggSpawnPoint;            // 卵のスポーン位置
    public GameObject backButton;              // タイトルに戻るボタン

    private bool isGameOverOrEnd = false;      // ゲームオーバーまたはゲームエンドのフラグ
    private bool isCountingDown = true;        // カウントダウン中かどうかのフラグ

    void Start()
    {
        UpdateUI();
        gameOverPanel.SetActive(false); // ゲームオーバーパネルを非表示
        gameOverText.gameObject.SetActive(false); // ゲームオーバーのテキストを非表示
        gameEndText.gameObject.SetActive(false); // ゲームエンドのテキストを非表示
        finalScoreText.gameObject.SetActive(false); // 最終スコアのテキストを非表示
        backButton.SetActive(false); // ボタンを非表示にする
        StartCoroutine(StartCountdown()); // カウントダウンの開始
    }

    void Update()
    {
        if (isCountingDown) return; // カウントダウン中は何も処理しない

        if (life > 0 && timeRemaining > 0) // ライフと残り時間がある場合のみカウントダウン
        {
            timeRemaining -= Time.deltaTime;
        }

        // 残り時間がゼロの場合、ゲームエンドを呼び出す
        if (timeRemaining <= 0)
        {
            GameEnd();
        }
        // ライフがゼロの場合はゲームオーバーを呼び出す
        else if (life <= 0)
        {
            GameOver();
        }

        UpdateUI();

        // ゲームオーバーまたはゲームエンド後にクリックで最終スコアを表示
        if (isGameOverOrEnd && Input.GetMouseButtonDown(0))
        {
            ShowFinalScore();
        }
    }

    private IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdownText.gameObject.SetActive(false);
        isCountingDown = false; // カウントダウンが終わったらフラグをリセット
    }

    public void AddScore()
    {
        score++;
        UpdateUI();
    }

    public void TakeDamage()
    {
        life--;
        UpdateUI(); // ライフを減らした後、UIを更新
        if (life <= 0)
        {
            GameOver();
        }
    }

    public void HealLife()
    {
        if (life < 3)
        {
            life++;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;

        // ハートマークをライフに応じて表示
        StringBuilder lifeStringBuilder = new StringBuilder();
        for (int i = 0; i < life; i++)
        {
            lifeStringBuilder.Append("\u2665"); // Unicodeを使用
        }
        lifeText.text = lifeStringBuilder.ToString(); // 最終的な文字列に変換

        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true); // ゲームオーバーパネルを表示
        gameOverText.gameObject.SetActive(true); // ゲームオーバーのメッセージを表示
        gameEndText.gameObject.SetActive(false); // ゲームエンドのメッセージを非表示
        gameOverText.text = "Game Over"; // ゲームオーバーのメッセージを設定
        Time.timeScale = 0; // ゲームを停止
        isGameOverOrEnd = true; // ゲームオーバーフラグをセット
        backButton.SetActive(true); // タイトルに戻るボタンを表示
    }

    void GameEnd()
    {
        gameOverPanel.SetActive(true); // ゲームオーバーパネルを表示
        gameEndText.gameObject.SetActive(true); // ゲームエンドのメッセージを表示
        gameOverText.gameObject.SetActive(false); // ゲームオーバーのメッセージを非表示
        gameEndText.text = "Game End"; // ゲームエンドのメッセージを設定
        Time.timeScale = 0; // ゲームを停止
        isGameOverOrEnd = true; // ゲームエンドフラグをセット
        backButton.SetActive(true); // タイトルに戻るボタンを表示
    }

    void ShowFinalScore()
    {
        gameOverText.gameObject.SetActive(false); // ゲームオーバーのメッセージを非表示
        gameEndText.gameObject.SetActive(false); // ゲームエンドのメッセージを非表示
        finalScoreText.text = "Final Score: " + score; // 最終スコアを設定
        finalScoreText.gameObject.SetActive(true); // 最終スコアのテキストを表示
        isGameOverOrEnd = false; // フラグをリセット
        Time.timeScale = 1; // ゲームを再開する場合は時間をリセット
    }

    public void BackToTitle() // タイトルに戻るメソッド
    {
        SceneManager.LoadScene("TittleScene"); // タイトルシーンの名前に変更
    }
}