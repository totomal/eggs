using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public GameObject[] eggPrefabs;      // 5つの卵のPrefabをここに追加
    public GameObject enemyPrefab;        // 敵のPrefabをここに追加
    public GameObject healthItemPrefab;   // 回復アイテムのPrefabをここに追加
    public Transform spawnArea;           // スポーンエリアを指定するTransform
    public float spawnInterval = 2f;      // 最初の卵のスポーン間隔
    public float fallSpeed = 0.06f;
    private float timer;
    private float gameTime = 60f;
    private float elapsedTime = 0f;       // 経過時間を追跡する変数
    public float spawnHeightOffset = 2f;  // スポーン位置を上にずらすオフセット
    private int enemySpawnChance = 3;     // 初期の敵の出現確率（randomIndexの範囲で調整）

    void Update()
    {
        // ゲーム時間を減少させる
        gameTime -= Time.deltaTime;
        elapsedTime += Time.deltaTime; // 経過時間を増加

        // 10秒ごとに落下速度を増加
        if (elapsedTime >= 10f)
        {
            fallSpeed += 0.03f; // 落下速度を増加
            enemySpawnChance += 1; // 10秒ごとに敵の出現確率を増加
            elapsedTime = 0f; // 経過時間をリセット
        }

        // スポーン間隔を調整
        if (gameTime <= 0)
        {
            gameTime = 0; // ゲーム時間を0にクリップ
        }
        else if (Mathf.FloorToInt(gameTime) % 10 == 0 && spawnInterval > 0.5f)
        {
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.1f); // スポーン間隔を短縮
        }

        // スポーンのタイミング
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();  // 卵、敵、回復アイテムをスポーン
            timer = 0; // タイマーをリセット
        }
    }

    void SpawnObject()
    {
        GameObject prefabToSpawn;

        // 経過時間が10秒未満の場合は卵だけをスポーン
        if (gameTime >= 50f) // 最初の10秒
        {
            int eggIndex = Random.Range(0, eggPrefabs.Length);
            prefabToSpawn = eggPrefabs[eggIndex];
        }
        else
        {
            // 10秒以上経過したら卵、敵、回復アイテムをランダムにスポーン
            int randomIndex = Random.Range(0, 20 + enemySpawnChance);  // 時間経過で敵の出現確率を上げる

            if (randomIndex < 16)
            {
                // 卵をスポーン
                int eggIndex = Random.Range(0, eggPrefabs.Length);
                prefabToSpawn = eggPrefabs[eggIndex];
            }
            else if (randomIndex >= 16 && randomIndex < 16 + enemySpawnChance)
            {
                // 敵をスポーン
                prefabToSpawn = enemyPrefab;
            }
            else
            {
                // 回復アイテムをスポーン（30秒経過している場合のみ）
                if (elapsedTime >= 30f)
                {
                    prefabToSpawn = healthItemPrefab;
                }
                else
                {
                    prefabToSpawn = eggPrefabs[Random.Range(0, eggPrefabs.Length)]; // 回復アイテムが出現しない場合は卵を出現
                }
            }
        }

        // より広い範囲を設定する
        float spawnWidth = spawnArea.localScale.x;  // 現在の幅を取得

        // スポーン位置を上にずらす
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnWidth / 2, spawnWidth / 2), 
            spawnArea.position.y + spawnHeightOffset,  // 上にオフセットを追加
            0
        );

        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallSpeed);  // 落下速度を設定
        Debug.Log("Spawned object with fall speed: " + fallSpeed);
    }
}
