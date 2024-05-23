using DynamicMeshCutter;
using UnityEngine;
using Valve.VR;

public class SwingWeapon : MonoBehaviour
{
    [SerializeField] private SteamVR_Behaviour_Pose LHandPose; // 左手の SteamVR ポーズ
    [SerializeField] private SteamVR_Behaviour_Pose RHandPose; // 右手の SteamVR ポーズ
    private SteamVR_Behaviour_Pose pose; // 現在の手の SteamVR ポーズ
    private Vector3 lastPosition; // 前回フレームの位置
    private Vector3 startPosition; // 振り始めの位置
    private Vector3 currentPosition; // 現在の位置
    private Vector3 swingDirection; // 振りの方向

    private bool isCutting = false; // 切断中かどうかのフラグ

    void Update()
    {
        // 武器を持っている手のポーズを設定する
        if (this.GetComponent<HoldObject>().IsLeftHandHolding())
        {
            pose = LHandPose;
        }
        else
        {
            pose = RHandPose;
        }
        currentPosition = pose.transform.position;
    }

    public Vector3 GetSwingDirection()
    {
        return swingDirection; // 振りの方向を返す
    }

    private void OnTriggerEnter(Collider other)
    {
        // 衝突した対象がターゲットでない場合は処理しない
        if (other.tag != "Target") return;

        startPosition = currentPosition; // 振り始めの位置を設定
    }

    private void OnTriggerExit(Collider other)
    {
        // 衝突した対象がターゲットでない場合、または既に切断中の場合は処理しない
        if (other.tag != "Target" || isCutting) return;

        lastPosition = currentPosition; // 前回のフレームの位置を設定
        Vector3 direction = startPosition - lastPosition; // 振りの方向を計算
        direction.z = 0; // Z方向の成分を無視する
        //direction.x *= -1; // X方向の成分を反転する（左右の振りを考慮する）
        direction.Normalize(); // 方向ベクトルを正規化する
        swingDirection = direction; // 振りの方向を設定

        // 切断効果の音を再生する
        this.GetComponent<AudioSource>().Play();

        other.GetComponent<AudioSource>().Play();

        // ターゲットに力を加えて飛ばす
        Vector3 force = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        other.GetComponent<Rigidbody>().AddForce(force);

        // PlaneBehaviour コンポーネントを使用して切断を実行する
        this.GetComponent<PlaneBehaviour>().Cut();

        // ターゲットの Collider をトリガーではなくする（物理挙動を有効にする）
        other.GetComponent<Collider>().isTrigger = false;

        isCutting = true; // 切断中フラグを設定
    }

    public bool IsCutting
    {
        get { return isCutting; }
    }

    // 切断をリセットするメソッド
    public void ResetCut()
    {
        swingDirection = Vector3.zero; // 振りの方向をリセット
        isCutting = false; // 切断中フラグをリセット
    }
}
