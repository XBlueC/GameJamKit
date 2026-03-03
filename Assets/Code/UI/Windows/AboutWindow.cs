using System;
using System.Collections.Generic;
using Code.Core.UI;
using Code.Core.Utils;
using Code.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows
{
    [Serializable]
    public class StaffMember
    {
        public string name;
        public string role;
    }

    public class AboutWindow : UIBase
    {
        public Button closeBtn;
        public List<StaffMember> members;
        public StaffMemberPrefab memberPrefab;
        public Transform memberParent;

        public override void OnInit()
        {
            closeBtn.onClick.AddListener(Close);
        }

        public override void OnShow(UIArgs args)
        {
            ListHelper.Shuffle(members);
            foreach (var member in members)
            {
                var go = Instantiate(memberPrefab, memberParent);
                go.gameObject.SetActive(true);
                go.memberName.text = member.name;
                go.role.text = member.role;
            }
        }
    }
}