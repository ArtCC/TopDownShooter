using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {
    [SerializeField] private Image healthbar;

    public void UpdateHealthBar(int maxHeight, int health) {
        healthbar.fillAmount = (float) health / maxHeight;
    }
}