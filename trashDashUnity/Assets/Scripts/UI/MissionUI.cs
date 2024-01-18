using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class MissionUI : MonoBehaviour
{
    public RectTransform missionPlace;
    public AssetReference missionEntryPrefab;
    public AssetReference addMissionButtonPrefab;

    //NFT Popup
    public GameObject mintedNFT;

    public RawImage nftImage;

    public TextMeshProUGUI nftName;

    public TextMeshProUGUI nftDescription;

    public TextMeshProUGUI nftId;

    public IEnumerator Open()
    {
        gameObject.SetActive(true);
        mintedNFT.SetActive(false);

        foreach (Transform t in missionPlace)
            Addressables.ReleaseInstance(t.gameObject);

        for(int i = 0; i < 3; ++i)
        {
            if (PlayerData.instance.missions.Count > i)
            {
                AsyncOperationHandle op = missionEntryPrefab.InstantiateAsync();
                yield return op;
                if (op.Result == null || !(op.Result is GameObject))
                {
                    Debug.LogWarning(string.Format("Unable to load mission entry {0}.", missionEntryPrefab.Asset.name));
                    yield break;
                }
                MissionEntry entry = (op.Result as GameObject).GetComponent<MissionEntry>();
                entry.transform.SetParent(missionPlace, false);
                entry.FillWithMission(PlayerData.instance.missions[i], this);
            }
            else
            {
                AsyncOperationHandle op = addMissionButtonPrefab.InstantiateAsync();
                yield return op;
                if (op.Result == null || !(op.Result is GameObject))
                {
                    Debug.LogWarning(string.Format("Unable to load button {0}.", addMissionButtonPrefab.Asset.name));
                    yield break;
                }
                AdsForMission obj = (op.Result as GameObject)?.GetComponent<AdsForMission>();
                obj.missionUI = this;
                obj.transform.SetParent(missionPlace, false);
            }
        }
    }

    public void CallOpen()
    {
        gameObject.SetActive(true);
        StartCoroutine(Open());
    }

    public async void Claim(MissionBase m)
    {
        PlayerData.instance.ClaimMission(m);

        List<string> accounts = await PassportService.FetchPlayerAccounts();
        NftMetadata nftMetadata = await ApiService.MakeMintRequest(accounts[0]);

        Texture2D texture = await ApiService.FetchImage(nftMetadata.image);
        nftName.text = nftMetadata.name;
        nftDescription.text = nftMetadata.description;
        nftId.text = "Token ID: " + nftMetadata.token_id;
        nftImage.texture = texture;
        mintedNFT.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void CloseNft() {
        StartCoroutine(Open());
        mintedNFT.SetActive(false);
    }
}
