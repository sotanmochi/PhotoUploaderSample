using System;
using UnityEngine;

namespace PhotoUploader
{
    [Serializable]
    [CreateAssetMenu(menuName = "AWS for Unity/Create Configs", fileName = "AWSConfigs")]
    public class AWSConfigs : ScriptableObject
    {
        public string S3Region;
        public string CognitoPoolRegion;
        public string IdentityPoolId;
        public string UserPoolId;
        public string ClientId;
        public string ClientSecret;
    }
}
