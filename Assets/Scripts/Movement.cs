using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocidad = 5f; // velocidad de movimiento
    public float fuerzaSalto = 10f; // fuerza de salto
    private Rigidbody rb; // rigidbody del cubo
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal"); // obtenemos el input horizontal
        float z = Input.GetAxis("Vertical"); // obtenemos el input vertical

        // creamos un vector de movimiento en función del input
        Vector3 movimiento = new Vector3(x, 0f, z) * velocidad * Time.deltaTime;

        // aplicamos el movimiento al cubo
        transform.Translate(movimiento);

        // si se presiona la tecla de espacio, hacemos que el cubo salte
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision other) {
        string collTag = other.gameObject.tag;
        if (collTag == "field1" || collTag == "field2"){
            isJumping = false;
        }
    }
}