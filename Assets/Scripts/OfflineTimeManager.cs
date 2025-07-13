using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OfflineTimeManager : MonoBehaviour
{
    public static OfflineTimeManager instance;        // �V���O���g���p�̕ϐ�

    private DateTime loadDateTime = new DateTime();�@�@�@�@�@ // �O��Q�[�����~�߂����ɃZ�[�u���Ă��鎞��

    private int elaspedTime;                          �@�@�@  // �o�ߎ���

    private const string SAVE_KEY_DATETIME = "OfflineDateTime";�@�@�@// ���Ԃ��Z�[�u�E���[�h����ۂ̕ϐ��B�萔�Ƃ��Đ錾����

    private const string FORMAT = "yyyy/MM/dd HH:mm:ss";�@�@ �@�@�@�@// �����̃t�H�[�}�b�g�w��p


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

        // ���Ԃ̃Z�[�u�f�[�^�̊m�F�ƃ��[�h
        LoadOfflineDateTime();

        // �I�t���C���ł̌o�ߎ��Ԃ��v�Z
        CalculateOfflineDateTimeElasped(loadDateTime);

        // TODO ���g���̃f�[�^�̃��[�h

    }

    /// <summary>
    /// �Q�[�����I�������Ƃ��Ɏ����I�ɌĂ΂��
    /// </summary>
    private void OnApplicationQuit()
    {

        // ���݂̎��Ԃ̃Z�[�u
        SaveOfflineDateTime();

        Debug.Log("�Q�[�����f�B���Ԃ̃Z�[�u����");
    }

    /// <summary>
    /// �I�t���C���ł̎��Ԃ����[�h
    /// </summary>
    public void LoadOfflineDateTime()
    {

        // �Z�[�u�f�[�^�����邩�m�F
        if (PlayerPrefsHelper.ExistsData(SAVE_KEY_DATETIME))
        {

            // �Z�[�u�f�[�^������ꍇ�A���[�h����
            string oldDateTimeString = PlayerPrefsHelper.LoadStringData(SAVE_KEY_DATETIME);

            // ���[�h����������� DateTime �^�Ɍ^�ϊ����Ď��Ԃ��擾
            loadDateTime = DateTime.ParseExact(oldDateTimeString, FORMAT, null);

            Debug.Log("�Q�[���J�n�� : �Z�[�u����Ă������� : " + oldDateTimeString);

            Debug.Log("���̎��� : " + DateTime.Now.ToString(FORMAT));

        }
        else
        {
            // �Z�[�u�f�[�^���Ȃ��ꍇ�A���݂̎��Ԃ��J�n�����Ƃ��Ď擾���Ă���
            loadDateTime = DateTime.Now;

            Debug.Log("�Z�[�u�f�[�^���Ȃ��̂ō��̎��Ԃ��擾 : " + loadDateTime.ToString(FORMAT));
        }
    }

    /// <summary>
    /// ���݂̎��Ԃ��Z�[�u
    /// </summary>
    public void SaveOfflineDateTime()
    {

        // ���݂̎��Ԃ��擾���āA������ɕϊ�
        string dateTimeString = DateTime.Now.ToString(FORMAT);

        // string �^�ŃZ�[�u
        PlayerPrefsHelper.SaveStringData(SAVE_KEY_DATETIME, dateTimeString);

        Debug.Log("�Q�[���I���� : �Z�[�u���� : " + dateTimeString);
    }

    /// <summary>
    /// �I�t���C���ł̌o�ߎ��Ԃ��v�Z
    /// </summary>
    public int CalculateOfflineDateTimeElasped(DateTime oldDateTime)
    {

        // ���݂̎��Ԃ��擾
        DateTime currentDateTime = DateTime.Now;

        // ���݂̎��ԂƃZ�[�u����Ă��鎞�Ԃ��m�F
        if (oldDateTime > currentDateTime)
        {

            // �Z�[�u�f�[�^�̎��Ԃ̕������̎��Ԃ����i��ł���ꍇ�ɂ́A���̎��Ԃ����Ȃ���
            oldDateTime = DateTime.Now;
        }

        // �o�߂������Ԃ̍���
        TimeSpan dateTimeElasped = currentDateTime - oldDateTime;

        // �o�ߎ��Ԃ�b�ɂ���(Math.Round ���\�b�h�𗘗p���āAdouble �^�� int �^�ɕϊ��B�����_�� 0 �̈ʂŁA���l�̊ۂ߂̏����̎w��� ToEven(���l�� 2 �̐��l�̒��ԂɈʒu����Ƃ��ɁA�ł��߂������̒l) ���w��) 
        elaspedTime = (int)Math.Round(dateTimeElasped.TotalSeconds, 0, MidpointRounding.ToEven);

        Debug.Log($"�I�t���C���ł̌o�ߎ��� : {elaspedTime} �b");

        return elaspedTime;
    }
}