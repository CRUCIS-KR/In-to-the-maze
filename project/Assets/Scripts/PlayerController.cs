using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float speed;
    public float rushtime = 5f;
    public bool isok = true, isdie;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        isdie = true;
    }

    void Update() { 
        if (isdie == false){
            if (Input.GetKey(KeyCode.LeftShift) == true && isok == true) {
                if (rushtime >= 0) {
                    speed = 25f;
                    rushtime -= Time.deltaTime;
                }
                else {
                    speed = 12f;
                    isok = false;
                    StartCoroutine(cooltime());
                }
            }
            else {
                speed = 12f;
                if (0 < rushtime && rushtime < 5 && isok == true)
                    rushtime += Time.deltaTime;
            }
            float x = Input.GetAxis("Horizontal") * speed;
            float y = Input.GetAxis("Vertical") * speed;
            playerRigidbody.velocity = transform.right * x + transform.forward * y;
        }  
    }

    private IEnumerator cooltime() {
        GameManager.instance.stoprush();
        yield return new WaitForSeconds(10f);
        rushtime = 5f;
        isok = true;
    }

    public void die() {
        isdie = true;
        Mouse m = GetComponent<Mouse>();
        playerRigidbody.velocity = transform.right * 0 + transform.forward * 0;
        playerRigidbody.angularVelocity = Vector3.zero;
        m.die();
    }
}
