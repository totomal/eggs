using UnityEngine;

public class Egg : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に触れた時の処理
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);  // 地面に触れたら卵を削除
        }
        // プレイヤーに触れた時の処理
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);  // プレイヤーに触れたら卵を削除
        }
    }
}
