using UnityEngine;
using UnityEngine.UI;

public class CharaDetail : MonoBehaviour
{
    [SerializeField]
    private Button btnChara;

    private GameManager gameManager;

    private TapPointDetail tapPointDetail;

    /// <summary>
    /// �L�����̐ݒ�
    /// </summary>
    /// <param name="gameManager"></param>
    /// <param name="tapPointDetail"></param>
    public void SetUpCharaDetail(GameManager gameManager, TapPointDetail tapPointDetail)
    {

        this.gameManager = gameManager;
        this.tapPointDetail = tapPointDetail;

        btnChara.interactable = false;

        btnChara.onClick.AddListener(OnClickChara);

        btnChara.interactable = true;
    }

    /// <summary>
    /// �L�������^�b�v�����ۂ̏���
    /// </summary>
    private void OnClickChara()
    {


        ////*  ��������ǉ�  *////


        // TODO ���g�����ʂ����U���g�쐬


        ////*  �����܂�  *////


        Debug.Log("���g���̌��ʂ�\��");

        Destroy(gameObject);
    }
}