using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타겟 정보
/// </summary>
[CreateAssetMenu(menuName = "Category", fileName = "Category_")]
public class Category : ScriptableObject, IEquatable<Category>
// IEquatable<T> 인터페이스  구조체 또는 클래스를 만들 때, 클래스 간의 비교를 진행하고자 할 때, 연결해주는 인터페이스이다. Equals 메소드를 가지고 있다.
// 사용 목적 : 제네릭 컬렉션에서 사용되는 요소 비교
{
    [SerializeField] private string id;
    [SerializeField] private string ingame_name;

    public string Id => id;
    public string Ingame_name => ingame_name;

    public bool Equals(Category other)
    {
        // 다른 값이 비어있다면 false
        if (other is null)
        {
            return false;
        }

        // reference를 비교했을 때, 상대방과 내가 일치한다면 true
        if (ReferenceEquals(other, this))
        {
            return true;
        }

        // 데이터 형태가 다를 경우에는 false
        if (GetType() != other.GetType())
        {
            return false;
        }

        // 일반적인 경우에는 id가 같은지에 대한 여부를 체크한다.
        return id == other.id;
    }

    public override int GetHashCode() => (id, ingame_name).GetHashCode();

    public override bool Equals(object obj) => base.Equals(obj);
    
    // 연산자 오버로딩 : 클래스 간의 비교 연산자에 대한 수정(연선자 오버로딩)
    public static bool operator ==(Category category, string id)
    {
        if (category is null)
        {
            return ReferenceEquals(id, null);
        }

        return category.id == id || category.ingame_name == id;
    }

    public static bool operator !=(Category category, string id) => !(category == id);
}
