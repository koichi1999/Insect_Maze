using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float Magnification; //加える力の量
    private Vector3 movingDirecion; //力を加える方向
    private Vector3 movingVelocity; //現在のベクトルを代入
    //private Transform _transform; //Transformのキャッシュ

    private Vector3 differenceDis;
    private Vector3 latestPos;
    private Quaternion rot;

    [SerializeField] private Animator animator;
    [SerializeField] private Count_Manager count_Manager;

    public Aroma_Inject[] injection; //Injectionクラス型を配列化.外部のクラスからも参照する

    private void Start()
    {
        //_transform = transform; //transformをキャッシュすると少しだけ、負荷が下がる

        movingDirecion.y = 0; //Y方向には力を加えない

        injection = new Aroma_Inject[3]; //配列の長さを決める

        for (int i = 0; i < 3; i++)
        {
            //InjectionクラスからSpray_Function()メソッドを呼び出して、"Spray"というコルーチンを実行しようと
            //したとき、MonoBehaviour（及びそれを継承したInjectionクラス）をnewで直接生成しているのがエラーの原因
            //injection[i] = new Injection();//インスタンスを生成して実体を作る


            //"Spray"コルーチン呼び出し（この場合は正常に実行できる）.参考記事(https://qiita.com/norikiyo777/items/0bedfdc239f85032ac86)
            injection[i] = (new GameObject("Injection_Class")).AddComponent<Aroma_Inject>();
            injection[i].channel = (i + 1); //射出チャンネル

        }
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        movingDirecion = new Vector3(x, 0, z);
        movingDirecion.Normalize();//斜めの距離が長くなるのを防ぐ.参考文献(ベクトルを正規化する, https://nekojara.city/unity-normalize-vector)
        movingVelocity = movingDirecion * Magnification;

        //Debug.Log("x:" + x); 入力軸の値を取る
        //Debug.Log("z:" + z); 入力軸の値を取る

        //移動スピードをanimatorに反映する
        animator.SetFloat("MoveSpeed", new Vector3(movingVelocity.x, 0, movingVelocity.z).magnitude);
    }

    //デフォルトだと0.02秒ごとに呼び出される
    private void FixedUpdate()
    {
        rb.velocity = movingVelocity;

        //前フレームとの位置の差から進行方向を割り出してその方向に回転します。
        differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(latestPos.x, 0, latestPos.z);
        latestPos = transform.position;
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            if (movingDirecion == new Vector3(0, 0, 0)) return;
            rot = Quaternion.LookRotation(-differenceDis);
            rot = Quaternion.Slerp(rb.transform.rotation, rot, 0.03f);
            this.transform.rotation = rot;
        }
    }

    //果物関連のタグが付いたオブジェクトと衝突したときの処理
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Banana"))
        {
            injection[0].Spray_Function();
            count_Manager.SetCountText();
            Debug.Log("Banana");
        }

        if (collider.CompareTag("Lemmon"))
        {
            injection[1].Spray_Function();
            count_Manager.SetCountText();
            Debug.Log("Lemmon");
        }

        if (collider.CompareTag("Peach"))
        {
            injection[2].Spray_Function();
            count_Manager.SetCountText();
            Debug.Log("Peach");
        }        
    }
}

public class Aroma_Inject : MonoBehaviour
{
    private const int duration = 300; //射出時間.ミリ秒単位
    public int channel; //Spray_1027()関数で射出するタンクのチャンネルを制御する変数
    private float duration_f; //durationを秒に変換.コルーチンでメソッドの停止時間を指定する

    public void Spray_Function()
    {
        StartCoroutine(Spray(channel, duration,duration_f));
        //コルーチンの使いかた 参考記事(https://tech.pjin.jp/blog/2021/12/23/unity-coroutine/)
    }

    private IEnumerator Spray(int channel, int duration, float duration_f)
    {
        HumanDll.HumanClass1.Spray_1027(channel, duration); //射出関数
        duration_f = (float)duration / 1000;
        yield return new WaitForSeconds(duration_f); //処理を指定秒数のあいだ停止する
        //Debug.Log(duration_f + "秒経過しました");
    }
}
