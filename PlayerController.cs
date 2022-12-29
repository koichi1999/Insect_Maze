using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float Magnification; //������̗͂�
    private Vector3 movingDirecion; //�͂����������
    private Vector3 movingVelocity; //���݂̃x�N�g������
    //private Transform _transform; //Transform�̃L���b�V��

    private Vector3 differenceDis;
    private Vector3 latestPos;
    private Quaternion rot;

    [SerializeField] private Animator animator;
    [SerializeField] private Count_Manager count_Manager;

    public Aroma_Inject[] injection; //Injection�N���X�^��z��.�O���̃N���X������Q�Ƃ���

    private void Start()
    {
        //_transform = transform; //transform���L���b�V������Ə��������A���ׂ�������

        movingDirecion.y = 0; //Y�����ɂ͗͂������Ȃ�

        injection = new Aroma_Inject[3]; //�z��̒��������߂�

        for (int i = 0; i < 3; i++)
        {
            //Injection�N���X����Spray_Function()���\�b�h���Ăяo���āA"Spray"�Ƃ����R���[�`�������s���悤��
            //�����Ƃ��AMonoBehaviour�i�y�т�����p������Injection�N���X�j��new�Œ��ڐ������Ă���̂��G���[�̌���
            //injection[i] = new Injection();//�C���X�^���X�𐶐����Ď��̂����


            //"Spray"�R���[�`���Ăяo���i���̏ꍇ�͐���Ɏ��s�ł���j.�Q�l�L��(https://qiita.com/norikiyo777/items/0bedfdc239f85032ac86)
            injection[i] = (new GameObject("Injection_Class")).AddComponent<Aroma_Inject>();
            injection[i].channel = (i + 1); //�ˏo�`�����l��

        }
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        movingDirecion = new Vector3(x, 0, z);
        movingDirecion.Normalize();//�΂߂̋����������Ȃ�̂�h��.�Q�l����(�x�N�g���𐳋K������, https://nekojara.city/unity-normalize-vector)
        movingVelocity = movingDirecion * Magnification;

        //Debug.Log("x:" + x); ���͎��̒l�����
        //Debug.Log("z:" + z); ���͎��̒l�����

        //�ړ��X�s�[�h��animator�ɔ��f����
        animator.SetFloat("MoveSpeed", new Vector3(movingVelocity.x, 0, movingVelocity.z).magnitude);
    }

    //�f�t�H���g����0.02�b���ƂɌĂяo�����
    private void FixedUpdate()
    {
        rb.velocity = movingVelocity;

        //�O�t���[���Ƃ̈ʒu�̍�����i�s����������o���Ă��̕����ɉ�]���܂��B
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

    //�ʕ��֘A�̃^�O���t�����I�u�W�F�N�g�ƏՓ˂����Ƃ��̏���
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
    private const int duration = 300; //�ˏo����.�~���b�P��
    public int channel; //Spray_1027()�֐��Ŏˏo����^���N�̃`�����l���𐧌䂷��ϐ�
    private float duration_f; //duration��b�ɕϊ�.�R���[�`���Ń��\�b�h�̒�~���Ԃ��w�肷��

    public void Spray_Function()
    {
        StartCoroutine(Spray(channel, duration,duration_f));
        //�R���[�`���̎g������ �Q�l�L��(https://tech.pjin.jp/blog/2021/12/23/unity-coroutine/)
    }

    private IEnumerator Spray(int channel, int duration, float duration_f)
    {
        HumanDll.HumanClass1.Spray_1027(channel, duration); //�ˏo�֐�
        duration_f = (float)duration / 1000;
        yield return new WaitForSeconds(duration_f); //�������w��b���̂�������~����
        //Debug.Log(duration_f + "�b�o�߂��܂���");
    }
}
