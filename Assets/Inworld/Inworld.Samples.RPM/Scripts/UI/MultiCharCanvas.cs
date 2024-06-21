/*************************************************************************************************
 * Copyright 2022-2024 Theai, Inc. dba Inworld AI
 *
 * Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
 * that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
 *************************************************************************************************/

using UnityEngine;
using Inworld.Entities;

namespace Inworld.Sample.RPM
{
    public class MultiCharCanvas : DemoCanvas
    {
        const string k_Instruction = "Press <color=green>\"Tab\"</color> to switch character selection method.\n";
        const string k_SelectByKey = "Press <color=green>\"1\"</color> and <color=green>\"2\"</color> to switch interact characters.\nPress <color=green>\"0\"</color> to broadcast.\n";
        const string k_SelectBySight = "Automatically select characters by sight and angle.\n";
        const string k_AutoChat = "The characters are chatting automatically.\n";
        const string k_SelectCharacter = "Please select characters\n";
        const string k_GroupChat = "Now <color=green>BroadCasting</color>";
        string m_CurrentMethod;
        string m_CharacterIndicator = k_GroupChat;
        
        protected override void OnSelectingModeUpdated(CharSelectingMethod method)
        {
            if (method == CharSelectingMethod.AutoChat)
                InworldController.Instance.SendTrigger(InworldMessenger.NextTurn); 
        }
        protected override void OnCharacterSelected(string newCharacter)
        {
            InworldCharacter character = InworldController.CharacterHandler.GetCharacterByBrainName(newCharacter);
            m_CharacterIndicator = character ? $"Now Talking to <color=green>{character.name}</color>" : k_GroupChat;
        }
        protected override void OnCharacterDeselected(string newCharacter)
        {
            m_CharacterIndicator = InworldController.Client.EnableGroupChat && InworldController.CharacterHandler.CurrentCharacters.Count > 0 ? k_GroupChat : k_SelectCharacter;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            m_CurrentMethod = _GetCurrentInstruction();
        }
        void Update()
        {
            m_Content.text = $"{k_Instruction}{m_CurrentMethod}{m_CharacterIndicator}";
            if (!Input.GetKeyUp(KeyCode.Tab))
                return;
            InworldController.CharacterHandler.ChangeSelectingMethod();
            m_CurrentMethod = _GetCurrentInstruction();
        }
        string _GetCurrentInstruction()
        {
            switch (InworldController.CharacterHandler.SelectingMethod)
            {
                case CharSelectingMethod.KeyCode:
                    return k_SelectByKey;
                case CharSelectingMethod.SightAngle:
                    return k_SelectBySight;
                case CharSelectingMethod.AutoChat:
                    return k_AutoChat;
            }
            return "";
        }
        public void NextCharacterSpeaking(InworldCharacter character) 
        {
            // TODO(YAN): When opening chat panel, it'll set to manual.
            //            Let's figure out a better way to handle both situations.
            if (InworldController.CharacterHandler.SelectingMethod != CharSelectingMethod.AutoChat && InworldController.CharacterHandler.SelectingMethod != CharSelectingMethod.Manual)
                return;
            if (string.IsNullOrEmpty(character.ID))
                return;
            // YAN: We may support nominate the next character in future, but now only automatically selecting is supported.
            InworldController.Instance.SendTrigger(InworldMessenger.NextTurn); 
        }
    }
}
