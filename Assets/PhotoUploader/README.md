# Photo Uploader Sample

ARアプリで撮影した写真をAmazon S3にアップロードして画像のURLをQRコード表示するサンプル

## License
このプロジェクトは、サードパーティのアセットを除き、CC0 Public Domainでライセンスされています。  
This project is licensed under CC0 Public Domain except for third party assets.

## Third party assets
- AWS SDK for .NET (Apache License 2.0)
- [ZXing.Net](https://github.com/micjahn/ZXing.Net)(Apache License 2.0)

## サンプルを動かすまでの流れ
### AWSの準備
 - S3のバケット作成
 - CognitoのIDプール作成
 - IAMのポリシー作成・設定

### Unityプロジェクトの準備
- AWSConfigs作成
- AWSConfigsとバケット名の設定

### Unityアプリの実行
- UnityEditorで実行 or ビルドしてAndroid/iOS端末で実行

## Tips
- 画像アップロード先のバケットに対して、オブジェクトをアップロードする操作（PubObject）とアクセス権を設定する操作（PutObjectACL）を許可するポリシーが必要

- iOS向けにビルドして実行するためには、必要なライブラリをlink.xmlに記載してAssetsディレクトリ直下に配置する必要がある  
参考：[AWS SDK for .NETを使うUnityアプリをiOS/Android向けにビルドする](https://dev.classmethod.jp/client-side/unity-client-side/unity-app-build-for-ios-android/)

- Texture2D.ReadPixelsを使って、スクリーン画面からテクスチャデータへと保存するためのピクセルデータを読み込む。  
描画が完了してからでないとエラーが出るので、コルーチンで yield return new WaitForEndOfFrame() の後に処理する。  
参考：[Unityで画面のスクリーンショットを撮る(Application.CaptureScreenshotじゃない方法) - Qiita](https://qiita.com/tempura/items/e8f4bbb4419407916d12)
