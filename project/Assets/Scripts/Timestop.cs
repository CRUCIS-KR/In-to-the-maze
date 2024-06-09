using UnityEngine;

public class Timestop : MonoBehaviour
{
    public LayerMask ghost;
    private int ghostnum = 2;
    
    void Update() {
        if (GameManager.instance.getcoin() == 60)
            ghostnum = 4;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            int n = 0;
            Collider[] colliders = Physics.OverlapSphere(Vector3.zero, 600f, ghost);
            for (int i = 0; i < colliders.Length; i++) {
                GhostController g = colliders[i].GetComponent<GhostController>();
                if (g != null) {
                    g.setstop(true);
                    ++n;
                }
                if (n == ghostnum)
                    break;
            }
            Destroy(gameObject);
        }
    }
}
