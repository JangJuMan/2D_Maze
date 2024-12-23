using System;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Threading.Tasks;

// Google Mob Ads Rewarded Ad 샘플코드 참조
// https://github.com/googleads/googleads-mobile-unity/blob/main/samples/HelloWorld/Assets/Scripts/RewardedAdController.cs
namespace GoogleMobileAds.Sample
{

    // 사용자 스크립트 메뉴에 해당 스크립트 추가
    [AddComponentMenu("GoogleMobileAds/Samples/RewardedAdController")]
    
    public class AdHandler : MonoBehaviour
    {
        // 로딩 표현용 circle Spinner
        public GameObject AdLoadedStatus;
        public UIManager uiManager;

        // 테스트용 유닛 ID
#if UNITY_ANDROID
        private const string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        private const string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        private const string _adUnitId = "unused";
#endif

        private RewardedAd _rewardedAd;
        private const int rewardHint = 2;

        // 광고 1개 시작하자마자 로드시켜두기
        void Awake(){
            if(_rewardedAd == null){
                LoadAd();
                // 스피너 사용 X
                // AdLoadedStatus?.SetActive(false);
                // Debug.Log("스피너 비활성화");
            }
        }

        // 광고 Load
        public void LoadAd()
        {
            // 스피너 사용 X
            // 광고 로드중 표시
            // AdLoadedStatus?.SetActive(true);
            Debug.Log("광고 로드 시작 / 스피너 활성화");

            // Clean up the old ad before loading a new one.
            if (_rewardedAd != null)
            {
                DestroyAd();
            }

            // Debug.Log("Loading rewarded ad.");

            // Create our request used to load the ad.
            var adRequest = new AdRequest();
            
            // 디바이스 테스트광고(되는건가...?)
            // RequestConfiguration requestConfiguration = new RequestConfiguration();
            // requestConfiguration.TestDeviceIds.Add("81df12fe-3378-45de-a947-b2edb0db7e37");
            // MobileAds.SetRequestConfiguration(requestConfiguration);
            
            // Debug.Log("테스트 광고 추가");

            // Send the request to load the ad.
            RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                Debug.Log("광고 콜백함수시작");

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

                // 스피너 사용 X
                // 광고 로드 완료됨을 표시
                // AdLoadedStatus?.SetActive(false);
                Debug.Log("광고 콜백함수끝 / 스피너 비활성화");
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
                    int userHintCnt = PlayerPrefs.GetInt("userHintCnt", 0);
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
                Debug.LogError("Rewarded ad is not ready yet.");
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