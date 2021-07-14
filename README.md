# Unity Mobile Reference Game with Huawei Mobile Services Plugin Ads Kit 

![image](https://user-images.githubusercontent.com/67346749/125593360-13498f41-6563-4e8f-ad19-cb403638b37c.PNG)

Higgsman project is a pacman game for Unity mobile platform with Huawei Mobile Services Plugin Ads Kit integration.

It is an arcade labyrinth action game developed by Namco. 

Purpose: All dots should be eaten before Ghosts catch you to win the game.

**Ads Features:**

You will be lead to rewarded ad when you lost 3 health to win one more. 

You will be shown banner ad after you lost you all 3 health and skip the rewarded Ad . 

After Game Over you will be shown Interstitial Ad

 HMS Unity plugin used in this project for mobile Advertisement. It helps you integrate different advertisement formats of Huawei Mobile Services in to your Unity game.

* Ad Formats: Interstitial, rewarded videos and banner


## Requirements
Android SDK min 21
Net 4.x

## Important
This plugin supports:
* Unity version 2019, 2020 - Developed in 2.0 Branch
* Unity version 2018 - Developed in 2.0-2018 Branch

**Make sure to download the corresponding unity package for the Unity version you are using from the release section**

## Troubleshooting 1
Please check our [wiki page](https://github.com/EvilMindDevs/hms-unity-plugin/wiki/Troubleshooting)

## Status
This is an ongoing project, currently WIP. Feel free to contact us if you'd like to collaborate and use Github issues for any problems you might encounter. We'd try to answer in no more than a working day.

### Expected soon features
* Native Ads Integration

## Connect your game Huawei Mobile Services in 5 easy steps

1. Register your app at Huawei Developer
2. Import the Plugin to your Unity project
3. Connect your game with the HMS Kit Managers

### 1 - Register your app at Huawei Developer

#### 1.1-  Register at [Huawei Developer](https://developer.huawei.com/consumer/en/)

#### 1.2 - Create an app in AppGallery Connect.
During this step, you will create an app in AppGallery Connect (AGC) of HUAWEI Developer. When creating the app, you will need to enter the app name, app category, default language, and signing certificate fingerprint. After the app has been created, you will be able to obtain the basic configurations for the app, for example, the app ID and the CPID.

1. Sign in to Huawei Developer and click **Console**.
2. Click the HUAWEI AppGallery card and access AppGallery Connect.
3. On the **AppGallery Connect** page, click **My apps**.
4. On the displayed **My apps** page, click **New**.
5. Enter the App name, select App category (Game), and select Default language as needed.
6. Upon successful app creation, the App information page will automatically display. There you can find the App ID and CPID that are assigned by the system to your app.

#### 1.3 Add Package Name
Set the package name of the created application on the AGC.

1. Open the previously created application in AGC application management and select the **Develop TAB** to pop up an entry to manually enter the package name and select **manually enter the package name**.
2. Fill in the application package name in the input box and click save.

> Your package name should end in .huawei in order to release in App Gallery

#### Generate a keystore.

Create a keystore using Unity or Android Tools. make sure your Unity project uses this keystore under the **Build Settings>PlayerSettings>Publishing settings**


#### Generate a signing certificate fingerprint.

During this step, you will need to export the SHA-256 fingerprint by using keytool provided by the JDK and signature file.

1. Open the command window or terminal and access the bin directory where the JDK is installed.
2. Run the keytool command in the bin directory to view the signature file and run the command.

    ``keytool -list -v -keystore D:\Android\WorkSpcae\HmsDemo\app\HmsDemo.jks``
3. Enter the password of the signature file keystore in the information area. The password is the password used to generate the signature file.
4. Obtain the SHA-256 fingerprint from the result. Save for next step.


#### Add fingerprint certificate to AppGallery Connect
During this step, you will configure the generated SHA-256 fingerprint in AppGallery Connect.

1. In AppGallery Connect, click the app that you have created and go to **Develop> Overview**
2. Go to the App information section and enter the SHA-256 fingerprint that you generated earlier.
3. Click √ to save the fingerprint.

____

### 2 - Import the plugin to your Unity Project

To import the plugin:

1. Download the [.unitypackage](https://github.com/EvilMindDevs/hms-unity-plugin/releases)
2. Open your game in Unity
3. Choose Assets> Import Package> Custom
![Import Package](http://evil-mind.com/huawei/images/importCustomPackage.png "Import package")
4. In the file explorer select the downloaded HMS Unity plugin. The Import Unity Package dialog box will appear, with all the items in the package pre-checked, ready to install.
![image](https://user-images.githubusercontent.com/6827857/113576269-e8e2ca00-9627-11eb-9948-e905be1078a4.png)
5. Select Import and Unity will deploy the Unity plugin into your Assets Folder
____

### 3 - Update your agconnect-services.json file.

In order for the plugin to work, some kits are in need of agconnect-json file. Please download your latest config file from AGC and import into Assets/StreamingAssets folder.
![image](https://user-images.githubusercontent.com/6827857/113585485-f488bd80-9634-11eb-8b1e-6d0b5e06ecf0.png)
____

### 4 - Connect your game with any HMS Kit

In order for the plugin to work, you need to select the needed kits Huawei > Kit Settings.

In this Ads Kit reference game , i selected the Ads Kit.

![image](https://user-images.githubusercontent.com/67346749/125586300-2c3bf1a1-489d-448c-9f85-6be7021617c9.PNG)

It will automaticaly create the GameObject for you and it has DontDestroyOnLoad implemented so you don't need to worry about reference being lost.

Now you need your game to call the Ads Kit Manager from your game. See below for further instructions.
    

## Ads kit
There is a custom editor in Huawei > Kit Settings > Ads tab.
![image](https://user-images.githubusercontent.com/67346749/125590341-618599f5-d88c-4f5a-a126-1117f2fcafea.png)

You can enable/disable ad types that you want in your project.
Insert your Ad Id into these textboxes in the editor.

If you want to use test ads, you can check UseTestAds checkbox that'll overwrite all ad ids with test ads. 

If you want your banner ad shown as soon as application launch you can check Show Banner on Load* checbox.

Then you can call certain functions such as
```csharp
    HMSAdsKitManager.Instance.ShowBannerAd();
    HMSAdsKitManager.Instance.HideBannerAd();
    HMSAdsKitManager.Instance.ShowInterstitialAd();
    
    HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
    HMSAdsKitManager.Instance.ShowRewardedAd();
    
    public void OnRewarded(Reward reward)
    {
       
    }
```
This reference game project's GameManager script can be observed as an example for ads integration

## Troubleshooting 2
1.If you received package name error , please check your package name on File->Build Settings -> Player Settings -> Other Settings -> Identification

![image](https://user-images.githubusercontent.com/67346749/125590558-e0548c36-b4e9-4510-85bb-5098c7fd9cc9.PNG)

2.If you received min sdk error , 

![image](https://user-images.githubusercontent.com/67346749/125592730-940912c8-f9b4-4f8b-8fe4-b13532342613.PNG)

please set your API level as implied in the **Requirements** section

![image](https://user-images.githubusercontent.com/67346749/125591510-fc1bbd04-b344-4924-83e9-52342a39325e.PNG)

## Special Thanks
Adam Graham's pacman tutorial project used as unity base of this project.You can follow & support him from:

https://github.com/adamgraham

Project code:

https://github.com/zigurous/unity-pacman-tutorial

Changes: Mobile input integration, Best score track integration, HMS Unity Plugin Ads Kit integration


