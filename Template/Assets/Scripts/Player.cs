using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour{
    public int Energia;
    public float movX;
    bool verDerecha = true;
    Animator ManzanaAnim;
    Rigidbody2D cuerpo;
    public AudioSource ManzanaSFX;
    public AudioClip golpe;
    public AudioClip patada;
    public AudioClip srk;
    public Slider energyBar;
    public Text livesTXT;
    GameObject gm;

    public float fuerzaSalto = 50f;
    public bool tocarPiso = false;
    public Transform pies;
    float tPies = 0.5f;
    public LayerMask piso;

    // Start is called before the first frame update
    void Start(){
        ManzanaAnim = GetComponent<Animator>();
        Energia = 100;
        cuerpo = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKey(KeyCode.P)){
            ManzanaAnim.Play("apple_Punch");
            ManzanaSFX.clip = golpe;
            ManzanaSFX.Play();
        }
        if(Input.GetKey(KeyCode.U)){
            ManzanaAnim.Play("apple_Upper");
        }
        if(Input.GetKey(KeyCode.K)){
            ManzanaAnim.Play("apple_Kick");
        }
        if(Energia <= 0){
            ManzanaAnim.Play("apple_Death");
        }
        /*
        if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.A)){
            ManzanaAnim.Play("apple_Walk");
        }
        */
        tocarPiso = Physics2D.OverlapCircle(pies.position, tPies, piso);
        ManzanaAnim.SetBool("Floor", tocarPiso);

        if(Input.GetKey(KeyCode.UpArrow) && tocarPiso == true){
            cuerpo.AddForce(new Vector2(0, fuerzaSalto));
        }

        movX = Input.GetAxisRaw("Horizontal");
        ManzanaAnim.SetFloat("movH",Mathf.Abs(movX)); //el codigo manda al animator un dato
        ManzanaAnim.SetFloat("movV",cuerpo.velocity.y); //el codigo manda al animator un dato

        cuerpo.velocity = new Vector2(movX*5,cuerpo.velocity.y);
        if(verDerecha && movX < 0){
            Flip();
        }
        if(!verDerecha && movX > 0){
            Flip();
        }
        energyBar.value = Energia;
        livesTXT.text = "VIDAS: " + gm.GetComponent<GameManager>().vidas.ToString();
    }

    void Flip(){
        verDerecha = !verDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void Death(){
        Destroy(gameObject);
        SceneManager.LoadScene(1);
        gm.GetComponent<GameManager>().vidas -= 1;
    }
}