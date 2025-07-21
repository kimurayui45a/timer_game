using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 褒賞一覧の1つひとつ（アイコン）の表示やボタン押下時の動作を制御するクラス
/// </summary>
public class RewardDetail : MonoBehaviour
{
    // 褒賞の画像を表示するUIコンポーネント
    [SerializeField]
    private Image imgReward;

    // このオブジェクトに紐づく褒賞データ
    [SerializeField]
    private RewardData rewardData;

    // 褒賞アイコンをタップしたときのボタン
    [SerializeField]
    private Button btnRewardDetail;

    // 表示に使う AlbumPopUp を保持（ポップアップUIとのやりとり用）
    private AlbumPopUp albumPopUp;

    /// <summary>
    /// 褒賞アイコンの見た目や動作を設定する
    /// </summary>
    /// <param name="rewardData">表示する褒賞のデータ</param>
    /// <param name="albumPopUp">画像表示用の親ポップアップ</param>
    public void SetUpRewardDetail(RewardData rewardData, AlbumPopUp albumPopUp)
    {
        // 褒賞データと親ポップアップを記録
        this.rewardData = rewardData;
        this.albumPopUp = albumPopUp;

        // 褒賞アイコンの画像を設定
        imgReward.sprite = this.rewardData.spriteReward;

        // ボタン押下時のイベントを登録（クリックで詳細表示）
        btnRewardDetail.onClick.AddListener(OnClickRewardDetail);
    }

    /// <summary>
    /// 褒賞アイコンをクリックしたときの処理（大きく表示）
    /// </summary>
    public void OnClickRewardDetail()
    {
        // ポップアップに画像を渡して表示
        albumPopUp.DisplayReward(rewardData.spriteReward);
    }
}