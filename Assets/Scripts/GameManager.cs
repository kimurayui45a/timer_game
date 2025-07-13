using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<TapPointDetail> tapPointDetailsList = new List<TapPointDetail>();     //  �Q�[�����ɂ���s��𑩂˂ĊǗ����邽�߂̕ϐ�

    [SerializeField]
    private Transform canvasTran;�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@//  �s����m�F�p�|�b�v�A�b�v�Ȃǂ̐����ʒu�̎w��̕ϐ��BTapPointDetail �N���X���ڊ�

    [SerializeField]
    private JobsConfirmPopUp jobsConfirmPopUpPrefab;                                   //  �s����m�F�|�b�v�A�b�v�̃v���t�@�u���A�T�C������ϐ��BTapPointDetail �N���X���ڊ�

    [SerializeField]
    private CharaDetail charaDetailsPrefab;                                            //  ���g���������ɐ�������L�����̃v���t�@�u���A�T�C������ϐ��BTapPointDetail �N���X���ڊ�

    [SerializeField]
    private List<CharaDetail> charaDetailsList = new List<CharaDetail>();              //  ���g�������̃L�����𑩂˂ĊǗ����邽�߂̕ϐ�


    void Start()
    {

        // TODO ���g���̎��ԃf�[�^�̊m�F�ƃ��[�h


        // �e TapPointDetail �̐ݒ�
        TapPointSetUp();
    }

    /// <summary>
    /// �e TapPointDetail �̐ݒ�B���g���̏󋵂ɍ��킹�āA�d�������d���I�����𔻒f���ăL�����𐶐����邩�A���g�����ĊJ���邩����
    /// </summary>
    private void TapPointSetUp()
    {

        // List �ɓo�^����Ă��邷�ׂĂ� TapPointDetail �N���X�ɑ΂��ĂP�񂸂������s��
        for (int i = 0; i < tapPointDetailsList.Count; i++)
        {

            // TapPointDetail �N���X�̐ݒ���s�� 
            tapPointDetailsList[i].SetUpTapPointDetail(this);

            // TODO �e TapPointDetail �ɓo�^����Ă��� JobData �ɊY������ JobTimeData ���擾


            // TODO ���̍s���悪���g�����łȂ���Ύ��̏����ֈڂ�


            // TODO ���g���̏�ԂƎc�莞�Ԃ��擾


            // TODO if ���[�h�������ԂƃZ�[�u�������Ԃ��v�Z���āA�܂����g���̎��Ԃ��o�߂��Ă��Ȃ��ꍇ�ɂ́A�B�����s����
            //         ���g�����������Ă���ꍇ�ɂ͇@�ƇA�����s����


            // TODO �@���g���̃��X�g�ƃZ�[�u�f�[�^���폜


            // TODO �A�L�����������Č��ʂ��m�F


            // TODO �B�s����̉摜�����g�����̉摜�ɕύX���āA���g���������ĊJ�B

        }
    }

    /// <summary>
    /// TapPoint ���N���b�N�����ۂɂ��g���m�F�p�̃|�b�v�A�b�v���J���BTapPointDetail �N���X���ڊ�
    /// </summary>
    /// <param name="tapPointDetail"></param>
    public void GenerateJobsConfirmPopUp(TapPointDetail tapPointDetail)
    {

        // ���g���m�F�p�̃|�b�v�A�b�v���쐬
        JobsConfirmPopUp jobsConfirmPopUp = Instantiate(jobsConfirmPopUpPrefab, canvasTran, false);

        // �|�b�v�A�b�v�̃��\�b�h�����s����B���\�b�h�̈����𗘗p���ă|�b�v�A�b�v�� TapPointDetail �N���X�̏��𑗂邱�ƂŁAJobData �̏������p�ł���悤�ɂ���
        jobsConfirmPopUp.OpenPopUp(tapPointDetail, this);
    }

    /// <summary>
    /// ���g���������󂯂����m�F
    /// </summary>
    /// <param name="isSubmit"></param>
    public void JudgeSubmitJob(bool isSubmit, TapPointDetail tapPointDetail, int remainingTime = -1)
    {
        if (isSubmit)
        {

            // TODO ���g���̓o�^


            // ���g���̏����Ƃ��g���J�n
            tapPointDetail.PrapareteJobs();


            // TODO ���g���J�n���Ԃ̃Z�[�u


        }
        else
        {
            Debug.Log("���g���ɂ͍s���Ȃ�");
        }
    }

    /// <summary>
    /// �L��������
    /// </summary>
    public void GenerateCharaDetail(TapPointDetail tapPointDetail)
    {

        Debug.Log("���g���p�̃L�����̐���");

        CharaDetail chara = Instantiate(charaDetailsPrefab, tapPointDetail.transform, false);

        // �L�����̐ݒ�
        chara.SetUpCharaDetail(this, tapPointDetail);
    }
}