using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cucecontr : MonoBehaviour
{
    Transform playerbody;
    float gravity = -9.81f;
    CharacterController contr;
    public float speed = 12f;
    bool isGrounded = false;
    int time = 10;
    [SerializeField] TextMeshProUGUI sec;
    [SerializeField] TextMeshProUGUI tt;
    void timeMinus()
    {
        time = time - 1;
        sec.text = "" + time;
    }

    void Start()
    {
        InvokeRepeating("timeMinus", 1f, 1f);
        playerbody = GetComponent<Transform>();
        contr = GetComponent<CharacterController>();

    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        contr.Move(playerbody.up * gravity * Time.deltaTime);
        float vertical = Input.GetAxis("Vertical");
        contr.Move(playerbody.forward * vertical * speed * Time.deltaTime);
        playerbody.Rotate(0, mouseX, 0);
        if (Input.GetKeyDown("space") && isGrounded == true)
        {
            contr.Move(playerbody.up * 5f);
        }
        else
        {
            isGrounded = false;
        }
        if (time == 0)
        {
            CancelInvoke("timeMinus");
            tt.text = "You lose";
            Destroy(contr);
        }

    }
    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if (col.gameObject.tag == "graund")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "Finish")
        {
            isGrounded = true;
            tt.text = "You win";
            CancelInvoke("timeMinus");
        }
    }
}