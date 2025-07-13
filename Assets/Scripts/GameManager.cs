using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<TapPointDetail> tapPointDetailsList = new List<TapPointDetail>();     //  ゲーム内にある行先を束ねて管理するための変数

    [SerializeField]
    private Transform canvasTran;　　　　　　　　　　　　　　　　　　　　　　　　　　　//  行き先確認用ポップアップなどの生成位置の指定の変数。TapPointDetail クラスより移管

    [SerializeField]
    private JobsConfirmPopUp jobsConfirmPopUpPrefab;                                   //  行き先確認ポップアップのプレファブをアサインする変数。TapPointDetail クラスより移管

    [SerializeField]
    private CharaDetail charaDetailsPrefab;                                            //  お使い完了時に生成するキャラのプレファブをアサインする変数。TapPointDetail クラスより移管

    [SerializeField]
    private List<CharaDetail> charaDetailsList = new List<CharaDetail>();              //  お使い完了のキャラを束ねて管理するための変数


    void Start()
    {

        // TODO お使いの時間データの確認とロード


        // 各 TapPointDetail の設定
        TapPointSetUp();
    }

    /// <summary>
    /// 各 TapPointDetail の設定。お使いの状況に合わせて、仕事中か仕事終了かを判断してキャラを生成するか、お使いを再開するか決定
    /// </summary>
    private void TapPointSetUp()
    {

        // List に登録されているすべての TapPointDetail クラスに対して１回ずつ処理を行う
        for (int i = 0; i < tapPointDetailsList.Count; i++)
        {

            // TapPointDetail クラスの設定を行う 
            tapPointDetailsList[i].SetUpTapPointDetail(this);

            // TODO 各 TapPointDetail に登録されている JobData に該当する JobTimeData を取得


            // TODO この行き先がお使い中でなければ次の処理へ移る


            // TODO お使いの状態と残り時間を取得


            // TODO if ロードした時間とセーブした時間を計算して、まだお使いの時間が経過していない場合には、③を実行する
            //         お使いが完了している場合には①と②を実行する


            // TODO ①お使いのリストとセーブデータを削除


            // TODO ②キャラ生成して結果を確認


            // TODO ③行き先の画像をお使い中の画像に変更して、お使い処理を再開。

        }
    }

    /// <summary>
    /// TapPoint をクリックした際にお使い確認用のポップアップを開く。TapPointDetail クラスより移管
    /// </summary>
    /// <param name="tapPointDetail"></param>
    public void GenerateJobsConfirmPopUp(TapPointDetail tapPointDetail)
    {

        // お使い確認用のポップアップを作成
        JobsConfirmPopUp jobsConfirmPopUp = Instantiate(jobsConfirmPopUpPrefab, canvasTran, false);

        // ポップアップのメソッドを実行する。メソッドの引数を利用してポップアップに TapPointDetail クラスの情報を送ることで、JobData の情報を活用できるようにする
        jobsConfirmPopUp.OpenPopUp(tapPointDetail, this);
    }

    /// <summary>
    /// お使いを引き受けたか確認
    /// </summary>
    /// <param name="isSubmit"></param>
    public void JudgeSubmitJob(bool isSubmit, TapPointDetail tapPointDetail, int remainingTime = -1)
    {
        if (isSubmit)
        {

            // TODO お使いの登録


            // お使いの準備とお使い開始
            tapPointDetail.PrapareteJobs();


            // TODO お使い開始時間のセーブ


        }
        else
        {
            Debug.Log("お使いには行かない");
        }
    }

    /// <summary>
    /// キャラ生成
    /// </summary>
    public void GenerateCharaDetail(TapPointDetail tapPointDetail)
    {

        Debug.Log("お使い用のキャラの生成");

        CharaDetail chara = Instantiate(charaDetailsPrefab, tapPointDetail.transform, false);

        // キャラの設定
        chara.SetUpCharaDetail(this, tapPointDetail);
    }
}