using System;
using UnityEngine;

namespace Models.Audio {
    public enum SoundId {
        Restart,
        Complete,
        Coin
    }
    
    [Serializable]
    public class AudioData {
        public SoundId Ident;
        public AudioClip Source;
    }
}