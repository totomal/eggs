using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    void Update()
    {
        // 画面のどこでもクリックしたら次のシーン（ゲームシーン）に遷移
        if (Input.GetMouseButtonDown(0))  // 0は左クリック
        {
            // "GameScene"はプレイ画面のシーン名に置き換えてください
            SceneManager.LoadScene("GameScene");
        }

        // スマホやタブレットでタップを検出するための処理
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
