using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachmentList : MonoBehaviour
{
    [field: SerializeField] public Image MainImage;
    [field: SerializeField] public Image CanBeAttachImage;
    [field: SerializeField] public List<AttachmentImage> AttachmentImages;
    [field: SerializeField] public AttachType[] Types { get; private set; }
    [field: SerializeField] public RayAttachments RayAttachments { get; private set; }
    [field: SerializeField] public Transform Center { get; private set; }
    
    public AttachType attachType;
}