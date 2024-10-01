using System.Collections; // IEnumeratorを使用するために追加
using UnityEngine;
using TMPro;

public class TitleBlink : MonoBehaviour
{
    public TMP_Text startText; // 点滅させたいテキスト

    private float blinkInterval = 0.5f; // 点滅の間隔
    private bool isBlinking = true; // 点滅中かどうかのフラグ

    void Start()
    {
        // 点滅を開始するコルーチンを呼び出す
        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        while (isBlinking)
        {
            // テキストを表示
            startText.alpha = 1; // 不透明にする
            yield return new WaitForSeconds(blinkInterval);

            // テキストを非表示
            startText.alpha = 0; // 不透明度を0にする
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    // アクティブを無効にする時に点滅を止める
    void OnDisable()
    {
        isBlinking = false;
    }
}
