using System;
using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;

// Google Mob Ads Rewarded Ad 샘플코드 참조
// https://github.com/googleads/googleads-mobile-unity/blob/main/samples/HelloWorld/Assets/Scripts/RewardedAdController.cs
namespace GoogleMobileAds.Sample
{

    // 사용자 스크립트 메뉴에 해당 스크립트 추가
    [AddComponentMenu("GoogleMobileAds/Samples/RewardedAdController")]
    
    public class AdHandler : MonoBehaviour
    {
        public UIManager uiManager;

#if UNITY_ANDROID
        // private const string _adUnitId = "ca-app-pub-3940256099942544/5224354917"; // 테스트 광고 ID
        private const string _adUnitId = "ca-app-pub-7805719777410961/3701164839";
#elif UNITY_IPHONE
        private const string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        private const string _adUnitId = "unused";
#endif

        private RewardedAd _rewardedAd;
        private const int rewardHint = 1;
        private const float _loadDelay = 0.2f;

        // 광고 1개 시작하자마자 로드시켜두기
        void Start(){
            if(_rewardedAd == null){
                StartCoroutine("LoadAd_withDelay");
                // LoadAd();  // 딜레이 없는 버전
            }
        }

        // 광고 로드시 반복적인 요청은 버그를 발생시키기 때문에, 딜레이 추가
        IEnumerator LoadAd_withDelay(){
            yield return new WaitForSeconds(_loadDelay);
            LoadAd();
        }

        // 광고 Load
        public void LoadAd()
        {
            // Clean up the old ad before loading a new one.
            if (_rewardedAd != null)
            {
                DestroyAd();
            }

            // Create our request used to load the ad.
            var adRequest = new AdRequest();;

            // Send the request to load the ad.
            RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                // If the operation failed with a reason.
                if (error != null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                    return;
                }
                // If the operation failed for unknown reasons.
                // This is an unexpected error, please report this bug if it happens.
                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Rewarded load event fired with null ad and null error.");
                    return;
                }

                // The operation completed successfully.
                Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());
                _rewardedAd = ad;

                // Register to ad events to extend functionality.
                RegisterEventHandlers(ad);
            });
        }

        // 광고 재생
        public void ShowAd()
        {
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                Debug.Log("Showing rewarded ad.");
                _rewardedAd.Show((Reward reward) =>
                {
                    Debug.Log(String.Format("Rewarded ad granted a reward: {0} {1}",
                                            reward.Amount,
                                            reward.Type));
                    int userHintCnt = PlayerPrefs.GetInt("userHintCnt");
                    // 
                    PlayerPrefs.SetInt("userHintCnt", userHintCnt + rewardHint);
                    Debug.Log("힌트개수 : after : " + userHintCnt);
                    uiManager.UI_CloseAdPopup();
                    uiManager.UI_OpenHintPopup();
                });
            }
            else
            {
                // 광고가 아직 로드 안된 경우, 알려주기
                uiManager.UI_OpenNoAdPopup();
                Debug.LogError("광고가 로드되지 않았습니다. 네트워크 환경을 확인해주세요");
            }
        }

        // Destroy Ad
        public void DestroyAd()
        {
            if (_rewardedAd != null)
            {
                Debug.Log("Destroying rewarded ad.");
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }
        }

        // Unity Logs the ResponseInfo.
        public void LogResponseInfo()
        {
            if (_rewardedAd != null)
            {
                var responseInfo = _rewardedAd.GetResponseInfo();
                UnityEngine.Debug.Log(responseInfo);
            }
        }

        private void RegisterEventHandlers(RewardedAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Rewarded ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Rewarded ad was clicked.");
            };
            // Raised when the ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Rewarded ad full screen content opened.");
                // 광고 재생 중, 다음 광고 미리 대기시켜두기
                LoadAd();
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Rewarded ad full screen content closed.");
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Rewarded ad failed to open full screen content with error : "
                    + error);
            };
        }
    }
}