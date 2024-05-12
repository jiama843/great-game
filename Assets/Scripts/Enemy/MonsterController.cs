using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] string affinity;

    // skeleton -> KEY
    // slime -> SLIME_JAR
    [SerializeField] string droppedItemName;

    [Header("Sprites")]
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite damagedSprite;
    [SerializeField] Sprite healedSprite;

    [Header("Particle Effects")]
    ParticleSystem healEffect;
    ParticleSystem damageEffect;

    int health = 2;
    bool isDead = false;
    [SerializeField] SpriteRenderer spriteRenderer;


    // Since monsters are prefabs and instantiated by BattleManager, I think we need to use Awake
    void Awake()
    {
        spriteRenderer.sprite = defaultSprite;
        healEffect = GameObject.Find("HealEffect").GetComponent<ParticleSystem>();
        damageEffect = GameObject.Find("DamageEffect").GetComponent<ParticleSystem>();
    }

    public string GetAffinitiy()
    {
        return affinity;
    }

    public string GetDroppedItemName()
    {
        return droppedItemName;
    }

    // Since monsters have 2 life, we can just show the damaged sprite 
    // to indicate they are about to die for now...
    public void TakeDamage()
    {
        health -= 1;
        damageEffect.Play();

        if (health == 0)
        {

            isDead = true;
        }
        else
        {
            spriteRenderer.sprite = damagedSprite;
        }
    }

    public void Heal()
    {
        healEffect.Play();
        spriteRenderer.sprite = healedSprite;
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
