using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllMaster : MonoBehaviour
{
    // Start is called before the first frame update

void Start()
    {

    }

    void Update()//Update�@���́E�A�j���[�V����
    {
    }

    void Hit()//�����������ɔ�����m�F����֐��B
    {
        //���������I�u�W�F�N�g�̏����擾���z�񓙂Ɋi�[����
        //�z�񂩂�m�F���ĊY���̃I�u�W�F�N�g�̂�List�Ɉړ�����B
        //foreach�ŃI�u�W�F�N�g�̏�Ԃ��m�F�B
        //����O��TargetResarch�̊֐��������g�����肵���㕪��ŕ�����B
        //�����true��JumpFlagment�̊֐�����������B
        //Break
    }

    bool JumpFlgment()//�W�����v���ł����Ԃ��m�F����֐��B
    {
        return true;
        //Hit�Œʉ߂����I�u�W�F�N�g���Q�Ƃ��A�n�ʂɖ��m�ɐG��Ă��邩�m�F����B
        //Direction�̐��l����ɂ��W�����v�̓�����P�B
        //�ǂƏ���F�m���₷���悤�ɑg�݂Ȃ����B
    }

    bool TargetResarch()
    {
        return true;
        //�ȑO����g���Ă���֐��𗬗p
    }

}
