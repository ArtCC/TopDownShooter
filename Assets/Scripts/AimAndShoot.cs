using UnityEngine;

public class AimAndShoot : MonoBehaviour {
    [SerializeField] private Transform playerTransform, shootPosition;
    [SerializeField] private float aimSpeed;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private AudioClip launchArrowClip;
    
    private Camera cam;
    private Vector2 mouseWorldPosition, direction;

    void Start() {
        cam = Camera.main;
    }

    void Update() {
        if (GameManager.Instance.IsPlayerDead) {
            return;
        }

        transform.position = playerTransform.position;
        
        Aim();
        Shoot();
    }

    private void Aim() {
        mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);

        direction = (mouseWorldPosition - (Vector2)transform.position).normalized;

        transform.right = Vector2.MoveTowards(transform.right, direction, aimSpeed * Time.deltaTime);
    }

    private void Shoot() {
        if (Input.GetButtonDown("Fire1")) {
            AudioManager.Instance.PlaySoundEffect(launchArrowClip, 0.5f);
            
            GameObject arrow = Instantiate(arrowPrefab, shootPosition.position, transform.rotation);
            arrow.GetComponent<Arrow>().Launch(direction);
        }
    }
}