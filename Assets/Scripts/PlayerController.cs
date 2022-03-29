using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    [SerializeField] private float zSpeed;
    private float _currentRunningSpeed; //4
    [SerializeField] private float limit_Z;
    public Vector3 offSet;

    public float runningSpeed = 4f;
    public bool gameCont = false;

    public Animator playerAnim;

    Camera cam;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void SetSpeed(float val)
    { 
        _currentRunningSpeed = val;
        playerAnim.SetBool("Move", true);
    }

    void Update()
    {
        if (!gameCont)
            return;
      

        float newX = 0;
        float touchXDelta = 0;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchXDelta = -Input.GetTouch(0).deltaPosition.x / Screen.width; // parmak kaydırması
        }
        else if (Input.GetMouseButton(0))
        {
            touchXDelta = -Input.GetAxis("Mouse X"); // mouse kaydırması
        }
        newX = transform.position.z + zSpeed * touchXDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limit_Z, limit_Z); // Sınırlandırma

        Vector3 newPosition = new Vector3(transform.position.x + _currentRunningSpeed * Time.deltaTime, transform.position.y, newX);
        transform.position = newPosition;
    }


}
