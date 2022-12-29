using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count_Manager : MonoBehaviour
{
    //参考文献(ゲーム上に敵が何体いるかを確認する方法, https://mono-pro.net/archives/7716)
    private int sum; //アイテムの合計数
    [SerializeField] private Text countText; //現在のアイテム取得数をUI.textで表示するための変数
    [SerializeField] private GameObject[] FruitBox; //フィールド内にあるすべてのアイテム数を取得

    // Start is called before the first frame update
    private void Start()
    {
        sum = 0;
        countText.text = "果物取得数: " + sum;
        FruitBox = GameObject.FindGameObjectsWithTag("Fruit_Object");
        print("フィールド上の果物の数：" + FruitBox.Length);
    }

    //プレイヤーが果物と接触したときに呼び出される
    public void SetCountText()
    {
        sum = sum + 1;
        countText.text = "果物取得数: " + sum.ToString(); //現在のアイテム取得数を表示

        //現在の果物合計取得数がゲーム開始時のフィールド内にあるすべての果物数と一致したとき
        if (sum == FruitBox.Length)
        {
            Debug.Log("フィールド内の全てのアイテムを取得しました。");
            //ゲームクリアの条件
            Goal_Manager.isGoal = true;
        }
    }
}
