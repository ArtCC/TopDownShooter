using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject explosionPrefabs;
    [SerializeField] private EnemyHealthBar healthbar;
    [SerializeField] private AudioClip enemyDamageClip;
    [SerializeField] private AudioClip enemyDieCLip;

    private int health;
    private SpriteRenderer spriteRenderer;

    void Start() {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Arrow arrow = collision.gameObject.GetComponent<Arrow>();
        
        if (arrow != null) {
            TakeDamage();
        }
    }

    private void TakeDamage() {
        health--;

        healthbar.UpdateHealthBar(maxHealth, health);

        if (health <= 0) {
            AudioManager.Instance.PlaySoundEffect(enemyDieCLip, 0.5f);

            Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
            Destroy(gameObject);

            GameManager.Instance.DecreaseEnemiesLeft();
        } else {
            AudioManager.Instance.PlaySoundEffect(enemyDamageClip, 0.5f);

            StartCoroutine(Blink(0.2f));
        }
    }

    private IEnumerator Blink(float blinkTime) {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(blinkTime);
        spriteRenderer.color = Color.white;
    }
}