using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//공통 인터페이스: 모든 아이템 효과 스크립트는 이 인터페이스를 구현합니다.
public interface IUsableItem
{
    void UsePrimary(Transform transform);      // 기본 사용 (좌클릭 등)
    void UseSecondary(Transform transform);    // 보조 사용 (우클릭 등)

}
