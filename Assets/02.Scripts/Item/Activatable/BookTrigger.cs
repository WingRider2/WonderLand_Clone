using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTrigger : MonoBehaviour
{
    public Transform books;
    public bool isFirst;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && !isFirst)
        {

            // [추가] 책 넘기는 효과음 재생
            SoundManager.Instance.PlaySFX(SoundType.bookSound);
            books.GetComponent<MovePlafoms>().movePlafom();
            isFirst = true;
        }
    }
}
