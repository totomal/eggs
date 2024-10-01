using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に触れた時の処理
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);  // 地面に触れたら敵を削除
        }
    }
}
