using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager GM;
    private Mouse mouse;
    private PlayerController playerController;
    private bool over = false, tk = false;
    private int coin = 0;
    public Text totalcoin;
    public GameObject GameOverUI, PlayUI, OpenUI, EndGame, tracking, potalprefab;
    public Slider rushbar;
    
    public static GameManager instance {
        get {
            if (GM == null)
                GM = FindObjectOfType<GameManager>();
            return GM;
        }
    }
    
    void Awake() {
        if (instance != this)
            Destroy(gameObject);
    }
    
    void Start() {
        mouse = FindObjectOfType<Mouse>();
        playerController = FindObjectOfType<PlayerController>();
        playerController.playerRigidbody.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        OpenUI.SetActive(true);
    }

    public void gamestart() {
        Cursor.lockState = CursorLockMode.Locked;
        playerController.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        mouse.isdie = false;
        playerController.isdie = false;
        OpenUI.SetActive(false);
        PlayUI.SetActive(true);
    }
    public void Exit() {
        Application.Quit();
    }

    void Update()
    {
        if (over == false){
            if (PlayUI.activeInHierarchy){
                 addcoin(coin);
                 if (tk)
                    tracking.SetActive(true);
                else
                    tracking.SetActive(false);
                calrush();
            }
        }
    }
    private void addcoin(int coin) {
        totalcoin.text = "Coin\n" + coin + " / 180";
    }
    public void istracking(bool i) {
        tk = i;
    }
    private void calrush() {
        if (playerController.isok == true)
            rushbar.value = playerController.rushtime;
    }
    
    public void stoprush() {
        StartCoroutine(charge());
    }
    
    private IEnumerator charge() {
        rushbar.maxValue = 10f;
        rushbar.value = 0f;
        for (int i = 1; i <= 10; i++){
            rushbar.value += 1f;
            yield return new WaitForSeconds(1f);
        }
        rushbar.maxValue = playerController.rushtime;
        rushbar.value = playerController.rushtime;
    }

    public void setcoin() {
        coin += 1;
        if (coin == 180)
            potalspawn();
    }
    
    public int getcoin() {
        return coin;
    }
    
    public void gameover() {
        over = true;
        PlayUI.SetActive(false);
        GameOverUI.SetActive(true);
    }

    public bool isover() {
        return over;
    }
    
    public void Continue() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void potalspawn() {
        GameObject potal = Instantiate(potalprefab, new Vector3(0, 10, 0), transform.rotation);
    }
    
    public void endgame() {
        over = true;
        PlayUI.SetActive(false);
        EndGame.SetActive(true);
    }
}
