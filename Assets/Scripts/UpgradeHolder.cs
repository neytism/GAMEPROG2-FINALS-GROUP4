using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHolder : MonoBehaviour
{
    [SerializeField] private Sprite[] _icons;
    [SerializeField] private Upgrades[] _upgrades;
}

[Serializable] public class UpgradeSelectionBox
{
    public Button _button;
    public TextMeshProUGUI _textBox;
    public Image _iconHolder;
}