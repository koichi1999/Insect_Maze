using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemmon_Controller : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        //���m�����I�u�W�F�N�g�Ƀv���C���[�^�O�����Ă���΁A�ˏo�̃��\�b�h���Ăяo��
        if (collider.CompareTag("Player"))
        {
            Debug.Log("��������: �v���C���[�����m���܂���");
            gameObject.SetActive(false);
        }
    }
}
