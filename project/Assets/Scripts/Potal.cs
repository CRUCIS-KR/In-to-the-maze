using UnityEngine;

public class Potal : MonoBehaviour
{
    private float r = 50f;
    void Update()
    {
        transform.Rotate(new Vector3(0, r * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Destroy(gameObject);
            PlayerController p = other.GetComponent<PlayerController>();
            p.die();
            GameManager.instance.endgame();
        }
    }
}
