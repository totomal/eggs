using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // プレイヤーの移動速度
    public GameController gameController;

    void Update()
    {
        // PC移動 (←→キー)
        float move = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(move, 0, 0);

        // PCでの左右反転処理
        if (move < 0) // 左に移動
        {
            if (transform.localScale.x > 0) // 右向きの時だけ反転
            {
                FlipPlayer(); // 左向きに反転
            }
        }
        else if (move > 0) // 右に移動
        {
            if (transform.localScale.x < 0) // 左向きの時だけ反転
            {
                FlipPlayer(); // 右向きに戻す
            }
        }

        // スマホのタッチ移動
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 画面の左半分をタッチした場合は左に移動
            if (touch.position.x < Screen.width / 2)
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0); // 左に移動
                if (transform.localScale.x > 0) // 右向きの時だけ反転
                {
                    FlipPlayer(); // 左向きに反転
                }
            }
            // 画面の右半分をタッチした場合は右に移動
            else
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0); // 右に移動
                if (transform.localScale.x < 0) // 左向きの時だけ反転
                {
                    FlipPlayer(); // 右向きに戻す
                }
            }
        }
    }

    void FlipPlayer()
    {
        // 現在のスケールを反転させる
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // キャッチ処理
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Egg"))
        {
            gameController.AddScore();  // スコアを増加
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            gameController.TakeDamage();  // ダメージを受ける
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("HealthItem"))
        {
            gameController.HealLife();  // LIFEを回復
            Destroy(other.gameObject);
        }
    }
}
