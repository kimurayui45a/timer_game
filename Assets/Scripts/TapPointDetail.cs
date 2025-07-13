using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TapPointDetail : MonoBehaviour
{
    [SerializeField]
    private Button btnTapPoint;　　　// Button コンポーネントを制御するための変数

    [SerializeField, Header("この行き先(タップポイント)のお使い番号")]
    private int myJobNo;

    public JobData jobData; // お使いの情報を登録

    [SerializeField]
    private Image imgTapPoint;　　　// 行き先の画像を変更するためのコンポーネントの代入

    [SerializeField]
    private Sprite jobSprite;       // お使い中の画像の登録用

    [SerializeField]
    private Sprite defaultSprite;　 // 初期の行き先の画像の登録用

    private Tween tween;            // DOTween の処理を代入するための変数

    private int currentJobTime;    // お使いをしている時間の計測用

    private bool isJobs;           // お使い中かどうかを判定する値。true ならばお使い中として利用する

    /// <summary>
    /// isJobs 変数のプロパティ
    /// </summary>
    public bool IsJobs
    {
        set
        {
            isJobs = value;
        }
        get
        {
            return isJobs;
        }
    }

    private GameManager gameManager;

    /// <summary>
    /// TapPointDetail の設定
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpTapPointDetail(GameManager gameManager)
    {

        // ボタンを押した際に実行する処理(メソッド)を引数に指定して登録
        btnTapPoint.onClick.AddListener(OnClickTapPoint);

        this.gameManager = gameManager;
    }

    /// <summary>
    /// タップポイントをタップした際の処理
    /// </summary>
    private void OnClickTapPoint()
    {

        Debug.Log("TapPoint タップ");

        // タップアニメ演出  DOTween の機能の１つである DOPunchScale メソッドを利用してアニメ演出
        transform.DOPunchScale(Vector3.one * 1.25f, 0.15f).SetEase(Ease.OutBounce);

        //GameManager クラスにある行き先確認ポップアップを生成するメソッドを実行する
        gameManager.GenerateJobsConfirmPopUp(this);
    }


    /// <summary>
    /// お使いの準備
    /// </summary>
    public void PrapareteJobs()
    {
        ChangeJobSprite();
        IsJobs = true;
        StartCoroutine(WorkingJobs(jobData.jobTime));
    }

    /// <summary>
    /// お使い中の画像に変更
    /// </summary>
    public void ChangeJobSprite()
    {
        imgTapPoint.sprite = jobSprite;
        transform.localScale = new Vector3(1.75f, 1.0f, 1.0f);

        tween = transform.DOPunchPosition(new Vector3(10.0f, 0, 0), 3.0f, 10, 3)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    /// <summary>
    /// お使いの開始。時間経過処理
    /// </summary>
    /// <param name="normaJobTime"></param>
    /// <returns></returns>
    public IEnumerator WorkingJobs(int normaJobTime)
    {

        // 残っているお使いの時間を設定
        currentJobTime = normaJobTime;

        // お使いが終わるかを監視
        while (IsJobs)
        {
            // TODO 条件として時間を確認する
            currentJobTime--;

            // 残り時間が 0 以下になったら
            if (currentJobTime <= 0)
            {
                KillTween();
                IsJobs = false;
            }

            yield return null;
        }

        // お使いに関する情報を初期状態に戻す
        ReturnDefaultState();

        // 仕事終了
        Debug.Log("お使い 終了");

        // GameManager クラスにあるキャラを生成するメソッドを実行する
        gameManager.GenerateCharaDetail(this);

    }

    /// <summary>
    /// Tween を破棄
    /// </summary>
    public void KillTween()
    {
        tween.Kill();
    }

    /// <summary>
    /// お使いに関する情報を初期状態に戻す
    /// </summary>
    public void ReturnDefaultState()
    {
        // お使い中の画像を元の画像に戻す
        imgTapPoint.sprite = defaultSprite;

        // お使いの時間をリセット
        currentJobTime = 0;

        // オブジェクトのサイズを初期値に戻す
        transform.localScale = Vector3.one;
    }

}