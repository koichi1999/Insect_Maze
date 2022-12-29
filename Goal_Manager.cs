using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal_Manager : MonoBehaviour
{
    public static bool isGoal;

    [SerializeField] private Text clear_Text;
    // Start is called before the first frame update
    private void Start()
    {
        isGoal = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //検知したオブジェクトにプレイヤータグがついていれば、射出のメソッドを呼び出す
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ゴール側: プレイヤーを検知しました");
            if (isGoal)
            {
                clear_Text.text = "Game Clear!";
                //4秒後にメソッドを実行する
                Invoke("LoadEndingScene", 4f);

            }
        }
    }

    //参考記事(シーン切り替えを数秒遅らせる方法, https://ymgsapo.com/2021/01/14/unity-delay-scene-load/)
    private void LoadEndingScene()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
