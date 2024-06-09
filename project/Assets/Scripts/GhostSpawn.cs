using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    public GameObject GhostPrefab, g1, g2, g3, g4;
    private PlayerController p;
    private Vector3[] tp;
    private int sector, key1 = 0, key2 = 0;
    void Start() {
        tp = new Vector3[4];
        tp[0] = new Vector3(-150, 8, 150);
        tp[1] = new Vector3(150, 8, 150);
        tp[2] = new Vector3(-150, 8, -150);
        tp[3] = new Vector3(150, 8, -150);
        p = FindObjectOfType<PlayerController>();
    }

    void Update() {
        if (p.isdie == false && isstop(key1)){
            int n1 = Random.Range(0, tp.Length);
            int n2 = Random.Range(0, tp.Length);
            while (n1 == n2)
                n2 = Random.Range(0, tp.Length);
            g1 = Instantiate(GhostPrefab, tp[n1], transform.rotation);
            g2 = Instantiate(GhostPrefab, tp[n2], transform.rotation);
            g1.transform.LookAt(p.transform.position);
            g2.transform.LookAt(p.transform.position);
            key1 = 1;
        }
        if (GameManager.instance.getcoin() == 60 && isstop(key2)) {
            if (p.transform.position.x < 0 && p.transform.position.z > 0)
                sector = 1;
            else if (p.transform.position.x > 0 && p.transform.position.z > 0)
                sector = 2;
            else if (p.transform.position.x < 0 && p.transform.position.z < 0)
                sector = 3;
            else
                sector = 4;
            int n3 = Random.Range(0, tp.Length);
            int n4 = Random.Range(0, tp.Length);
            while (n3 == sector || n4 == sector) {
                if (n3 == sector)
                    n3 = Random.Range(0, tp.Length);
                if (n4 == sector)
                    n4 = Random.Range(0, tp.Length);
            }
            g3 = Instantiate(GhostPrefab, tp[n3], transform.rotation);
            g4 = Instantiate(GhostPrefab, tp[n4], transform.rotation);
            g3.transform.LookAt(p.transform.position);
            g4.transform.LookAt(p.transform.position);
            key2 = 1;
        }
        if (GameManager.instance.isover() == true) {
            if (g1 != null)
                Destroy(g1);
            if (g2 != null)
                Destroy(g2);
            if (g3 != null)
                Destroy(g3);
            if (g4 != null)
                Destroy(g4);
        }
    }

    private bool isstop(int key) {
        if (key == 0)
            return true;
        else 
            return false;
    }
}