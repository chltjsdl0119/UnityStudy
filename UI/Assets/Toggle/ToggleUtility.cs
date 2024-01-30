using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUtility : MonoBehaviour
{
    public GameObject cube;

    public void SetColor(bool isOn)
    {
        if(isOn)
            cube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            //������ ����� �޾ƿͼ� ��Ƽ������ ������ ���� �÷��� �����մϴ�.
        else//üũ ��Ȱ��ȭ �ÿ��� �ʱ� ��(�Ͼ��)���� ����
            cube.GetComponent<Renderer>().material.color = Color.white;
    }
   
    public void ObjectRotate(bool isOn)
    {
        if(isOn)
        {
            cube.transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        }
    }

    public void OnDebug(bool isOn)
    {
        if (isOn)
            Debug.Log("디버깅 테스트");
        else
            Debug.Log("기능 비활성화");
    }
        
}
