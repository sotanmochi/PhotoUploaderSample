﻿using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using Amazon.S3;

namespace PhotoUploader
{
    public class PhotoUploadPresenter : MonoBehaviour
    {
        [SerializeField] private AWSConfigs _AWSConfigs;
        [SerializeField] private string _BucketName;

        [SerializeField] private RawImage _Qrcode;
        [SerializeField] private GameObject _QRCodeUIRoot;
        [SerializeField] private RawImage _PhotoPreview;
        [SerializeField] private GameObject _PhotoPreviewUIRoot;
        [SerializeField] private Button _Button_SaveImage;
        [SerializeField] private Button _Button_SaveCancel;
        [SerializeField] private Button _Button_QRCodeBack;

        private bool _IsPreview;

        private S3Storage _StorageClient;

        void Start()
        {
            _StorageClient = new S3Storage(_AWSConfigs);
            _IsPreview = false;
            _Button_SaveImage.onClick.AddListener(OnClickSaveImage);
            _Button_SaveCancel.onClick.AddListener(OnClickSaveCancel);
            _Button_QRCodeBack.onClick.AddListener(OnClickQRCodeBack);
        }

        void Update()
        {
            if (!_IsPreview)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        Debug.Log("*** Touch ***");
                        TakePhoto();
                    }
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("*** MouseButtonDown ***");
                    TakePhoto();
                }
            }
        }

        private void TakePhoto()
        {
            StartCoroutine(TakePhotoCoroutine());
        }

        IEnumerator TakePhotoCoroutine()
        {
            _IsPreview = true;

            yield return new WaitForEndOfFrame();

            // Read pixels from screen(currently active RenderTexture) into the saved texture data.
            Texture2D preview = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false, false);
            preview.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            preview.Apply();

            _PhotoPreview.texture = preview;
            _PhotoPreviewUIRoot.SetActive(true);
        }

        private async void OnClickSaveImage()
        {
            Texture2D tex2d = _PhotoPreview.texture as Texture2D;
            byte[] pngData = tex2d.EncodeToPNG();
            MemoryStream memoryStream = new MemoryStream(pngData);

            string dateTimeStr = DateTime.Now.ToString("yyyyMMddHHmmss");
            string filename = "Photo_" + dateTimeStr + ".png";

            var response = await _StorageClient.PubObjectAsync(_BucketName, filename, memoryStream, S3CannedACL.PublicRead);
            
            Debug.Log("Response: " + response.HttpStatusCode);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                string url = _StorageClient.GetPublicUrl(_BucketName, filename);
                _Qrcode.texture = QRCodeGenerator.GenerateQRCodeTexture(url, 256, 256);

                _PhotoPreviewUIRoot.SetActive(false);
                _QRCodeUIRoot.SetActive(true);
            }
            else
            {
                Debug.LogError("Response: " + response.HttpStatusCode);
            }
        }

        private void OnClickSaveCancel()
        {
            _IsPreview = false;
            _PhotoPreviewUIRoot.SetActive(false);
            _QRCodeUIRoot.SetActive(false);
        }

        private void OnClickQRCodeBack()
        {
            _IsPreview = false;
            _PhotoPreviewUIRoot.SetActive(false);
            _QRCodeUIRoot.SetActive(false);
        }
    }
}
