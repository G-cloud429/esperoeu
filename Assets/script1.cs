using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MoverCubo : MonoBehaviour
{
    public float velocidadeFrente = 22f;
    public float velocidadeLado = 22f;
    public Rigidbody rb;
    public AudioSource som;
    public AudioClip somEstrela;
    public AudioClip sombarreira;


    public TMP_Text textoEstrelas;
    public TMP_Text textoVidas;

    int estrelas = 0;
    int vidas = 3;

    void Start()
    {
        Time.timeScale = 1;
        if (rb == null) rb = GetComponent<Rigidbody>();
        AtualizarTextos();
    }

    void FixedUpdate()
    {
        rb.AddForce(0,0,15);
    if (Input.touchCount > 0)
        {
            Touch toque=Input.GetTouch(0);
            if (toque.position.x < Screen.width / 2)
            {
                rb.AddForce(-55,0,0);
            }
            else
            {
                rb.AddForce(55,0,0);
            }
        }
        // se cair do mapa, reinicia
        if (transform.position.y < -10)
            Reiniciar();
    }

    private void OnCollisionEnter(Collision colisao)
    {
        if (colisao.gameObject.CompareTag("barreira"))
        {
            som.PlayOneShot(sombarreira);
            vidas--;
            AtualizarTextos();

            Destroy(colisao.gameObject);   // a barreira desaparece

            if (vidas <= 0)
                Reiniciar();

        }
        if ( colisao.gameObject.CompareTag("fim"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }





    }






    private void OnTriggerEnter(Collider outro)
    {
        if (outro.CompareTag("Estrela"))
        {
            som.PlayOneShot(somEstrela);
            estrelas++;
            AtualizarTextos();
            Destroy(outro.gameObject);
        }
    }

    void AtualizarTextos()
    {
        if (textoEstrelas != null) textoEstrelas.text = estrelas.ToString();
        if (textoVidas != null) textoVidas.text = vidas.ToString();
    }

    void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}