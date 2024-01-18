using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.Collections.Generic;

[Serializable]
public class NftData
{
    public List<string> ids;
    public string imageUrl;
    public string name;
    public string description;
	public int amount;
}

public class NftInventoryUI : MonoBehaviour
{

	public GameObject loadingNfts;

	public GameObject contentPanel;

	public GameObject nftPanelPrefab;

	private Dictionary<string, NftData> nftDictionary;

	private List<GameObject> instantiatedPrefabs = new List<GameObject>();

	public void Open()
	{
		gameObject.SetActive(true);
		FetchNFTs();
	}

    public async void FetchNFTs() {
        loadingNfts.SetActive(true);

		List<string> accounts = await PassportService.FetchPlayerAccounts();
        List<TokenObject> tokenObjects = await ApiService.GetTokens(accounts[0]);

		nftDictionary = new Dictionary<string, NftData>();
        
        // Iterate through the array and check the token_id
        foreach (var token in tokenObjects)
        {
			if (token.name != null) {
				if (nftDictionary.TryGetValue(token.name, out NftData nftData))
				{
					// Increment the amount for this NftData
					nftData.amount++;
					// Add the current id to the list
					nftData.ids.Add(token.token_id);
				}
				else
				{
					// Create new NftData if not exists
					nftData = new NftData
					{
						ids = new List<string> { token.token_id },
						imageUrl = token.image,
						name = token.name,
						description = token.description,
						amount = 1
					};
					nftDictionary.Add(token.name, nftData);
				}
			}
			else
			{
        		Debug.LogWarning($"Token ID: {token.token_id}, has a null name");			
			}
        }
		loadingNfts.SetActive(false);
		Populate();
    }

	public void Close()
	{
		gameObject.SetActive(false);
		foreach (GameObject prefab in instantiatedPrefabs)
        {
            Destroy(prefab);
        }
        instantiatedPrefabs.Clear();
	}

	public void Populate()
	{

		foreach (NftData nftData in nftDictionary.Values)
		{
			GameObject instantiatedNftPanel = Instantiate(nftPanelPrefab);
			NftPanel prefabScript = instantiatedNftPanel.GetComponent<NftPanel>();
			prefabScript.Populate(nftData.name, nftData.ids, nftData.imageUrl, nftData.amount, nftData.description);
			instantiatedPrefabs.Add(instantiatedNftPanel);
			instantiatedNftPanel.transform.SetParent(contentPanel.transform);
			instantiatedNftPanel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); 
		}
	}
}
