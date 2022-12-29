using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count_Manager : MonoBehaviour
{
    //�Q�l����(�Q�[����ɓG�����̂��邩���m�F������@, https://mono-pro.net/archives/7716)
    private int sum; //�A�C�e���̍��v��
    [SerializeField] private Text countText; //���݂̃A�C�e���擾����UI.text�ŕ\�����邽�߂̕ϐ�
    [SerializeField] private GameObject[] FruitBox; //�t�B�[���h���ɂ��邷�ׂẴA�C�e�������擾

    // Start is called before the first frame update
    private void Start()
    {
        sum = 0;
        countText.text = "�ʕ��擾��: " + sum;
        FruitBox = GameObject.FindGameObjectsWithTag("Fruit_Object");
        print("�t�B�[���h��̉ʕ��̐��F" + FruitBox.Length);
    }

    //�v���C���[���ʕ��ƐڐG�����Ƃ��ɌĂяo�����
    public void SetCountText()
    {
        sum = sum + 1;
        countText.text = "�ʕ��擾��: " + sum.ToString(); //���݂̃A�C�e���擾����\��

        //���݂̉ʕ����v�擾�����Q�[���J�n���̃t�B�[���h���ɂ��邷�ׂẲʕ����ƈ�v�����Ƃ�
        if (sum == FruitBox.Length)
        {
            Debug.Log("�t�B�[���h���̑S�ẴA�C�e�����擾���܂����B");
            //�Q�[���N���A�̏���
            Goal_Manager.isGoal = true;
        }
    }
}
