using System;
using UnityEngine;

[Serializable]
public class AttachmentBulderList
{
    [field: SerializeField] public AttachmentBulderConfig attachmentBulderConfig { get; private set; }
    [field: SerializeField] public Transform position { get; private set; }
}