using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player2 : MonoBehaviour{
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
    public AudioClip muerte;
    GameObject gm;

    public float fuerzaSalto = 100f;
    public bool tocarPiso = false;
    public Transform pies;
    float tPies = 0.5f;
    public LayerMask piso;
    public bool powered = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && powered == true)  // Ensure the colliding object is the player
        {
            if(GameObject.FindWithTag("PowerUp").GetComponent<Masher>() != null){
                GameObject.FindWithTag("PowerUp").GetComponent<Masher>().Initialize(collision.gameObject);  // Initialize or reinitialize the shooter
            }
            else{
                GameObject.FindWithTag("PowerUp").GetComponent<Shooter>().Initialize(collision.gameObject);
            }
            powered = false;
        }
    }
    
    // Start is called before the first frame update
    void Start(){
        ManzanaAnim = GetComponent<Animator>();
        Energia = 100;
        cuerpo = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKey(KeyCode.U)){    // Punch
            ManzanaAnim.Play("apple_Punch");
            ManzanaSFX.clip = golpe;
            ManzanaSFX.Play();
        }
        if (Input.GetKey(KeyCode.H))
        {    // Punch
            ManzanaAnim.Play("apple_Punch");
            ManzanaSFX.clip = golpe;
            ManzanaSFX.Play();
        }
        if (Input.GetKey(KeyCode.N)){    // Grab
            ManzanaAnim.Play("apple_Upper");
        }
        /*
        if(Input.GetKey(KeyCode.L)){    // Throw
            ManzanaAnim.Play("apple_Kick");
        }
        */
        if(Energia <= 0){               // Death
            ManzanaSFX.clip = muerte;
            ManzanaSFX.Play();
            ManzanaAnim.Play("apple_Death");
        }
        /*
        if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.A)){
            ManzanaAnim.Play("apple_Walk");
        }
        */

        energyBar.value = Energia;
    }
    void FixedUpdate(){
        // Handle horizontal movement
        if (Input.GetKey(KeyCode.J))
        {
            movX = -1;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            movX = 1;
        }
        else
        {
            movX = 0;
        }
        Collider2D collider = Physics2D.OverlapCircle(pies.position, tPies, piso);
        if(collider != null && collider.CompareTag("Piso")){
            tocarPiso = true;
        }
        else{
            tocarPiso = false;
        }

        ManzanaAnim.SetBool("Floor", tocarPiso);

        if(Input.GetKey(KeyCode.I) && tocarPiso == true){
            cuerpo.AddForce(new Vector2(0, fuerzaSalto));
        }

        // movX = Input.GetAxisRaw("Horizontal");
        ManzanaAnim.SetFloat("movH",Mathf.Abs(movX)); //el codigo manda al animator un dato
        ManzanaAnim.SetFloat("movV",cuerpo.velocity.y); //el codigo manda al animator un dato

        cuerpo.velocity = new Vector2(movX*8,cuerpo.velocity.y);
        if(verDerecha && movX > 0){
            Flip();
        }
        if(!verDerecha && movX < 0){
            Flip();
        }
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