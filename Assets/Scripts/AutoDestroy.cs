using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    private float destroyTime = 1;
    
    void Start() {
        Destroy(gameObject, destroyTime);
    }
}