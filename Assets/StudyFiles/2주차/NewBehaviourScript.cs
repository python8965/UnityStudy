using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("오브젝트가 일어났습니다");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("프레임의 시작입니다");
    }

    private void FixedUpdate()
    {
        Debug.Log("프레임의 고정된 업데이트입니다; 물리학 업데이트를 할 때 쓰입니다.");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("프레임의 업데이트입니다; 게임 로직을 업데이트 할 때 쓰입니다.");
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
    }

    private void LateUpdate()
    {
        Debug.Log("업데이트 후에 실행됩니다.");
    }

    private void OnDestroy()
    {
        Debug.Log("오브젝트가 파괴되었습니다.");
    }
    
    private void OnDisable()
    {
        Debug.Log("오브젝트가 비활성화 되었습니다.");
    }
    
    private void OnEnable()
    {
        Debug.Log("오브젝트가 활성화 되었습니다.");
    }
}
