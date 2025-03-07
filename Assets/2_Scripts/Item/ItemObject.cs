using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface IInteractable
{
    public string GetInteractPrompt();// 아이템 정보 표시

    public void OnInteract(); // 상호작용 호출
}


public class ItemObject : MonoBehaviour, IInteractable
{

    public ItemDate data;
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.descroption}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        if(data.type != ItemType.Pad)
        {   
            UsingItem();
            Destroy(gameObject);
        }

    }

    public void UsingItem()
    {
        for(int i = 0; i < data.consumables.Length; i++)
        {
            switch(data.consumables[i].type)
            {
                case ConsumableType.Health:
                    CharacterManager.Instance.Player.playerCondition.Heal(data.consumables[i].value);
                    break;
                case ConsumableType.Stamina:
                    CharacterManager.Instance.Player.playerCondition.HealStamina(data.consumables[i].value);
                    break;
                case ConsumableType.Speed:
                    CharacterManager.Instance.Player.playerController.AddSpeed(data.consumables[i].value, 5f);
                    break;
            }
        }
     
    }

 

}