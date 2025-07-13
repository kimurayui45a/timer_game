using UnityEngine;
using UnityEngine.UI;

public class CharaDetail : MonoBehaviour
{
    [SerializeField]
    private Button btnChara;

    private GameManager gameManager;

    private TapPointDetail tapPointDetail;

    /// <summary>
    /// キャラの設定
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
    /// キャラをタップした際の処理
    /// </summary>
    private void OnClickChara()
    {


        ////*  ここから追加  *////


        // TODO お使い結果をリザルト作成


        ////*  ここまで  *////


        Debug.Log("お使いの結果を表示");

        Destroy(gameObject);
    }
}