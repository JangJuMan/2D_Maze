using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupHandler : MonoBehaviour
{
    public GameObject popupBlocker;
    public float maxScale = 1.1f;
    public float minScale = 0.2f;
    public float originScale = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        DOTween.Init();

        // transform 의 scale값을 모두 0.1f 로 변경 
        transform.localScale = Vector3.one * 0.1f;

        // 객체 비활성화
        gameObject.SetActive(false);
    }

    public void Show(){
        // 팝업 활성화
        gameObject.SetActive(true);

        // DOTween 함수를 차례대로 수행하도록 함
        // var : 컴파일러가 자동으로 형식 결정
        var seq = DOTween.Sequence().SetUpdate(true);

        // DOScale 의 첫 번째 파라미터는 목표 scale 값, 두번째 시간
        // 최대로 커졌다가, 원래 팝업창 사이즈로 줄어드는 애니메이션 예약
        seq.Append(transform.DOScale(maxScale, 0.2f));
        seq.Append(transform.DOScale(originScale, 0.1f));

        // 애니메이션 플레이
        seq.Play();

        // 팝업 블로커 활성화
        popupBlocker.SetActive(true);
    }

    public void Hide(){
        // 시퀀스 생성
        var seq = DOTween.Sequence().SetUpdate(true);

        // 팝업창 최소화
        transform.localScale = Vector3.one * minScale;

        // 시퀀스 예약(사이즈, duration)
        seq.Append(transform.DOScale(maxScale, 0.1f));
        seq.Append(transform.DOScale(minScale, 0.2f));

        // OnComplete는 seq에 설정한 애니메이션의 플레이가 완료되면
        // {} 안에 있는 코드가 수행됨을 의미
        // 여기서는 닫기 애니메이션이 완료된 후 객체를 비활성화한다.
        // 객체 비활성화가 완료된 후, 팝업 블로커 비활성화
        seq.Play().OnComplete(() => {
            gameObject.SetActive(false);
            popupBlocker.SetActive(false);
        });
    }
}
