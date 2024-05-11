using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] string monsterName;
    [SerializeField] string affinity;
    [SerializeField] string droppedItem;

    [Header("Sprites")]
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite damagedSprite;
    [SerializeField] Sprite healedSprite;
    int health = 2;
    [SerializeField] SpriteRenderer spriteRenderer;

    // Since monsters are prefabs and instantiated by BattleManager, I think we need to use Awake
    void Awake()
    {
        // TODO: Load sprites
        spriteRenderer.sprite = defaultSprite;
    }

    public string GetAffinitiy()
    {
        return affinity;
    }

    // Since monsters have 2 life, we can just show the damaged sprite 
    // to indicate they are about to die for now...
    public void TakeDamage()
    {
        health -= 1;

        if (health == 0)
        {
            PlayDeathAnimation();
            // TODO: Wait and transition back to game
        }
        else
        {
            spriteRenderer.sprite = damagedSprite;
        }
    }

    void PlayDeathAnimation() { }

    public void Heal()
    {
        Debug.Log("Healed!");
        spriteRenderer.sprite = healedSprite;
        // TODO: Wait and transition back to game
    }
}
