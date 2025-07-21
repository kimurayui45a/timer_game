using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class AlbumPopUp : MonoBehaviour
{
    private Vector3 closePos;

    [SerializeField]
    private Button btnClose;

    [SerializeField]
    private Image imgReward;

    [SerializeField]
    private RewardDetail rewardDetailPrefab;

    [SerializeField]
    private Transform rewardDetailTran;

    [SerializeField]
    private List<RewardDetail> rewardDetailsList = new List<RewardDetail>();

    /// <summary>
    /// AlbumPopUp の設定と表示
    /// </summary>
    public void SetUpAlbumPopUp(GameManager gameManager, Vector3 centerPos, Vector3 btnPos)
    {  //　<=　この第2引数と第3引数があることで、ポップアップの移動処理が実装できる

        // ポップアップの位置をボタンの位置に設定
        transform.position = btnPos;

        // 最後にポップアップを閉じる際に利用するために、現在の位置(ボタンの位置)を保持
        closePos = transform.position;

        // ボタンを押した際に処理したいメソッドを登録
        btnClose.onClick.AddListener(OnClickCloseAlbum);

        // ゲームオブジェクトのサイズを 0 にして見えない状態にする
        transform.localScale = Vector3.zero;

        // Sequence を初期化して利用できる状態にする
        Sequence sequence = DOTween.Sequence();

        // ポップアップをボタンの位置から画面の中央(Canvas ゲームオブジェクトの位置)に移動させつつ
        sequence.Append(transform.DOLocalMove(centerPos, 0.3f).SetEase(Ease.Linear));

        // ポップアップを徐々に大きくしながら表示。指定したサイズになったら、元のポップアップの大きさに戻す
        sequence.Join(transform.DOScale(Vector2.one * 1.2f, 0.5f).SetEase(Ease.InBack)).OnComplete(() => { transform.DOScale(Vector2.one, 0.2f); });


        // 獲得している褒賞の数だけサムネイル用のゲームオブジェクトの生成処理を繰り返す
        for (int i = 0; i < GameData.instance.GetEarnedRewardsListCount(); i++)
        {

            // 獲得している褒賞用のゲームオブジェクト(サムネイル用)を生成
            RewardDetail rewardDetail = Instantiate(rewardDetailPrefab, rewardDetailTran, false);

            // サムネイル用のゲームオブジェクトに利用する褒賞のデータを取得して設定（RewardNo から RewardData を取得して RewardDetail を設定）
            rewardDetail.SetUpRewardDetail(gameManager.GetRewardDataFromRewardNo(i), this);

            // 初期画像の設定（最初の RewardDetail を初期画像に設定）
            if (rewardDetailsList.Count == 0)
            {
                imgReward.sprite = gameManager.GetRewardDataFromRewardNo(i).spriteReward;
            }

            // 褒賞一覧の List に登録
            rewardDetailsList.Add(rewardDetail);
        }

    }

    /// <summary>
    /// アルバムポップアップを閉じる
    /// </summary>
    private void OnClickCloseAlbum()
    {

        // Sequence を初期化して利用できる状態にする
        Sequence sequence = DOTween.Sequence();

        // ポップアップの大きさを徐々に 0 にして見えない状態にさせつつ
        sequence.Append(transform.DOScale(Vector2.zero, 0.3f).SetEase(Ease.Linear));

        // それに合わせてポップアップをアルバムボタンの位置に移動させる。移動後にポップアップを破棄
        // DOLocalMove メソッドにするとボタンの位置に戻らないため、DOMove メソッドを使う
        sequence.Join(transform.DOMove(closePos, 0.3f).SetEase(Ease.Linear)).OnComplete(() => { Destroy(gameObject); });
    }

    /// <summary>
    /// アルバム一覧で選択された褒賞の画像をポップアップに表示
    /// </summary>
    public void DisplayReward(Sprite spriteReward)
    {
        imgReward.sprite = spriteReward;
    }

}
