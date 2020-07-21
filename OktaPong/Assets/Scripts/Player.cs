using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool podeMover = false; // Bool que verifica se o player pode se mover
    public bool passarTurno = false; // Bool para o tratamento dos turnos
    public bool umaBala = false; // Para que instacie só uma bala por turno do jogador 
    public float speed = 10.0f; // Velocidade que o player se movimenta
    public float rotationSpeed = 100.0f; // Velocidade de rotação do player
    public int vida = 100; // Vida do player, se alguma chegar a 0, o jogo acaba
    public GameObject[] balas;// Vetor usado para guardas os tipos de bala que os players usam
    public GameObject balaUsada;// Bala escolhida pelo jogador no momento
    public Bala balaCod;// Script da bala escolhida para movimentação e tiro
    public Transform mira;// Objeto criança do jogador, usado como referencia para a movimentação e tiro da bala
    public float alturaMax;// Altura máxima que o jogador pode chegar na tela
    public float alturaMin;// Altura mínima que o jogador pode chegar na tela
    public float anguloMax;// Rotação máxima que o jogador pode chegar na tela
    public float anguloMin;// Rotação mínima que o jogador pode chegar na tela
    private Rigidbody2D rb; // RigidBody2D do player
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // RigidBody2D do player armazenado em uma variável
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Lógica de movimentação e tiro do player
        if (podeMover == true)
        {
            // Função usada para se mover na vertical
            Movimentacao();

            //Função usada para rotacionar
            Rotacao();

            if (Input.GetKeyDown(KeyCode.Space) && balaUsada != null)
            {
                // Função para atirar e passar o turno
                atirar();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
           {
               // Função para trocar a bala no Vetor
               trocarBala(0);
           }
           else if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
               trocarBala(1);
           }
            
            if (balaCod.atirou == false )
            {
                // Enquanto a variável atirou do Script balaCod for falso, a bala ira receber a direcao e movimentação
                // Do objeto criança que se encontra no player
                balaCod.playerAtirando = this.tag;
                balaUsada.transform.position = mira.position;
               
                balaCod.direcao = mira.up;
                  
            }

            
         
        }

       


    }


    //Movimentação do Player
    private void Movimentacao()
    {
        
       
            if (Input.GetKey(KeyCode.UpArrow) && rb.position.y <= alturaMax)
            {
                rb.velocity += new Vector2(0, speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && rb.position.y >= alturaMin )
            {
                rb.velocity += new Vector2(0, (speed * Time.deltaTime) * -1);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        
    }


    // Rotação do Player
    private void Rotacao()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && rb.rotation < anguloMax)
        {
            rb.MoveRotation(rb.rotation + rotationSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) && rb.rotation > anguloMin)
        {
            rb.MoveRotation(rb.rotation - rotationSpeed * Time.fixedDeltaTime);
        }


    }

    // Tiro do player e também usado para passar o turno
    private void atirar()
    {
        podeMover = false; // Player não pode se mover
        rb.velocity = Vector2.zero;// Velocidade do RigidBody2D recebe zero para não deslizar na tela
        passarTurno = true;// Variavel para o GameManager tratar os turnos

        balaCod.atirou = true;// Variavel do Script determinando se a bala foi atirada
        umaBala = false; // Variavel recebendo falso para quando for o turno do player denovo uma bala poder se instanciada
    }


    //Função para trocar a bala do player
    private void trocarBala(int tipo)
    {
        // Se o objeto não for nulo, destroy o objeto e muda a variavel umaBala para falso para instanciar outra bala
        if (balaUsada != null)
        {
            Destroy(balaUsada.gameObject);
            umaBala = false;
        }

        //Bala nova sendo instanciada
        if (umaBala == false)
        {
            balaUsada = Instantiate(balas[tipo], mira.position, Quaternion.identity);
         
            balaCod = balaUsada.GetComponent<Bala>(); //balaCod recebendo Script da bala atual 
            umaBala = true; //Variavel setada para true para não instanciar outra bala
        }
        
    }

}
