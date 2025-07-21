using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class OfflineTimeManager : MonoBehaviour
{
    // シングルトン用の変数
    public static OfflineTimeManager instance;

    // 前回ゲームを止めた時にセーブしている時間
    private DateTime loadDateTime = new DateTime();

    // 経過時間
    private int elaspedTime;

    // 時間をセーブ・ロードする際の変数。定数として宣言する
    private const string SAVE_KEY_DATETIME = "OfflineDateTime";

    // 日時のフォーマット指定用
    private const string FORMAT = "yyyy/MM/dd HH:mm:ss";

    //  お使いの時間のデータをセーブ・ロードするための変数
    private const string WORKING_JOB_SAVE_KEY = "workingJobNo_";

    private GameManager gameManager;

    /// <summary>
    /// お使い用の時間データを管理するためのクラス
    /// </summary>
    [Serializable]
    public class JobTimeData
    {
        // お使いの通し番号
        public int jobNo;

        // お使いの残り時間
        public int elaspedJobTime;

        // DateTime クラスを文字列にするための変数
        public string jobTimeString;

        /// <summary>
        /// DateTime を文字列型で保存しているので、DateTime 型に戻して取得
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTime()
        {
            return DateTime.ParseExact(jobTimeString, FORMAT, null);
        }
    }

    [Header("お使いの時間データのリスト")]
    public List<JobTimeData> workingJobTimeDatasList = new List<JobTimeData>();


    /// <summary>
    /// ゲームオブジェクトが生成された瞬間に最初に一度だけ実行される処理
    /// オフライン時間を扱う常駐マネージャーの初期化
    /// 前回終了時刻を読み込み、現在時刻との差を計算する
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 時間のセーブデータの確認とロード
        LoadOfflineDateTime();

        // オフラインでの経過時間を計算
        CalculateOfflineDateTimeElasped(loadDateTime);

        // TODO お使いのデータのロード

    }

    /// <summary>
    /// ゲームが終了したときに自動的に呼ばれる
    /// </summary>
    private void OnApplicationQuit()
    {

        // 現在の時間のセーブ
        SaveOfflineDateTime();

        Debug.Log("ゲーム中断。時間のセーブ完了");


        // お使い中のデータが１つ以上ある場合
        for (int i = 0; i < workingJobTimeDatasList.Count; i++)
        {

            // お使いの時間データを１つずつ順番にすべてセーブ
            SaveWorkingJobTimeData(workingJobTimeDatasList[i].jobNo);
        }
    }

    /// <summary>
    /// オフラインでの時間をロード
    /// </summary>
    public void LoadOfflineDateTime()
    {

        // セーブデータがあるか確認
        if (PlayerPrefsHelper.ExistsData(SAVE_KEY_DATETIME))
        {

            // セーブデータがある場合、ロードする
            string oldDateTimeString = PlayerPrefsHelper.LoadStringData(SAVE_KEY_DATETIME);

            // ロードした文字列を DateTime 型に型変換して時間を取得
            loadDateTime = DateTime.ParseExact(oldDateTimeString, FORMAT, null);

            Debug.Log("ゲーム開始時 : セーブされていた時間 : " + oldDateTimeString);

            Debug.Log("今の時間 : " + DateTime.Now.ToString(FORMAT));

        }
        else
        {
            // セーブデータがない場合、現在の時間を開始時刻として取得しておく
            loadDateTime = DateTime.Now;

            Debug.Log("セーブデータがないので今の時間を取得 : " + loadDateTime.ToString(FORMAT));
        }
    }

    /// <summary>
    /// 現在の時間をセーブ
    /// </summary>
    public void SaveOfflineDateTime()
    {

        // 現在の時間を取得して、文字列に変換
        string dateTimeString = DateTime.Now.ToString(FORMAT);

        // string 型でセーブ
        PlayerPrefsHelper.SaveStringData(SAVE_KEY_DATETIME, dateTimeString);

        Debug.Log("ゲーム終了時 : セーブ時間 : " + dateTimeString);
    }

    /// <summary>
    /// オフラインでの経過時間を計算
    /// </summary>
    public int CalculateOfflineDateTimeElasped(DateTime oldDateTime)
    {

        // 現在の時間を取得
        DateTime currentDateTime = DateTime.Now;

        // 現在の時間とセーブされている時間を確認
        if (oldDateTime > currentDateTime)
        {

            // セーブデータの時間の方が今の時間よりも進んでいる場合には、今の時間を入れなおす
            oldDateTime = DateTime.Now;
        }

        // 経過した時間の差分
        TimeSpan dateTimeElasped = currentDateTime - oldDateTime;

        // 経過時間を秒にする(Math.Round メソッドを利用して、double 型を int 型に変換。小数点は 0 の位で、数値の丸めの処理の指定は ToEven(数値が 2 つの数値の中間に位置するときに、最も近い偶数の値) を指定) 
        elaspedTime = (int)Math.Round(dateTimeElasped.TotalSeconds, 0, MidpointRounding.ToEven);

        Debug.Log($"オフラインでの経過時間 : {elaspedTime} 秒");

        return elaspedTime;
    }

    /// <summary>
    /// 各お使いの残り時間の更新
    /// </summary>
    /// <param name="jobNo"></param>
    /// <param name="currentJobTime"></param>
    public void UpdateCurrentJobTime(int jobNo, int currentJobTime)
    {

        // List から該当の JobTimeData を検索して取得し、elaspedJobTime の値を currentJobTime に更新
        workingJobTimeDatasList.Find(x => x.jobNo == jobNo).elaspedJobTime = currentJobTime;
    }

    /// <summary>
    /// List に JobTimeData を追加。このリストにある情報が現在お使いをしている内容になる
    /// </summary>
    /// <param name="jobTimeData"></param>
    public void AddWorkingJobTimeDatasList(JobTimeData jobTimeData)
    {

        // お使いを List に追加する前に、すでにリストにあるか確認して重複登録を防ぐ
        if (!workingJobTimeDatasList.Exists(x => x.jobNo == jobTimeData.jobNo))
        {

            // List にない場合のみ、新しく追加する
            workingJobTimeDatasList.Add(jobTimeData);

            Debug.Log(jobTimeData.elaspedJobTime);
        }
    }

    /// <summary>
    /// 現在お使い中の JobTimeData の作成と List への追加
    /// </summary>
    /// <param name="tapPointDetail"></param>
    public void CreateWorkingJobTimeDatasList(TapPointDetail tapPointDetail, int remainingTime)
    {

        // お使いの残り時間を設定。-1 の場合はお使い開始時なので、お使い時間をそのままセット
        // それ以外はお使いの途中なので、残り時間をセット(そのまま remainingTime を使う)
        if (remainingTime == -1)
        {
            remainingTime = tapPointDetail.jobData.jobTime;
        }

        // JobTimeData をインスタンスして初期化
        JobTimeData jobTimeData = new JobTimeData { jobNo = tapPointDetail.jobData.jobNo, elaspedJobTime = remainingTime };

        // List に JobTimeData を追加
        AddWorkingJobTimeDatasList(jobTimeData);
    }


    /// <summary>
    /// お使いの時間のセーブ
    　　/// お使い開始時とゲーム終了時にセーブ
    /// </summary>
    /// <param name="jobNo"></param>
    public void SaveWorkingJobTimeData(int jobNo)
    {

        // セーブ対象の JobTimeData を List から検索して取得
        JobTimeData jobTimeData = workingJobTimeDatasList.Find(x => x.jobNo == jobNo);

        // 今の時間を取得して文字列に変換
        jobTimeData.jobTimeString = DateTime.Now.ToString(FORMAT);

        // お使いの時間データのセーブ
        PlayerPrefsHelper.SaveSetObjectData(WORKING_JOB_SAVE_KEY + jobTimeData.jobNo.ToString(), jobTimeData);

        string str = DateTime.Now.ToString(FORMAT);
        Debug.Log("仕事中 : セーブ時間 : " + str);
        Debug.Log("セーブ時のお使いの残り時間 : " + jobTimeData.elaspedJobTime);
    }

    /// <summary>
    /// 行き先の数だけ、その行き先の JobTimeData があるかどうか確認し、ある場合にはロードして WorkingJobTimeDatasList に追加
    /// </summary>
    public void GetWorkingJobTimeDatasList(List<TapPointDetail> tapPointDetailsList)
    {
        for (int i = 0; i < tapPointDetailsList.Count; i++)
        {
            // 該当するお使いの番号でセーブされている時間データがあるかどうか確認
            LoadOfflineJobTimeData(tapPointDetailsList[i].jobData.jobNo);
        }
    }

    /// <summary>
    /// お使い時間のロード
    /// </summary>
    /// <param name="jobNo"></param>
    public void LoadOfflineJobTimeData(int jobNo)
    {

        // 指定されたお使いの時間データのセーブデータがあるか確認
        if (PlayerPrefsHelper.ExistsData(WORKING_JOB_SAVE_KEY + jobNo.ToString()))
        {

            // セーブデータがある場合、取得してクラスに復元
            JobTimeData jobTimeData = PlayerPrefsHelper.LoadGetObjectData<JobTimeData>(WORKING_JOB_SAVE_KEY + jobNo.ToString());

            //  List に JobTimeData を追加
            AddWorkingJobTimeDatasList(jobTimeData);

            // 文字列になっている時間を DateTime 構造体に復元して取得
            DateTime time = jobTimeData.GetDateTime();

            string str = time.ToString(FORMAT);
            Debug.Log("仕事開始時 : セーブされていた時間 : " + str);
            Debug.Log("ロード時の残り時間 : " + jobTimeData.elaspedJobTime);
        }
    }

    /// <summary>
    /// お使いの終了した JobTimeData を削除し、セーブデータを削除
    /// </summary>
    public void RemoveWorkingJobTimeDatasList(int removeJobNo)
    {
        // 対象のお使いを照合して、リストから削除
        workingJobTimeDatasList.Remove(workingJobTimeDatasList.Find(x => x.jobNo == removeJobNo));

        // 対象のお使いのセーブデータを削除
        PlayerPrefsHelper.RemoveObjectData(WORKING_JOB_SAVE_KEY + removeJobNo);
    }

    /// <summary>
    /// デバッグ用
    /// すべてのお使いの JobTimeData を削除
    /// </summary>
    public void AllRemoveWorkingJobTimeDatasList()
    {
        // リストからすべて削除
        workingJobTimeDatasList.Clear();

        // すべてのセーブデータを削除
        PlayerPrefsHelper.AllClearSaveData();
        DebugManager.instance.DisplayDebugDialog("すべてのセーブデータを削除 実行");
    }


}