using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana_Controller : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        //検知したオブジェクトにプレイヤータグがついていれば、射出のメソッドを呼び出す
        if (collider.CompareTag("Player"))
        {
            Debug.Log("バナナ側: プレイヤーを検知しました");
            gameObject.SetActive(false);
        }
    }
}
