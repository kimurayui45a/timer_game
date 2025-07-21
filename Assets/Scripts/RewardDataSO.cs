using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 褒賞のデータベース
/// </summary>
[CreateAssetMenu(fileName = "RewardDataSO", menuName = "Create RewardDataSO")]
public class RewardDataSO : ScriptableObject
{
    public List<RewardData> rewardDatasList = new List<RewardData>();
}
