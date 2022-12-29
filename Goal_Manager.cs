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
        //���m�����I�u�W�F�N�g�Ƀv���C���[�^�O�����Ă���΁A�ˏo�̃��\�b�h���Ăяo��
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�S�[����: �v���C���[�����m���܂���");
            if (isGoal)
            {
                clear_Text.text = "Game Clear!";
                //4�b��Ƀ��\�b�h�����s����
                Invoke("LoadEndingScene", 4f);

            }
        }
    }

    //�Q�l�L��(�V�[���؂�ւ��𐔕b�x�点����@, https://ymgsapo.com/2021/01/14/unity-delay-scene-load/)
    private void LoadEndingScene()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
