using UnityEngine;

public class HealthItem : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に触れた時の処理
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);  // 地面に触れたら回復アイテムを削除
        }
    }
}
