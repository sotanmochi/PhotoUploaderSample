# Photo Uploader Sample

## License
このプロジェクトは、サードパーティのアセットを除き、CC0 Public Domainでライセンスされています。  
This project is licensed under CC0 Public Domain except for third party assets.

## Tips
- iOS向けにビルドして実行するためには、必要なライブラリをlink.xmlに記載してAssetsディレクトリ直下に配置する必要がある  
参考：[AWS SDK for .NETを使うUnityアプリをiOS/Android向けにビルドする](https://dev.classmethod.jp/client-side/unity-client-side/unity-app-build-for-ios-android/)

- Texture2D.ReadPixelsを使って、スクリーン画面からテクスチャデータへと保存するためのピクセルデータを読み込む。  
描画が完了してからでないとエラーが出るので、コルーチンで yield return new WaitForEndOfFrame() の後に処理する。  
参考：[Unityで画面のスクリーンショットを撮る(Application.CaptureScreenshotじゃない方法) - Qiita](https://qiita.com/tempura/items/e8f4bbb4419407916d12)

## Third party assets
- AWS SDK for .NET (Apache License 2.0)
- [ZXing.Net](https://github.com/micjahn/ZXing.Net)(Apache License 2.0)
