using System;
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

        // 광고 Load
        public void LoadAd()
        {
            // Clean up the old ad before loading a new one.
            if (_rewardedAd != null)
            {
                DestroyAd();
            }

            // Debug.Log("Loading rewarded ad.");

            // Create our request used to load the ad.
            var adRequest = new AdRequest();

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

                // Inform the UI that the ad is ready.
                // AdLoadedStatus가 null이 아니라면 SetActive(true)(null 이라면 null로 처리)
                AdLoadedStatus?.SetActive(true);
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
                    PlayerPrefs.SetInt("userHintCnt", userHintCnt + 1);
                    Debug.Log("힌트개수 : after : " + userHintCnt);
                    uiManager.UI_CloseAdPopup();
                    uiManager.UI_OpenHintPopup();
                });
            }
            else
            {
                Debug.LogError("Rewarded ad is not ready yet.");
            }

            // Inform the UI that the ad is not ready.
            AdLoadedStatus?.SetActive(false);
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

            // Inform the UI that the ad is not ready.
            AdLoadedStatus?.SetActive(false);
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