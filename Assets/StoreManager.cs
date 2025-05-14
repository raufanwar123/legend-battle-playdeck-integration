//using System;
//using UnityEngine;
//using UnityEngine.Purchasing;

//public class StoreManager : MonoBehaviour, IStoreListener
//{
//    private static IStoreController storeController;
//    private static IExtensionProvider extensionProvider;

//    // Replace "your_product_id" with the actual product ID
//    private static string productID = "com.unknown.legends.free.fire.battleground.survival.coins50000";
//    private static string productID1 = "com.unknown.legends.free.fire.battleground.survival.coins100000";
//    private static string productID2 = "com.unknown.legends.free.fire.battleground.survival.removead";

//    void Start()
//    {
//        InitializePurchasing();
//    }

//    public void InitializePurchasing()
//    {
//        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

//        // Add your product ID to the builder
//        builder.AddProduct(productID, ProductType.Consumable);
//        builder.AddProduct(productID1, ProductType.Consumable);
//        builder.AddProduct(productID2, ProductType.NonConsumable);

//        UnityPurchasing.Initialize(this, builder);
//    }

//    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//    {
//        storeController = controller;
//        extensionProvider = extensions;
//    }

//    public void OnInitializeFailed(InitializationFailureReason error)
//    {
//        Debug.Log("Initialization failed: " + error);
//    }

//    public void OnInitializeFailed(InitializationFailureReason error, string message)
//    {
//        Debug.LogError("Initialization failed: " + error + ", " + message);
//    }

//    public void BuyCoins50000()
//    {
//        if (storeController != null)
//        {
//            Product product = storeController.products.WithID(productID);

//            if (product != null && product.availableToPurchase)
//            {
//                storeController.InitiatePurchase(product);
//            }
//            else
//            {
//                Debug.Log("Product not available for purchase.");
//            }
//        }
//        else
//        {
//            Debug.Log("Store controller is not initialized.");
//        }
//    }
    
//    public void BuyCoins100000()
//    {
//        if (storeController != null)
//        {
//            Product product = storeController.products.WithID(productID1);

//            if (product != null && product.availableToPurchase)
//            {
//                storeController.InitiatePurchase(product);
//            }
//            else
//            {
//                Debug.Log("Product not available for purchase.");
//            }
//        }
//        else
//        {
//            Debug.Log("Store controller is not initialized.");
//        }
//    }
    
//    public void RemoveAds()
//    {
//        if (storeController != null)
//        {
//            Product product = storeController.products.WithID(productID2);

//            if (product != null && product.availableToPurchase)
//            {
//                storeController.InitiatePurchase(product);
//            }
//            else
//            {
//                Debug.Log("Product not available for purchase.");
//            }
//        }
//        else
//        {
//            Debug.Log("Store controller is not initialized.");
//        }
//    }

//    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//    {

//        if (String.Equals(args.purchasedProduct.definition.id, productID, StringComparison.Ordinal))
//        {
//            GameConfiguration.setTotalCash(GameConfiguration.getTotalCash() + 50000);
//            MainMenuController.instance.UpdateCash();
//            Debug.Log("Give 50000");
//           // WeaponSelection.Instance.OpenallGuns();
//        }
//       else if (String.Equals(args.purchasedProduct.definition.id, productID1, StringComparison.Ordinal))
//        {
//            GameConfiguration.setTotalCash(GameConfiguration.getTotalCash() + 100000);
//            MainMenuController.instance.UpdateCash();
//            Debug.Log("Give 100000");
//            // WeaponSelection.Instance.OpenallGuns();
//        }

//        else if (String.Equals(args.purchasedProduct.definition.id, productID2, StringComparison.Ordinal))
//        {
//            Remove_Ads();
//            Debug.Log("Remove Ads");
//            // WeaponSelection.Instance.OpenallGuns();
//        }
//        else
//        {
//            Debug.Log("Purchase Failed");
//        }

//        Debug.Log("Purchase successful: " + args.purchasedProduct.definition.id);
//        // Add your logic for handling the successful purchase here

//        return PurchaseProcessingResult.Complete;
//    }

//    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//    {
//        Debug.Log("Purchase failed: " + failureReason);
//        // Add your logic for handling the failed purchase here
//    }


//    public void Remove_Ads()
//    {
//        PlayerPrefs.SetInt("adsvalue", 1);
//    }
//}