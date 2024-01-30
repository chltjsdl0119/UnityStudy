using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ����� �̺�Ʈ OnValueChanged�� �����ϴ� ������ ���� ��ũ��Ʈ�Դϴ�.
/// </summary>
public class ToggleListener : MonoBehaviour
{
    public Toggle toggle; //������ ������ ���


    void Start()
    {
        //onValueChanged�� ��� ���� ����� �� ����Ǵ� �ݹ� ����Դϴ�.
        //����ڰ� ����� Ȱ��ȭ�ϰų� ��Ȱ��ȭ�ϴ� �ñ⸦ ������ �̺�Ʈ�� ó���մϴ�.
        //AddListener�� ���� ��ũ��Ʈ �󿡼� ó���ϴ� ���, ��ۿ� ���� ������ ���� �Լ���
        //�븮��(delegate)�� ���ؼ� �߰��մϴ�.

        //delegate : �Լ��� ����(������ ��ȯ Ÿ�԰� �Ű�����)�� ���� ���¿� ���ؼ�
        //��� ȣ���� �������ִ� C#�� ����Դϴ�.

        //��� ���� : ������ ����� ������ �Լ��� �� �������ִ� �ͺ���,
        //���¿� ���缭 ���� �� �ְ� ���ִ°� �� �����ϱ� ����


        toggle.onValueChanged.AddListener(delegate { ToggleDebug(toggle); });
        toggle.onValueChanged.AddListener(ToggleDebugBool);

    }

    void ToggleDebugBool(bool isOn)
    {
        if (isOn)
            Debug.Log("��� �����(bool)");   
    }

    //����� ������ �޾� ������ �Լ�
    void ToggleDebug(Toggle change)
    {
        Debug.Log("��� �����(delegate)");
    }
}
