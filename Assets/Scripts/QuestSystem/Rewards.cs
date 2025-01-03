using System;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    [SerializeField] private PillsScript pills;
    [SerializeField] private AntiRadiationScript antiRadiationFlasks;
    [SerializeField] private PocketBullets bullets;
    [SerializeField] private QuestManager questManager;
    
    public void GetRewards()
    {
        for (int i = 0; i < questManager.activeQuest.rewardItemType.Length; i++)
        {
            if (questManager.activeQuest.rewardItemType[i] == CollectibleType.Pills)
                pills.UpdatePills(questManager.activeQuest.rewardItemQuantities[i]);
            else if (questManager.activeQuest.rewardItemType[i] == CollectibleType.Ammo)
                bullets.UpdateBullets(questManager.activeQuest.rewardItemQuantities[i]);
            else if (questManager.activeQuest.rewardItemType[i] == CollectibleType.AntiRadiationFlasks)
                antiRadiationFlasks.UpdateFlasks(questManager.activeQuest.rewardItemQuantities[i]);
        }
    }
}
