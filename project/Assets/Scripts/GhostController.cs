using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class GhostController : MonoBehaviour
{
    public PlayerController player;
    private NavMeshAgent navMeshAgent;
    private bool stop = false;

    public void setstop(bool set) {
        stop = set;
    }

    void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        player = FindObjectOfType<PlayerController>();
        navMeshAgent.speed = 8f;
        navMeshAgent.isStopped = false;
        StartCoroutine(tracking());    
    }

    private IEnumerator tracking() {
        while (GameManager.instance.isover() == false) {
            if (stop) {
                navMeshAgent.isStopped = true;
                yield return new WaitForSeconds(5f);
                navMeshAgent.isStopped = false;
                setstop(false);
            }
            navMeshAgent.speed = 8f;
            navMeshAgent.SetDestination(player.transform.position);
            RaycastHit point;
            if (Physics.Raycast(transform.position, Vector3.forward, out point, 450f)) {
                if (point.collider.tag == "Player"){
                    navMeshAgent.speed = 25f;
                    GameManager.instance.istracking(true);
                    yield return new WaitForSeconds(3f);
                    GameManager.instance.istracking(false);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Destroy(gameObject);
            PlayerController p = other.GetComponent<PlayerController>();
            p.die();
            GameManager.instance.gameover();
        }
    }
}
