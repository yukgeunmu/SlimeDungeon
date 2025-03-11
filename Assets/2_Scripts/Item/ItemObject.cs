using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface IInteractable
{
    public string GetInteractPrompt();// ������ ���� ǥ��

    public void OnInteract(); // ��ȣ�ۿ� ȣ��
}


public class ItemObject : MonoBehaviour, IInteractable
{

    public ItemDate data;
    public MeshRenderer renderer;
    public Collider collider;
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
            switch(data.type)
            {
                case ItemType.Consumalbe:
                    UsingItem();
                    Destroy(gameObject);
                    break;
                case ItemType.Equipable:
                    EquipItme();
                    break;
                case ItemType.Interactive:
                    OpenBox();
                    break;
            }

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

    public void EquipItme()
    {
        transform.SetParent(CharacterManager.Instance.Player.ModelTransform);
        transform.localPosition = new Vector3(0 ,0.3f, 0);
        transform.localEulerAngles = Vector3.zero;
        gameObject.layer = LayerMask.NameToLayer("Equipment");
    }


    public void OpenBox()
    {
        SetTransparent(renderer, 0.5f);
        collider.enabled = false;
    }

    public void SetTransparent(MeshRenderer renderer, float alpha)
    {
        if (renderer == null) return;

        Material material = renderer.material;
        Color color = material.color;
        color.a = alpha; // ���İ� ����
        material.color = color;

        // ���� ó���� ���� ��Ƽ������ Render Mode ����
        material.SetFloat("_Mode", 3); // 3 = Transparent ���
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000; // Transparent Queue�� ����
    }


}