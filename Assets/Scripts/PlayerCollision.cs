using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    [SerializeField] private AudioClip playerDie;
    
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            AudioManager.Instance.PlaySoundEffect(playerDie, 0.5f);
            
            animator.SetTrigger("Die");

            GameManager.Instance.Die();
        }
    }
}