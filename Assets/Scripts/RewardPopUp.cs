using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class RewardPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnSubmit;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Image imgReward;

    [SerializeField]
    private TMP_Text txtRewardPoint;

    [SerializeField]
    private TMP_Text txtRarity;

    /// <summary>
    /// ポップアップの設定と表示
    /// </summary>
    /// <param name="rewardData"></param>
    public void SetUpRewardPopUp(RewardData rewardData)
    {
        // ポップアップを非表示にする
        canvasGroup.alpha = 0;

        // ポップアップを徐々に表示する
        canvasGroup.DOFade(1.0f, 0.5f).SetEase(Ease.Linear);

        // ボタンにメソッドの登録
        //btnSubmit.onClick.RemoveAllListeners(); // イベントの重複登録を防ぐ
        btnSubmit.onClick.AddListener(OnClickCloseRewardPopUp);

        // 褒賞のポイント表示
        txtRewardPoint.text = rewardData.rewardPoint.ToString();

        // 褒賞の希少度の表示
        txtRarity.text = rewardData.rarityType.ToString();

        // 褒賞の画僧の設定
        imgReward.sprite = rewardData.spriteReward;

        // TODO 表示の際の演出

    }

    /// <summary>
    /// ポップアップ非表示
    /// </summary>
    private void OnClickCloseRewardPopUp()
    {

        // ポップアップを徐々に非表示にする
        canvasGroup.DOFade(0.0f, 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // 非表示になったらポップアップを破壊
                Destroy(gameObject);
            });
    }
}