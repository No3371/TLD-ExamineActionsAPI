using Il2Cpp;
using Il2CppAK.Wwise;
using Il2CppTLD.Gear;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    public class DefaultPanel : IExamineActionPanel
    {
		internal GameObject repairPanelClone/*, toolSelectionClone*/, materialsClone, yieldsClone, chanceClone, durationClone, buttonClone, buttonRoot;
		internal List<HarvestRepairMaterial> Materials, Products;
		internal List<UILabel> materialChances, productChances;
		internal InfoBlock info1, info2, infoChance, infoDuration;
		internal UISprite bg1, bg2;
		Vector2 info1Pos, info2Pos;
		internal UIButton buttonContinue, buttonSubLeft, buttonSubRight;
		internal UILabel buttonLabel, materiaslLabel, requiresToolLabel, consumeLabel, stopLabel;
        public Panel_Inventory_Examine PIE { get; }
		bool infoBlockExtended;

        public DefaultPanel(Panel_Inventory_Examine pie)
        {
			repairPanelClone = GameObject.Instantiate(pie.m_RepairPanel, pie.m_RepairPanel.transform.parent);
            Transform gameobjects = FindChildWrapper(repairPanelClone.transform, "GameObject");
			// toolSelectionClone = GameObject.Instantiate(pie.m_ActionToolSelect, pie.m_ActionToolSelect.transform.parent);
			chanceClone = FindChildWrapper(repairPanelClone.transform, "GameObject/ChanceLabels").gameObject;
            Transform origAmountLabel = FindChildWrapper(repairPanelClone.transform, "GameObject/AmountLabels");
			durationClone = FindChildWrapper(repairPanelClone.transform, "GameObject/TimeLabels").gameObject;
            Transform origSkillLabel = FindChildWrapper(repairPanelClone.transform, "GameObject/SkillLabels");

			info1 = new InfoBlock(GameObject.Instantiate(origAmountLabel.gameObject, gameobjects));
			info1.go.transform.localPosition = info1Pos = durationClone.transform.localPosition + new Vector3(0, 24);
			info2 = new InfoBlock(GameObject.Instantiate(origAmountLabel.gameObject, gameobjects));
			info2.go.transform.localPosition = info2Pos = chanceClone.transform.localPosition + new Vector3(0, 24);
			info1.go.SetActive(false);
			info2.go.SetActive(false);

			bg1 = gameobjects.transform.FindChild("BG (1)").GetComponent<UISprite>();
			bg2 = gameobjects.transform.FindChild("BG (2)").GetComponent<UISprite>();
			bg2.transform.localPosition = new (bg2.transform.localPosition.x, bg2.transform.localPosition.y - 70, bg2.transform.localPosition.z);
			bg2.height += 80;
			SetInfoBlock1Row();
			SetInfoBlock2Rows();

			infoChance = new InfoBlock(chanceClone, chanceClone.transform.FindChild("Header").GetComponent<UILabel>(), chanceClone.transform.FindChild("Repair_ChanceLabel_Value").GetComponent<UILabel>());
			infoDuration = new InfoBlock(durationClone, durationClone.transform.FindChild("Header").GetComponent<UILabel>(), durationClone.transform.FindChild("Repair_TimeLabel_Value").GetComponent<UILabel>());

			infoDuration.label1.text = "Time Needed";

            chanceClone.transform.localPosition = new(chanceClone.transform.localPosition.x, 124, chanceClone.transform.localPosition.z);
            durationClone.transform.localPosition = new(durationClone.transform.localPosition.x, 124, durationClone.transform.localPosition.z);
					// MelonLogger.Msg($"1");

            // skillClone = GameObject.Instantiate(__instance.m_ReadPanel.transform.FindChild("GameObject/Skill").gameObject, gameobjects);
            // skillClone.transform.localPosition = new Vector3(skillClone.transform.localPosition.x, 115, skillClone.transform.localPosition.z);
            // skillClone.transform.FindChild("ProgressBar").gameObject.SetActive(false);

            materialsClone = FindChildWrapper(repairPanelClone.transform, "GameObject/RequiredMaterials").gameObject;
			Materials = new (3);
            Materials.AddRange(materialsClone.GetComponentsInChildren<HarvestRepairMaterial>(true));
			Materials.Add(GameObject.Instantiate(Materials[0], Materials[0].transform.parent).GetComponent<HarvestRepairMaterial>());
			Materials.Add(GameObject.Instantiate(Materials[0], Materials[0].transform.parent).GetComponent<HarvestRepairMaterial>());
			Materials.Add(GameObject.Instantiate(Materials[0], Materials[0].transform.parent).GetComponent<HarvestRepairMaterial>());
					// MelonLogger.Msg($"2: {__instance.m_HarvestYieldRoot}");

			yieldsClone = GameObject.Instantiate(pie.m_HarvestYieldRoot, gameobjects);
			Products = new (3);
            Products.AddRange(yieldsClone.GetComponentsInChildren<HarvestRepairMaterial>(true));
			Products.Add(GameObject.Instantiate(Products[0], Products[0].transform.parent).GetComponent<HarvestRepairMaterial>());
			Products.Add(GameObject.Instantiate(Products[0], Products[0].transform.parent).GetComponent<HarvestRepairMaterial>());
			Products.Add(GameObject.Instantiate(Products[0], Products[0].transform.parent).GetComponent<HarvestRepairMaterial>());

			materialChances = new (Materials.Count);
			for (int i = 0; i < Materials.Count; i++)
			{
				materialChances.Add(GameObject.Instantiate(Materials[0].m_StackLabel, Materials[i].transform));
				materialChances[i].transform.localPosition = new Vector2(40, -26);
				materialChances[i].color = new Color(0.9044f, 0.6985f, 0.3059f);
				Materials[i].m_StackLabel.transform.localPosition = new Vector2(40, -40);
			}

			productChances = new (Products.Count);
			for (int i = 0; i < Products.Count; i++)
			{
				productChances.Add(GameObject.Instantiate(Products[0].m_StackLabel, Products[i].transform));
				productChances[i].transform.localPosition = new Vector2(40, -26);
				productChances[i].color = new Color(0.9044f, 0.6985f, 0.3059f);
				Products[i].m_StackLabel.transform.localPosition = new Vector2(40, -40);
			}

            // pie.m_ToolScrollList.CreateFromList
            GameObject.Destroy(origAmountLabel.gameObject);
            GameObject.Destroy(origSkillLabel.gameObject);
            // MelonLogger.Msg($"3");

            buttonRoot = FindChildWrapper(repairPanelClone.transform, "RepairPanel_Buttons").gameObject;
            buttonRoot.transform.localPosition = new (buttonRoot.transform.localPosition.x, buttonRoot.transform.localPosition.y - 50, buttonRoot.transform.localPosition.z);
            buttonRoot.GetComponentInChildren<UIControllerScheme>().enabled = false;
			buttonRoot.gameObject.SetActive(true);
            buttonClone = FindChildWrapper(repairPanelClone.transform, "RepairPanel_Buttons/Mouse/ButtonRepair").gameObject;
			foreach (var b in buttonClone.GetComponents<UIButton>())
			{
				if (b.onClick.Count == 0) continue;
				b.onClick.Clear();
				EventDelegate.Add(b.onClick, new System.Action(ExamineActionsAPI.Instance.OnPerformSelectedAction));

				buttonContinue = b;
				buttonLabel = b.GetComponentInChildren<UILabel>();
				MonoBehaviour.Destroy(buttonLabel.GetComponent<UILocalize>());
			}

			requiresToolLabel = FindChildWrapper(repairPanelClone.transform, "RequiresToolLabel").GetComponent<UILabel>();
			materiaslLabel = FindChildWrapper(materialsClone.transform, "Label_RequiredMaterials").GetComponent<UILabel>();

            buttonSubLeft = GameObject.Instantiate(pie.m_Button_ToolDecrease, buttonClone.transform.parent).GetComponent<UIButton>();
            buttonSubRight = GameObject.Instantiate(pie.m_Button_ToolIncrease, buttonClone.transform.parent).GetComponent<UIButton>();
            buttonSubLeft.transform.localPosition = buttonClone.transform.localPosition + new Vector3(-160, 0);
            buttonSubRight.transform.localPosition = buttonClone.transform.localPosition + new Vector3(160, 0);
            buttonSubLeft.onClick[0] = new EventDelegate(new System.Action(ExamineActionsAPI.Instance.OnPreviousSubAction));
            buttonSubRight.onClick[0] = new EventDelegate(new System.Action(ExamineActionsAPI.Instance.OnNextSubAction));

            consumeLabel = GameObject.Instantiate(requiresToolLabel, buttonClone.transform.parent).GetComponent<UILabel>();
            MonoBehaviour.Destroy(consumeLabel.GetComponent<UIAnchor>());
            consumeLabel.transform.localPosition = buttonClone.transform.localPosition + new Vector3(0, -24);
            consumeLabel.color += new Color(0f, 0.3f, -0.1f);

            stopLabel = GameObject.Instantiate(requiresToolLabel, buttonClone.transform.parent).GetComponent<UILabel>();
            MonoBehaviour.Destroy(stopLabel.GetComponent<UIAnchor>());
            stopLabel.transform.localPosition = buttonClone.transform.localPosition + new Vector3(0, -44);
            stopLabel.color += new Color(0f, 0.3f, -0.1f);

			Toggle(false);
            PIE = pie;
        }

        Transform FindChildWrapper (Transform transform, string childPath)
        {
            var target = transform.FindChild(childPath);
            if (target == null)
            {
                ExamineActionsAPI.Instance.LoggerInstance.Error($"Failed to grab official UI gameobject: {childPath} from {transform.name} (this is likely caused by UI changed by game update or other mods)");
                var lastSep = childPath.LastIndexOf('/');
                if (lastSep != -1) ExamineActionsAPI.Instance.LoggerInstance.Error($"Traversing up...");
                while (lastSep != -1)
                {
                    childPath = childPath.Substring(0, lastSep);
                    target = transform.FindChild(childPath);
                    if (target == null)
                        ExamineActionsAPI.Instance.LoggerInstance.Error($"Failed to find {childPath}");
                    else
                    {
                        ExamineActionsAPI.Instance.LoggerInstance.Error($"Found {childPath}");
                        break;
                    }

                    lastSep = childPath.LastIndexOf('/');
                }
                target = null;
            }
            return target;
        }

        void IExamineActionPanel.OnActionDeselected(ExamineActionState state) {}

        void IExamineActionPanel.OnActionSelected(ExamineActionState state)
        {
            materialsClone.SetActive(false);
            yieldsClone.SetActive(false);
            PIE.m_ActionToolSelect.SetActive(false);
            bg2.gameObject.SetActive(false);
            int subs = state.Action.GetSubActionCounts(state);
            ExamineActionsAPI.VeryVerboseLog($"Sub {state.SubActionId + 1} / {subs}");
            buttonSubLeft.gameObject.SetActive(state.SubActionId > 0);
            buttonSubRight.gameObject.SetActive(state.SubActionId < subs - 1);
            bool extendInfoBlock = false;
            UpdateConsumeLabel(state);
            UpdateStopLabel(state);
            MaybeSetupMaterials(state);
            MaybeSetupProducts(state);
            MaybeSetupTools(state);
            UpdateInfoBlock(state);

            bool displayingMats = state.Action is IExamineActionRequireMaterials rMats && rMats.GetMaterials(state) != null;
            bool displayingProducts = state.Action is IExamineActionProduceItems pItms && pItms.GetProducts(state) != null;
            int mid = infoBlockExtended ? -58 : -30;
            int upperY = infoBlockExtended ? -30 : 0;
            int lowerY = infoBlockExtended ? -134 : -112;
            if (displayingMats && displayingProducts)
            {
                materialsClone.transform.localPosition = new Vector3(materialsClone.transform.localPosition.x, upperY, materialsClone.transform.localPosition.z);
                yieldsClone.transform.localPosition = new Vector3(yieldsClone.transform.localPosition.x, lowerY, yieldsClone.transform.localPosition.z);
            }
            else if (displayingMats)
            {
                materialsClone.transform.localPosition = new Vector3(materialsClone.transform.localPosition.x, mid, materialsClone.transform.localPosition.z);
            }
            else if (displayingProducts)
            {
                yieldsClone.transform.localPosition = new Vector3(yieldsClone.transform.localPosition.x, mid, yieldsClone.transform.localPosition.z);
            }



            buttonLabel.text = state.Action.ActionButtonLocalizedString.Text();
        }

        private void UpdateInfoBlock(ExamineActionState state)
        {
            infoDuration.label2.text = Utils.GetExpandedDurationString(state.ActiveActionDurationMinutes.Value);

            bool showingChance = false;
            if (state.Action is IExamineActionFailable eaf)
            {
                infoChance.label2.text = string.Format("{0:0.0}%", state.ActiveSuccessChance);
                infoChance.go.SetActive(true);
                showingChance = true;
            }
            else
            {
                infoChance.go.SetActive(false);
            }

            if (!ExamineActionsAPI.DISABLE_CUSTOM_INFO
             && state.Action is IExamineActionCustomInfo eaci)
            {
                var conf1 = eaci.GetInfo1(state);
                var conf2 = eaci.GetInfo2(state);
                if ((showingChance && conf1 != null) || (showingChance && conf2 != null) || (conf1 != null && conf2 != null))
                    SetInfoBlock2Rows();
                else SetInfoBlock1Row();

                if (showingChance)
                {
                    info1.go.transform.localPosition = info1Pos;
                    info2.go.transform.localPosition = info2Pos;
                }
                else
                {
                    info1.go.transform.localPosition = infoChance.go.transform.localPosition;
                    info2.go.transform.localPosition = info1Pos;
                }

                SetupCustomInfos(eaci, state, conf1, conf2);
            }
            else
            {
                info1.go.SetActive(false);
                info2.go.SetActive(false);
                SetInfoBlock1Row();
            }
        }

        private void MaybeSetupMaterials(ExamineActionState state)
        {
            if (state.Action is IExamineActionRequireMaterials eam)
            {
                materiaslLabel.color = (state.AllMaterialsReady.Value ? PIE.m_RequiredMaterialsLabelDefaultColor : PIE.m_RepairLabelColorDisabled);
                var mats = eam.GetMaterials(state);
                if (mats != null)
                {
                    float num2 = (float)mats.Length / 2f - 0.5f;
                    for (int i = 0; i < mats.Length; i++)
                    {
                        Vector3 localPosition = this.Materials[i].transform.localPosition;
                        localPosition.x = PIE.m_RequiredMaterialCenteredX;
                        localPosition.x += PIE.m_RequiredMaterialSpacing * ((float)i - num2) / (mats.Length >=4? 2f: 1.2f);
                        this.Materials[i].transform.localPosition = localPosition;
                        this.Materials[i].transform.localScale = mats.Length >= 4 ? new Vector3(0.8f, 0.8f, 1) : new Vector3(1f, 1f, 1);
                        int stackNum = mats[i].Item2;
                        int checkingStackNum = mats[i].Item2;
                        if (state.Subject.name == mats[i].Item1) checkingStackNum += state.Subject.m_StackableItem?.m_Units?? 1;
                        for (int j = 0; j < i; j++)
                            if (mats[i].Item1 == mats[j].Item1) checkingStackNum += mats[j].Item2;
                        var invItem = GameManager.GetInventoryComponent().GearInInventory(mats[i].Item1, checkingStackNum);
                        GearItem prefab = GearItem.LoadGearItemPrefab(mats[i].Item1);
                        if (prefab == null)
                            MelonLogger.Error($"Invalid material: {mats[i].Item1}. There will be exception following this line of log. Check do you have the item mod installed or contact the mod providing this action or this action recipe.");
                        this.Materials[i].ShowItem(prefab, stackNum, invItem != null);
                        if (mats[i].Item3 < 100)
                        {
                            this.materialChances[i].text = $"{mats[i].Item3}%";
                            this.materialChances[i].gameObject.SetActive(true);
                        }
                        else this.materialChances[i].gameObject.SetActive(false);
                    }
                    for (int j = mats.Length; j < this.Materials.Count; j++)
                    {
                        this.Materials[j].Hide();
                    }
                    materialsClone.SetActive(true);
                    if (!bg2.gameObject.activeSelf) bg2.gameObject.SetActive(true);
                }
                else
                {
                    // foreach (var m in Materials)
                    //     m.Hide();
                }
                
            }
        }

        private void MaybeSetupProducts(ExamineActionState state)
        {
            (string gear_name, int units, byte chance)[] mats = null;
            if (state.Action is IExamineActionProduceItems eap)
                mats = eap.GetProducts(state);
            List<(GearLiquidTypeEnum type, float units, byte chance)> liquids = null;
            if (state.Action is IExamineActionProduceLiquid eapl)
            {
                liquids = new();
                eapl.GetProductLiquid(state, liquids);
            }
            List<(PowderType type, float units, byte chance)> powders = null;
            if (state.Action is IExamineActionProducePowder eapp)
            {
                powders = new();
                eapp.GetProductPowder(state, powders);
            }

            int matCount = mats?.Length ?? 0;
            int liqCount = liquids?.Count ?? 0;
            int powCount = powders?.Count ?? 0;
            int total = matCount + liqCount + powCount;
            if (total == 0) return;

            for (int configured = 0; configured < 5; configured++)
            {
                if (configured >= total)
                {
                    this.Products[configured].Hide();
                    continue;
                }
    
                Vector3 localPosition = this.Products[configured].transform.localPosition;
                localPosition.x = PIE.m_RequiredMaterialCenteredX;
                localPosition.x += PIE.m_RequiredMaterialSpacing * ((float)configured - (total / 2f - 0.5f)) / (total >=4? 2f: 1.2f);
                this.Products[configured].transform.localPosition = localPosition;
                this.Products[configured].transform.localScale = total >= 4 ? new Vector3(0.8f, 0.8f, 1) : new Vector3(1f, 1f, 1);

                if (configured >= matCount + liqCount) // Powder
                {
                    var conf = powders[configured - matCount];
                    
                    if (conf.chance < 100)
                    {
                        this.productChances[configured].text = $"{conf.chance}%";
                        this.productChances[configured].gameObject.SetActive(true);
                    }
                    else this.productChances[configured].gameObject.SetActive(false);

                    this.Products[configured].ShowPowder(conf.type, conf.units);
                }
                else if (configured >= matCount) // Liquid
                {
                    var conf = liquids[configured - matCount];
                    
                    if (conf.chance < 100)
                    {
                        this.productChances[configured].text = $"{conf.chance}%";
                        this.productChances[configured].gameObject.SetActive(true);
                    }
                    else this.productChances[configured].gameObject.SetActive(false);

                    this.Products[configured].ShowLiquid(conf.type, conf.units, false);
                }
                else
                {
                    var conf = mats[configured];
                    GearItem prefab = GearItem.LoadGearItemPrefab(conf.gear_name);
                    if (prefab == null)
                        MelonLogger.Error($"Invalid prodcut: {conf.gear_name}. There will be exception following this line of log. Contact mod providing this action or this action recipe.");

                    if (conf.chance < 100)
                    {
                        this.productChances[configured].text = $"{conf.chance}%";
                        this.productChances[configured].gameObject.SetActive(true);
                    }
                    else this.productChances[configured].gameObject.SetActive(false);
                    this.Products[configured].ShowItem(prefab, conf.units);
                }
            }
            yieldsClone.SetActive(true);
            if (!bg2.gameObject.activeSelf) bg2.gameObject.SetActive(true);
        }

        void IExamineActionPanel.OnPerformingAction(ExamineActionState state) {}

        void IExamineActionPanel.OnSelectingTool(ExamineActionState state)
        {
            bg2.gameObject.SetActive(true);
            yieldsClone.gameObject.SetActive(false);
            materialsClone.gameObject.SetActive(false);
        }

        void IExamineActionPanel.OnToolSelected(ExamineActionState state)
        {
            bg2.gameObject.SetActive(false);
        }
		public void Toggle (bool toggle)
		{
			repairPanelClone.SetActive(toggle);
		}
		public void SetupCustomInfos (IExamineActionCustomInfo act, ExamineActionState state, InfoItemConfig? conf1, InfoItemConfig? conf2)
		{
			if (conf1 != null && conf2 != null)
			{
				this.info1.label1.text = conf1.Title.Text();
				this.info1.label2.text = conf1.Content;
				this.info2.label1.text = conf2.Title.Text();
				this.info2.label2.text = conf2.Content;
				this.info1.go.SetActive(true);
				this.info2.go.SetActive(true);
			}
			else if (conf1 != null && conf2 == null)
			{
				this.info1.label1.text = conf1.Title.Text();
				this.info1.label2.text = conf1.Content;
				this.info1.go.SetActive(true);
				this.info2.go.SetActive(false);
			}
			else if (conf1 == null && conf2 != null)
			{
				this.info1.label1.text = conf2.Title.Text();
				this.info1.label2.text = conf2.Content;
				this.info1.go.SetActive(true);
				this.info2.go.SetActive(false);
			}
		}

        private void MaybeSetupTools(ExamineActionState state)
        {
            if (state.Action is IExamineActionRequireTool eat)
            {
                if (PIE.m_Tools.Count == 0)
                {
                    requiresToolLabel.gameObject.SetActive(true);
                    requiresToolLabel.text = Localization.Get("GAMEPLAY_ToolRequired");
                }
                else requiresToolLabel.gameObject.SetActive(false);

                PIE.m_ToolScrollList.CleanUp();
                PIE.m_ToolScrollList.CreateList(PIE.m_Tools.Count);
                for (int k = 0; k < PIE.m_Tools.Count; k++)
                {
                    UITexture componentInChildren = Utils.GetComponentInChildren<UITexture>(PIE.m_ToolScrollList.m_ScrollObjects[k]);
                    if (PIE.m_Tools[k] != null)
                    {
                        GearItem component7 = PIE.m_Tools[k].GetComponent<GearItem>();
                        if (component7)
                        {
                            componentInChildren.mainTexture = Utils.GetInventoryIconTexture(component7);
                        }
                    }
                    else
                    {
                        componentInChildren.mainTexture = Utils.GetInventoryGridIconTexture(PIE.m_NoToolSpriteName);
                        componentInChildren.color = PIE.m_NoToolSpriteColor;
                        componentInChildren.transform.localScale = PIE.m_NoToolSpriteSizeModifier;
                    }
                }
                PIE.RefreshSelectedActionTool();
            }
            else requiresToolLabel.gameObject.SetActive(false);
        }

        void UpdateStopLabel (ExamineActionState state)
        {
            var cancellable = state.Action is IExamineActionCancellable;
            var interruptable = state.Action is IExamineActionInterruptable;
            stopLabel.gameObject.SetActive(true);

            if (cancellable && interruptable)
                stopLabel.text = "Can be cancelled (ESC) / May be interrupted";
            else if (!cancellable && interruptable)
                stopLabel.text = "Can not cancelled / May be interrupted";
            else if (cancellable && !interruptable)
                stopLabel.text = "Can be cancelled (ESC)";
            else if (!cancellable && !interruptable)
                stopLabel.text = "Must be finished";

        }

        void UpdateConsumeLabel (ExamineActionState state)
        {
            var consumeS = state.Action.ConsumeOnSuccess(state);
            var consumeF = (state.Action as IExamineActionFailable)?.ConsumeOnFailure(state) ?? false; // null means anyway not possible to happens
            var consumeC = (state.Action as IExamineActionCancellable)?.ConsumeOnCancel(state) ?? false;
            var consumeI = (state.Action as IExamineActionInterruptable)?.ConsumeOnInterruption(state) ?? false;
            
            if (!consumeS && !consumeF && !consumeC && !consumeI)
                consumeLabel.text = "Never consume";
            if (consumeS && consumeF && consumeC && consumeI)
                consumeLabel.text = "Always consume";
            else if (consumeS && consumeF && consumeC)
                consumeLabel.text = "Consume on: Success / Failure / Cancellation";
            else if (consumeF && consumeC && consumeI)
                consumeLabel.text = "Consume on: Failure / Cancellation / Interruption";
            else if (consumeS && consumeC && consumeI)
                consumeLabel.text = "Consume on: Success / Cancellation / Interruption";
            else if (consumeS && consumeF && consumeI)
                consumeLabel.text = "Consume on: Success / Failure / Interruption";
            else if (consumeS && consumeF)
                consumeLabel.text = "Consume on: Success / Failure";
            else if (consumeS && consumeC)
                consumeLabel.text = "Consume on: Success / Cancellation";
            else if (consumeS && consumeI)
                consumeLabel.text = "Consume on: Success / Interruption";
            else if (consumeF && consumeC)
                consumeLabel.text = "Consume on: Failure / Cancellation";
            else if (consumeF && consumeI)
                consumeLabel.text = "Consume on: Failure / Interruption";
            else if (consumeC && consumeI)
                consumeLabel.text = "Consume on: Cancellation / Interruption";
            else if (consumeS)
                consumeLabel.text = "Consume on: Success";
            else if (consumeF)
                consumeLabel.text = "Consume on: Failure";
            else if (consumeC)
                consumeLabel.text = "Consume on: Cancellation";
            else if (consumeI)
                consumeLabel.text = "Consume on: Interruption";

            int consuming = state.Action.OverrideConsumingUnits(state);
            if (consuming > 1)
                consumeLabel.text += $" (x{consuming})";
        }

        void SetInfoBlock1Row ()
		{
			bg1.transform.localPosition = new Vector3(bg1.transform.localPosition.x, 70, bg1.transform.localPosition.z);
			bg1.height = 76;
			bg2.height = 366;
			infoBlockExtended = false;
		}

		void SetInfoBlock2Rows ()
		{
			bg1.transform.localPosition = new Vector3(bg1.transform.localPosition.x, 22, bg1.transform.localPosition.z);
			bg2.transform.localPosition = new Vector3(bg2.transform.localPosition.x, -296, bg2.transform.localPosition.z);
			bg1.height = 124;
			bg2.height = 318;
			infoBlockExtended = true;
		}

        public void OnActionSucceed(ExamineActionState state) {}

        public void OnActionFailed(ExamineActionState state) {}

        public void OnActionCancelled(ExamineActionState state) {}

        public void OnActionInterrupted(ExamineActionState state) {}

        public void OnSelectingToolChanged(ExamineActionState state)
        {
            UpdateInfoBlock(state);
        }

        public class InfoBlock
		{
			internal GameObject go;
			internal UISprite bg;
			internal UILabel label1, label2;

            public InfoBlock(GameObject go)
            {
                this.go = go;
				bg = go.GetComponentInChildren<UISprite>();

                Transform header = go.transform.FindChild("Header");
                MonoBehaviour.Destroy(header.GetComponent<UILocalize>());

				label1 = header.GetComponent<UILabel>();
				label2 = go.transform.FindChild("Repair_AmountLabel_Value")?.gameObject?.GetComponent<UILabel>();

				label1.text = "-";
				label2.text = "---";
            }

            public InfoBlock(GameObject go, UILabel header, UILabel content)
            {
                this.go = go;
				bg = go.GetComponentInChildren<UISprite>();

                MonoBehaviour.Destroy(header.GetComponent<UILocalize>());
				label1 = header;
				label2 = content;
            }

			public void Set (string header, string content)
			{
				label1.text = header;
				label2.text = content;
			}
        }
    }
}
