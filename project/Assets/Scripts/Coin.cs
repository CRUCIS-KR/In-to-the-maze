using UnityEngine;

public class Coin : MonoBehaviour
{
    float r = 80f;
    void Update()
    {
        transform.Rotate(new Vector3(0, r * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameManager.instance.setcoin();
            Destroy(gameObject);
        }
    }
}
