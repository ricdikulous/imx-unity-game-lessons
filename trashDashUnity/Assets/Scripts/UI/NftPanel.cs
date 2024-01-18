using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class NftPanel : MonoBehaviour
{
    // Public TextMeshPro variables that can be set dynamically
    public TextMeshProUGUI name;
    public RawImage image;

    public Button craftButton;
    public TextMeshProUGUI quantity;

    public TextMeshProUGUI tokenIds;
    public TextMeshProUGUI description;

    public void Populate(string nftName, List<string> ids, string imageUrl, int nftQuantity, string nftDescription)
    {
        name.text = nftName;
        quantity.text = "You own: " + nftQuantity;
        tokenIds.text = string.Join(", ", ids);
        description.text = nftDescription;
        if (nftQuantity > 1) {
            craftButton.gameObject.SetActive(true);
        } else {
            craftButton.gameObject.SetActive(false);
        }
        FetchImage(imageUrl);
    }

    private async void FetchImage(string imageUrl)
    {
        Texture2D texture = await ApiService.FetchImage(imageUrl);
        image.texture = texture;
    }
}
